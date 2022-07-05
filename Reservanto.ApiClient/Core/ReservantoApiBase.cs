using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Reservanto.ApiClient.Authorization;
using Reservanto.ApiClient.Public;
using Reservanto.ApiClient.Requests;
using Reservanto.ApiClient.Responses;

namespace Reservanto.ApiClient.Core
{
	/// <summary>
	/// Bázová třída pro práci s Reservanto API (ať už v módu s obchodníkem nebo bez).
	/// </summary>
	public abstract class ReservantoApiBase
	{
		private readonly string apiUrl;
		private readonly string clientId;
		private readonly string[] rights;

		/// <summary>
		/// Vytvoří novou instanci bázové třídy pro komunikaci s Reservanto API.
		/// </summary>
		/// <param name="apiUrl">Url adresa, na které se nachází API (např. https://merchant.reservanto.cz/api_v1). </param>
		/// <param name="clientId">Client Id přiděleno Reservantem.</param>
		/// <param name="ltt">Long Time Token, pro přístup k API.</param>
		/// <param name="rights">Pole s oprávněními, nutno zadávat pokud <c><see cref="ReservantoApiBase.AdminMode"/> = true</c></param>
		protected ReservantoApiBase(string apiUrl, string clientId, string ltt, string[] rights)
		{
			this.apiUrl = apiUrl;
			this.clientId = clientId;
			this.rights = rights;
			this.Ltt = ltt;

			if (this.AdminMode)
			{
				if (this.rights == null || this.rights.Length == 0)
				{
					throw new ArgumentException("The rights array cannot be null or empty in Admin mode", nameof(rights));
				}
			}
			else
			{
				if (string.IsNullOrEmpty(ltt))
				{
					throw new ArgumentException("Ltt cannot be null or empty in non-Admin mode", nameof(ltt));
				}
			}
		}

		#region Implement

		/// <summary>
		/// Vrací, zda je tato instance api v Admin módu (bez přihlášeného obchodníka).
		/// Tzn. není nutné znát LTT, protože se stáhne při prvním requestu.
		/// V tomto módu je ale nutné mít nenull pole rights.
		/// </summary>
		protected abstract bool AdminMode
		{
			get;
		}

		#endregion

		#region stt, ltt

		private string lttUnsafe;
		private string sttUnsafe;

		/// <summary>
		/// Zámky pro nastavování tokenů (aby se nepřepisovali např. při přístupu z více vláken).
		/// </summary>
		private readonly object lttLock = new object();
		private readonly object sttLock = new object();

		/// <summary>
		/// Vrací nebo nastavuje Long Time Token, používaný pro komunikaci s API.
		/// </summary>
		private string Ltt
		{
			get
			{
				return this.lttUnsafe;
			}
			set
			{
				lock (this.lttLock)
				{
					this.lttUnsafe = value;
				}
			}
		}

		/// <summary>
		/// Vrací nebo nastavuje Short Time Token, používaný pro komunikaci s API.
		/// </summary>
		private string Stt
		{
			get
			{
				return this.sttUnsafe;
			}
			set
			{
				lock (this.sttLock)
				{
					this.sttUnsafe = value;
				}
			}
		}

		#endregion

		#region Request helpers

		/// <summary>
		/// Akce, provedená pokud dotaz na API skončí chybou.
		/// </summary>
		private Action<Exception> onFail = null;

		/// <summary>
		/// Nastaví akci, která se má provést, když se nepodaří nějaký request.
		/// </summary>
		/// <param name="onFail">Nová akce - nesmí být null</param>
		public void SetOnFail(Action<Exception> onFail)
		{
			if(onFail == null) throw new ArgumentNullException(nameof(onFail));

			this.onFail = onFail;
		}

		/// <summary>
		/// Akce, provedená pokud dotaz na API skončí chybou (např. 404 - not found, 403 - forbidden...).
		/// </summary>
		/// <param name="e">Výjimka, kterou komunikace vyvolala.</param>
		internal virtual void OnFail(Exception e)
		{
			this.onFail?.Invoke(e);
			Debug.Fail(e.Message, e.StackTrace);
		}

		/// <summary>
		/// Bezpečné provedení dotazu na API.
		/// </summary>
		/// <typeparam name="T">Návratový typ dotazu na API, musí dědit z <see cref="Request"/>.</typeparam>
		/// <param name="function">Samotný dotaz na API.</param>
		/// <returns>Odpověď API.</returns>
		internal T Safe<T>(Func<T> function) where T : Response
		{
			try
			{
				T result = function();
				return result;
			}
			catch (HttpRequestException e)
			{
				// Při selhání komunikace vrací výchozí hodnoty návratového objektu.
				OnFail(e);
				return default(T);
			}
		}

		/// <summary>
		/// Provede dotaz na API.
		/// </summary>
		/// <typeparam name="T">Návratový typ dotazu na API, musí dědit z <see cref="Request"/>.</typeparam>
		/// <param name="path">Akce, na kterou se dotaz provádí např. "Event/Get".</param>
		/// <returns>Odpověď API.</returns>
		internal T MakeRequest<T>(string path) where T : Response
		{
			return MakeRequest<T, RequestModel>(path, RequestModel.Empty);
		}

