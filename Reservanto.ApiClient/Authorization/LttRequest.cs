using Reservanto.ApiClient.Requests;

namespace Reservanto.ApiClient.Authorization
{
	/// <summary>
	/// Požadavek pro získání Long Time Tokenu pro přístup k API.
	/// </summary>
	internal class LttRequest : Request
	{
		/// <summary>
		/// Vytvoří novou instanci požadavku pro získání Long Time Tokenu.
		/// </summary>
		/// <param name="clientId">Id klienta API, přiděleno Reservantem.</param>
		/// <param name="rights">Práva, o které klient žádá.</param>
		public LttRequest(string clientId, string[] rights)
		{
			this.ClientId = clientId;
			this.Rights = new string[rights.Length];
			rights.CopyTo(this.Rights, 0);
		}

		/// <summary>
		/// Vrací nebo nastavuje Id klienta API (přiděleno Reservantem).
		/// </summary>
		public string ClientId
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje požadované práva viz. <see cref="ApiRights"/>.
		/// </summary>
		public string[] Rights
		{
			get;
			set;
		}
	}
}
