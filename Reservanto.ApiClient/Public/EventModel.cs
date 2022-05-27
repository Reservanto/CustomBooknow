namespace Reservanto.ApiClient.Public
{
	/// <summary>
	/// Model reprezentující konkrétní událost (rezervaci).
	/// </summary>
	public class EventModel : ResponseResult
	{
		/// <summary>
		/// Vrací nebo nastavuje Id termínu.
		/// </summary>
		public int AppointmentId
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje začátek rezervace (UnixTimeStamp).
		/// </summary>
		public double StartDate
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje konec rezervace (UnixTimeStamp).
		/// </summary>
		public double EndDate
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje poznámku od obchodníka.
		/// </summary>
		public string MerchantNote
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje název provozovny.
		/// </summary>
		public string LocationName
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje jméno zdroje. (Zdroj může být masér, lezecká stěna, nebo třeba sauna)
		/// </summary>
		public string SourceName
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje id zdroje.
		/// </summary>
		public int SourceId
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje název služby (nemusí být nastaveno).
		/// </summary>
		public string ServiceName
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje název oboru podnikání.
		/// </summary>
		public string SegmentName
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje jméno zákazníka.
		/// </summary>
		public string CustomerFullName
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje Id zákazníka (v rezervačním systému Reservanto).
		/// </summary>
		public int CustomerId
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje poznámku od zákazníka.
		/// </summary>
		public string CustomerNote
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje cenu této rezervace.
		/// </summary>
		public double Price
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje, zda je daná rezervace už zaplacená.
		/// </summary>
		public bool IsPaid
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje počet osob/počet kusů u rezervace.
		/// </summary>
		public int Count
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje stav přítomnosti zákazníka. (viz <seealso cref="MersiteSystems.Reservanto.Common.NoShowState"/>)
		/// </summary>
		public byte NoShowStatus
		{
			get;
			set;
		}
	}
}