		/// <summary>
		/// Provede dotaz na API.
		/// </summary>
		/// <typeparam name="T">Návratový typ dotazu na API, musí dědit z <see cref="Request"/>.</typeparam>
		/// <typeparam name="TRequestModel">Rozhraní obsahující parametry požadavku.</typeparam>
		/// <param name="path">Akce, na kterou se dotaz provádí např. "Event/Get".</param>
		/// <returns>Odpověď API.</returns>
		internal T MakeRequest<T, TRequestModel>(string path, TRequestModel requestModel) where T : Response where TRequestModel : IRequestModel
		{
			return MakeRequest<T>(path, new Request<TRequestModel>(requestModel));
		}

		/// <summary>
		/// Provede dotaz na API.
		/// </summary>
		/// <typeparam name="T">Návratový typ dotazu na API, musí dědit z <see cref="Request"/>.</typeparam>
		/// <param name="request">Tělo (parametry) požadavku.</param>
		/// <param name="path">Akce, na kterou se dotaz provádí např. "Event/Get".</param>
		/// <returns>Odpověď API.</returns>
		internal T MakeRequest<T>(string path, Request request) where T : Response
		{
			// Long Time Token je povinný
			EnsureLtt();

			// Short Time Token ještě není získáný -> sáhnutí tokenu.
			if (string.IsNullOrEmpty(this.Stt))
			{
				RenewStt();
			}

			// První pokus získání dat.
			var message = MakeRequestRaw(path, request);
			var status = (int)message.StatusCode;
			var result = ReadContent(message);

			// Při statusu 401 - snaha obnovit Short Time Token.
			if (status == 401)
			{
				RenewStt();

				// A Druhý pokus o získání dat.
				message = MakeRequestRaw(path, request);
				status = (int)message.StatusCode;
				result = ReadContent(message);
			}

			// Status 200 = odpověď je v pořádku - lze vrátit.
			if (status == 200)
			{
				T response = JsonConvert.DeserializeObject<T>(result);
				return response;
			}

			// Dotaz úspěšný nebyl - vyhození výjimky.
			throw new HttpRequestException($"Request was not successful ({status}).\r\n\r\n {result} ");
		}

		/// <summary>
		/// Zajistí načtený Long Time Token.
		/// </summary>
		private void EnsureLtt()
		{
			// Long Time Token je prázdný.
			if (string.IsNullOrEmpty(this.Ltt))
			{
				// Nové získání Long Time Tokenu.
				var message = MakeRequestRaw("Authorize/GetClientLongTimeToken", new LttRequest(this.clientId, this.rights));

				var result = ReadContent(message);
				var status = (int)message.StatusCode;

				// Token se povedl získat.
				if (status == 200)
				{
					var response = JsonConvert.DeserializeObject<LttResponse>(result);
					
					// Odpověď je v pořádku.
					if (response != null && !response.IsError && response.Accepted)
					{
						// Uložení Long Time Tokenu.
						this.Ltt = response.Token;
						return;
					}
				}

				// Token se nepodařilo získat - vyhození výjimky.
				throw new HttpRequestException($"Cannot get LTT. ({status}) \r\n\r\n {result}");
			}
		}

		/// <summary>
		/// Zajistí stažení (obnovení) Short Time Tokenu.
		/// </summary>
		private void RenewStt()
		{
			// Nové získání Short Time Tokenu.
			var message = MakeRequestRaw("Authorize/GetShortTimeToken", new SttRequest(this.Ltt));

			var result = ReadContent(message);
			var status = (int)message.StatusCode;

			// Token se povedlo získat.
			if (status == 200)
			{
				var response = JsonConvert.DeserializeObject<SttResponse>(result);
				
				// Odpověď je v pořádku.
				if (response != null && !response.IsError)
				{
					// Uložení Short Time Tokenu.
					this.Stt = response.ShortTimeToken;
					return;
				}
			}

			// Token se nepodařilo získat - vyhození výjimky.
			throw new HttpRequestException($"Cannot get STT. ({status}) \r\n\r\n {result}");
		}

		/// <summary>
		/// Provede dotaz na API.
		/// </summary>
		/// <param name="path">Akce, na kterou se dotaz provádí např. "Event/Get".</param>
		/// <param name="request">Tělo (parametry) požadavku.</param>
		/// <returns>Vrací survou odpověď serveru,</returns>
		private HttpResponseMessage MakeRequestRaw(string path, Request request)
		{
			// Dotaz pomocí Http Clienta.
			using (var client = new HttpClient())
			{
				// Vytvoření "zprávy", která se pošle na API.
				var message = new HttpRequestMessage(HttpMethod.Post, this.apiUrl + path);
				
				// Vytvoření těla požadavku.
				var requestContent = request.Serialize();
				message.Content = new StringContent(requestContent);

				// Nastavení typu požadavku (JSON).
				message.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

				// Pokud je Short Time Token uložen, použije se pro ověření.
				var stt = this.Stt;
				if (!string.IsNullOrEmpty(stt))
				{
					message.Headers.Authorization = new AuthenticationHeaderValue(stt);
				}

				// ignore certificate errors (for debug)
#if DEBUG
				ServicePointManager.ServerCertificateValidationCallback +=
					(sender, cert, chain, sslPolicyErrors) => true;
#endif

				// Odeslání "zprávy" na API.
				Task<HttpResponseMessage> task = client.SendAsync(message);

				return task.Result;
			}
		}

		/// <summary>
		/// Pomocná metoda pro čtení výsledku dotazu na API.
		/// </summary>
		/// <param name="message">Surová odpověď serveru.</param>
		/// <returns>Odpověď serveru, převedená na string (JSON).</returns>
		private static string ReadContent(HttpResponseMessage message)
		{
			var task = message.Content.ReadAsStringAsync();
			return task.Result;
		}

		#endregion
	}
}
