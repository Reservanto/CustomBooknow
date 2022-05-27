namespace Reservanto.ApiClient.Public
{
	/// <summary>
	/// Třída, obsahující výčet všech oprávnění, které lze vyžádat na API.
	/// </summary>
	public static class ApiRights
	{
		/// <summary>
		/// Identifikátor práva ke čtení zákazníků.
		/// </summary>
		public const string CUSTOMERS_READ = "Customer_r";

		/// <summary>
		/// Identifikátor práva k zápisu zákazníků.
		/// </summary>
		public const string CUSTOMERS_WRITE = "Customer_w";

		/// <summary>
		/// Identifikátor práva ke čtení událostí (informací o rezervaci).
		/// </summary>
		public const string EVENTS_READ = "Event_r";

		/// <summary>
		/// Identifikátor práva ke čtení služeb.
		/// </summary>
		public const string BOOKINGSERVICES_READ = "BookingService_r";

		/// <summary>
		/// Identifikátor práva pro čtení produktů a jejich kategorií.
		/// </summary>
		public const string PRODUCTS_READ = "Product_r";

		/// <summary>
		/// Identifikátor práva pro čtení permanentek.
		/// </summary>
		public const string PASSES_READ = "Pass_r";

		/// <summary>
		/// Identifikátor práva pro čtení platebních metod.
		/// </summary>
		public const string PAYMENTMETHODS_READ = "PaymentMethod_r";

		/// <summary>
		/// Identifikátor práva pro vytváření pokladních dokladů a s tím i získávání FIK z EET.
		/// </summary>
		public const string RECEIPTS_WRITE = "Receipt_w";

		/// <summary>
		/// Identifikátor práva pro čtení nastavení.
		/// </summary>
		public const string SETTINGS_READ = "Settings_r";

		/// <summary>
		/// Identifikátor práva pro čtení segmentů.
		/// </summary>
		public const string SEGMENTS_READ = "Segment_r";

		/// <summary>
		/// Identifikátor práva pro čtení provozoven.
		/// </summary>
		public const string LOCATIONS_READ = "Location_r";

		/// <summary>
		/// Identifikátor práva pro čtení zdrojů.
		/// </summary>
		public const string BOOKINGRESOURCES_READ = "BookingResource_r";

		/// <summary>
		/// Identifikátor práva pro čtení volných míst na rezervace.
		/// (Jedná se o přístup s možnostmi zákazníka - který má stejné možnosti jako v BookNow.)
		/// </summary>
		public const string FREESPACE_READ = "FreeSpace_r";

		/// <summary>
		/// Identifikátor práva pro zápis (vytváření/storno) zákaznických rezervací.
		/// (Jedná se o přístup s možnostmi zákazníka - který má stejné možnosti jako v BookNow.)
		/// </summary>
		public const string BOOKINGS_WRITE = "Booking_w";

		/// <summary>
		/// Identifikátor práva pro získání entit na základě externích identifikátorů.
		/// </summary>
		public const string EXTERNALIDENTIFIERS_READ = "ExternalIdentifiers_r";

		/// <summary>
		/// Identifikátor práva pro propojení entit s externími identifikátory.
		/// </summary>
		public const string EXTERNALIDENTIFIERS_WRITE = "ExternalIdentifiers_w";



		/// <summary>
		/// Identifikátor práva ke čtení obchodníků.
		/// </summary>
		public const string MERCHANT_ADMIN_READ = "Merchant_cr";

		/// <summary>
		/// Identifikátor práva k zápisu obchodníků.
		/// </summary>
		public const string MERCHANT_ADMIN_WRITE = "Merchant_cw";

		/// <summary>
		/// Identifikátor práva ke čtení provozoven.
		/// </summary>
		public const string LOCATION_ADMIN_READ = "Location_cr";

		/// <summary>
		/// Identifikátor práva k zápisu provozoven.
		/// </summary>
		public const string LOCATION_ADMIN_WRITE = "Location_cw";

		/// <summary>
		/// Identifikátor práva ke čtení zdrojů.
		/// </summary>
		public const string BOOKINGRESOURCE_ADMIN_READ = "BookingResource_cr";

		/// <summary>
		/// Identifikátor práva k zápisu zdrojů.
		/// </summary>
		public const string BOOKINGRESOURCE_ADMIN_WRITE = "BookingResource_cw";

		/// <summary>
		/// Identifikátor práva ke čtení služeb.
		/// </summary>
		public const string BOOKINGSERVICE_ADMIN_READ = "BookingService_cr";

		/// <summary>
		/// Identifikátor práva k zápisu služeb.
		/// </summary>
		public const string BOOKINGSERVICE_ADMIN_WRITE = "BookingService_cw";

		/// <summary>
		/// Identifikátor práva k přímému volání akcí pluginu.
		/// </summary>
		public const string PLUGIN_ADMIN_CALL = "Plugin_c";

	}
}
