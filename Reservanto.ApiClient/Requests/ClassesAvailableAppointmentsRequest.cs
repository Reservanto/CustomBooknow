using System;

namespace Reservanto.ApiClient.Requests
{
	/// <summary>
	/// Požadavek pro získání všech rezervovatelných skupinových událostí.
	/// </summary>
	internal class ClassesAvailableAppointmentsRequest : AvailabilityRequest
	{
		/// <summary>
		/// Vytvoří nový požadavek pro získání všech dostupných lekcí pro danou provozovnu a zaměření.
		/// </summary>
		/// <param name="segmentId">Zaměření, pro kterou získá události.</param>
		/// <param name="locationId">Provozovna, pro kterou získá události.</param>
		public ClassesAvailableAppointmentsRequest(int segmentId, int locationId, DateTime from, DateTime to)
			: base(from, to)
		{
			this.SegmentId = segmentId;
			this.LocationId = locationId;
		}

		/// <summary>
		/// Vrací nebo nastavuje Id zaměření, ze kterého se události získají.
		/// </summary>
		public int SegmentId
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje Id provozovny, ze které se události získávají.
		/// </summary>
		public int LocationId
		{
			get;
			set;
		}
	}
}
