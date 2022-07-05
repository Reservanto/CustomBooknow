using System;

namespace Reservanto.ApiClient.Requests
{
	/// <summary>
	/// Dotaz pro získání pracovních hodin v daném intervalu.
	/// </summary>
	internal class WorkingHoursRequest : AvailabilityRequest
	{
		/// <summary>
		/// Vytvoří nový dotaz, pro získání pracovních hodin.
		/// Stahuje pracovní hodiny pro konkrétní zdroj, pokud není specifikován tak pro konkrétní provozovnu.
		/// </summary>
		/// <param name="locationId">Id provozovny, pro kterou se pracovní hodiny stahují.</param>
		/// <param name="bookingResourceId">Id zdroje, pro který se pracovní hodiny stahují.</param>
		public WorkingHoursRequest(int? locationId, int? bookingResourceId, DateTime from, DateTime to)
			: base (from, to)
		{
			this.LocationId = locationId;
			this.BookingResourceId = bookingResourceId;
		}

		/// <summary>
		/// Vrací nebo nastavuje Id provozovny.
		/// </summary>
		public int? LocationId
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje Id zdroje.
		/// </summary>
		public int? BookingResourceId
		{
			get;
			set;
		}
	}
}
