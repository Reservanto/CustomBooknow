namespace Reservanto.CustomBooknow.Code.ReservantoApiClient
{
	/// <summary>
	/// Konfigurační třída API Reservanta.
	/// </summary>
	public class ReservantoApiConfiguration
	{
		/// <summary>
		/// Vrací nebo nastavuje id klienta na straně Reservanta.
		/// </summary>
		public string ClientId
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje url adresu, na které se nachází API.
		/// </summary>
		public string ApiUrl
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje požadovaná práva.
		/// </summary>
		public string[] RequestedRights
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje Long Time Token pro přístup do Reservanta.
		/// </summary>
		public string LongTimeToken
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje Short Time Token pro přístup do Reservanta.
		/// </summary>
		public string ShortTimeToken
		{
			get;
			set;
		}
	}
}
