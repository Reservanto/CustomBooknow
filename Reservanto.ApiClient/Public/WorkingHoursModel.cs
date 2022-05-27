using System;

namespace Reservanto.ApiClient.Public
{
	/// <summary>
	/// API model pro pracovní hodiny.
	/// </summary>
	public class WorkingHoursModel : ResponseResult
	{
		/// <summary>
		/// Vrací nebo nastavuje den v týdnu. 0-6 (0 je neděle)
		/// </summary>
		public DayOfWeek DayOfWeek
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje začátek pracovní doby - UnixTimeStamp.
		/// </summary>
		public double Start
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje konec pracovní doby - UnixTimeStamp.
		/// </summary>
		public double End
		{
			get;
			set;
		}
	}
}
