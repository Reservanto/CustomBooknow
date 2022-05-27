using System;
using System.Collections.Generic;

namespace Reservanto.ApiClient.Requests
{
	/// <summary>
	/// Model pro dotaz, který vrací volné termíny zdrojů.
	/// </summary>
	internal class BookingResourceAvailabilityRequest : AvailabilityRequest
	{
		/// <summary>
		/// Dotaz, pro získání dostupnosti předaných zdrojů.
		/// </summary>
		/// <param name="resourceIds">Id předaných zdrojů.</param>
		public BookingResourceAvailabilityRequest(DateTime from, DateTime to, IEnumerable<int> resourceIds)
			: base(from, to)
		{
			this.BookingResourceIds = resourceIds;
		}

		/// <summary>
		/// Vrací nebo nastavuje kolekci vybraných id zdrojů, pro které se dostupnost bude kontrolovat.
		/// </summary>
		public IEnumerable<int> BookingResourceIds
		{
			get;
		}

		/// <summary>
		/// Vrací počátek intervalu od kterého volná místa chci.
		/// </summary>
		public double From => this.IntervalStart;

		/// <summary>
		/// Vrací konec intervalu do kterého volné místa chci.
		/// </summary>
		public double To => this.IntervalEnd;
	}
}
