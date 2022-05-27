using System;
using Newtonsoft.Json;
using Reservanto.ApiClient.Public;

namespace Reservanto.ApiClient.Requests
{
	/// <summary>
	/// Základní dotaz na API.
	/// </summary>
	internal abstract class Request
	{
		/// <summary>
		/// Vytvoří nový základní dotaz a naplní ho časovým razítkem.
		/// </summary>
		protected Request()
		{
			var now = DateTime.UtcNow;
			var old = new DateTime(1980, 1, 1);
			TimeStamp = (int)(now - old).TotalSeconds;
		}

		/// <summary>
		/// Vrací nebo nastavuje časové razítko dotazu.
		/// </summary>
		public double TimeStamp
		{
			get;
			set;
		}

		/// <summary>
		/// Převede požadavek do formátu JSON.
		/// </summary>
		/// <returns>String, obsahující tento požadavek ve formátu JSON.</returns>
		public string Serialize()
		{
			return JsonConvert.SerializeObject(this);
		}

		/// <summary>
		/// Vrací DateTime překonvertovaný do UnixTimeStamp (UTC).
		/// </summary>
		protected static double ToUnixTimeStamp(DateTime dt)
		{
			TimeSpan time = dt.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
			return Math.Round(time.TotalSeconds);
		}
	}

	/// <summary>
	/// Požadavek, který obsahuje pouze model.
	/// </summary>
	/// <typeparam name="T">Typ modelu požadavku.</typeparam>
	internal class Request<T> : Request where T : IRequestModel
	{
		/// <summary>
		/// Vytvoří nový požadavek s modelem (<c>Model = null</c>).
		/// </summary>
		public Request()
		{
		}

		/// <summary>
		/// Vytvoří nový požadavek s modelem.
		/// </summary>
		/// <param name="model">Předaný model, který je v požadavku obsažen.</param>
		public Request(T model)
		{
			this.Model = model;
		}

		/// <summary>
		/// Vrací nebo nastavuje model požadavku.
		/// </summary>
		public T Model
		{
			get;
			set;
		}
	}
}
