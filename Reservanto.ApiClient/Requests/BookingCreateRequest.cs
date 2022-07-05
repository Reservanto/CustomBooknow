using Reservanto.ApiClient.Public.Interface;

namespace Reservanto.ApiClient.Requests
{
	/// <summary>
	/// Dotaz, pro vytvoření rezervace pro zaměření typu one to one.
	/// </summary>
	internal class BookingCreateRequest : Request
	{
		/// <summary>
		/// Vytvoří nový dotaz, pro založení rezervace.
		/// </summary>
		/// <param name="booking">Interface, nesoucí informace potřebné k vytvoření rezervace.</param>
		public BookingCreateRequest(IBooking booking)
		{
			this.BookingResourceId = booking.BookingResourceId;
			this.BookingServiceId = booking.BookingServiceId;
			this.CustomerId = booking.CustomerId;
			this.BookingStart = ToUnixTimeStamp(booking.BookingTime);
			this.Voucher = booking.Voucher;
			this.CustomerNote = booking.CustomerNote;
		}

		/// <summary>
		/// Vrací nebo nastavuje identifikátor rezervovaného zdroje.
		/// </summary>
		public int BookingResourceId
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje identifikátor rezervované služby.
		/// </summary>
		public int BookingServiceId
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje identifikátor zákazníka, který se rezervuje.
		/// </summary>
		public int CustomerId
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje počátek rezervace (UnixTimeStamp).
		/// </summary>
		public double BookingStart
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje poznámku k rezervaci.
		/// </summary>
		public string CustomerNote
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje kód voucheru, který je použit pro uhrazení rezervace.
		/// </summary>
		public string Voucher
		{
			get;
			set;
		}
	}
}
