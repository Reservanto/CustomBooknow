using System;

namespace Reservanto.ApiClient.Requests
{
	/// <summary>
	/// Základní dotaz, který je omezen časy od a do.
	/// </summary>
	internal class AvailabilityRequest : Request
	{
		/// <summary>
		/// Vytvoří nový dotaz, omezený datumy "od", "do". 
		/// </summary>
		/// <param name="from">Datum, od kterého se mají stahovat výsledky.</param>
		/// <param name="to">Datum, do kterého se mají stahovat výsledky.</param>
		public AvailabilityRequest(DateTime from, DateTime to)
		{
			this.IntervalStart = ToUnixTimeStamp(from);
			this.IntervalEnd = ToUnixTimeStamp(to);
		}

		/// <summary>
		/// Vrací nebo nastavuje počátek intervalu od kterého volná místa chci.
		/// </summary>
		public double IntervalStart
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje konec intervalu do kterého volné místa chci.
		/// </summary>
		public double IntervalEnd
		{
			get;
			set;
		}
	}
}
