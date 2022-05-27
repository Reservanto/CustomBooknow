using Reservanto.ApiClient.Public.Interface;

namespace Reservanto.ApiClient.Requests
{
	/// <summary>
	/// Request pro vytvoření rezervace pro zaměření typu classes.
	/// </summary>
	internal class ClassesBookingCreateRequest : Request
	{
		/// <summary>
		/// Vytvoří nový dotaz, pro založení rezervace na skupinovou událost.
		/// </summary>
		/// <param name="booking">Interface, nesoucí potřebné údaje pro založení rezervace.</param>
		public ClassesBookingCreateRequest(IBooking booking)
		{
			this.AppointmentId = booking.AppointmentId;
			this.CustomerId = booking.CustomerId;
			this.Count = booking.Count;
			this.Voucher = booking.Voucher;
			this.CustomerNote = booking.CustomerNote;
		}

		/// <summary>
		/// Vrací nebo nastavuje identifikátor události.
		/// </summary>
		public int AppointmentId
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje identifikátor zákazníka.
		/// </summary>
		public int CustomerId
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje, pro kolik osob se rezervace má vytvořit.
		/// </summary>
		public int Count
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
		/// Vrací nebo nastavuje kód oucheru, který je použit pro uhrazení rezervace.
		/// </summary>
		public string Voucher
		{
			get;
			set;
		}
	}
}
