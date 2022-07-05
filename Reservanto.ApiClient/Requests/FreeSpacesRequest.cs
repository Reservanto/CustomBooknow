using System;

namespace Reservanto.ApiClient.Requests
{
	/// <summary>
	/// Request na API, který vrátí všechny možné termíny rezervace.
	/// </summary>
	internal class FreeSpacesRequest : AvailabilityRequest
	{
		/// <summary>
		/// Vytvoří nový dotaz pro získání všech rezervovatelných termínů pro kombinaci zdroj/služba.
		/// </summary>
		/// <param name="bookingResourceId">Požadovaný zdroj, pro který se stahují termíny.</param>
		/// <param name="bookingServiceId">Požadovaná služba, pro který se stahují termíny.</param>
		public FreeSpacesRequest(int bookingResourceId, int bookingServiceId, DateTime from, DateTime to)
			: base(from, to)
		{
			this.BookingServiceId = bookingServiceId;
			this.BookingResourceId = bookingResourceId;
		}

		/// <summary>
		/// Vrací nebo nastavuje id zdroje, který rezervuji.
		/// </summary>
		public int BookingResourceId
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje id služby, kterou rezervuji.
		/// </summary>
		public int BookingServiceId
		{
			get;
			set;
		}
	}
}
