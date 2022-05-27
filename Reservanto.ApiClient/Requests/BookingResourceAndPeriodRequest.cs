using System;

namespace Reservanto.ApiClient.Requests
{
	/// <summary>
	/// Model pro dotaz, který obsahuje zdroj a časové omezení.
	/// </summary>
	internal class BookingResourceAndPeriodRequest : AvailabilityRequest
	{
		/// <summary>
		/// Vytvoří nový dotaz, omezený na zdroj a časové období.
		/// </summary>
		/// <param name="bookingResourceId">Id požadovaného zdroje.</param>
		public BookingResourceAndPeriodRequest(int bookingResourceId, DateTime from, DateTime to)
			: base(from, to)
		{
			this.BookingResourceId = bookingResourceId;
		}

		/// <summary>
		/// Vrací nebo nastavuje Id zdroje.
		/// </summary>
		public int BookingResourceId
		{
			get;
			set;
		}
	}
}
