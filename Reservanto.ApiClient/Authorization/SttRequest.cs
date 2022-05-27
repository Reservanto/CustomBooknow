using Reservanto.ApiClient.Requests;

namespace Reservanto.ApiClient.Authorization
{
	/// <summary>
	/// Požadavek pro získání Short Time Tokenu pro přístup k API.
	/// </summary>
	internal class SttRequest : Request
	{
		/// <summary>
		/// Vytvoří novou instanci požadavku pro získání Short Time Tokenu.
		/// </summary>
		/// <param name="longTimeToken">Long Time Token pro přístup k API.</param>
		public SttRequest(string longTimeToken)
		{
			this.LongTimeToken = longTimeToken;
		}

		/// <summary>
		/// Vrací nebo nastavuje Long Time Token, na základě kterého se získává Short Time Token.
		/// </summary>
		public string LongTimeToken
		{
			get;
			set;
		}
	}
}
