namespace Reservanto.ApiClient.Public
{
	/// <summary>
	/// Model nesoucí informace o skupinové lekci.
	/// </summary>
	public class ClassAppointmentModel : ResponseResult
	{
		/// <summary>
		/// Vrací nebo nastavuje Id skupinové lekce.
		/// </summary>
		public int Id
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje začátek hodina (UnixTimeStamp).
		/// </summary>
		public double StartDate
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje konec hodina (UnixTimeStamp).
		/// </summary>
		public double EndDate
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje id kurzu, kterému tato hodina patří.
		/// </summary>
		public int? CourseId
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje název kurzu, kterému tato hodina patří.
		/// </summary>
		public string CourseName
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje id zdroje, kterému tato hodina patří.
		/// </summary>
		public int BookingResourceId
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje název zdroje, kterému tato hodina patří.
		/// </summary>
		public string BookingResourceName
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje id služby, na kterou tato hodina je.
		/// </summary>
		public int BookingServiceId
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje název služby, na kterou tato hodina je.
		/// </summary>
		public string BookingServiceName
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje id kalendáře, ve kterém je hodina umístěna.
		/// </summary>
		public int CalendarId
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje název kalendáře, ve kterém je hodina umístěna.
		/// </summary>
		public string CalendarName
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje, zda je možné se na tuto hodinu rezervovat.
		/// </summary>
		public bool IsAvailable
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje kapacitu této hodiny.
		/// </summary>
		public int Capacity
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje kolik je obsazeno z <see cref="Capacity"/>.
		/// </summary>
		public int OccupiedCapacity
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje string popisující obsazení této hodiny (formát je customizovatelný obchodníkem).
		/// </summary>
		public string FormattedAvailability
		{
			get;
			set;
		}
	}
}
