namespace Reservanto.ApiClient.Responses
{
	/// <summary>
	/// Odpověď API na požadavek o vytvoření rezervace.
	/// </summary>
	public class BookingStatusResponse : Response
	{
		/// <summary>
		/// Vrací nebo nastavuje Id appointmentu nové rezervace.
		/// </summary>
		public int AppointmentId
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje Id zákazníka nové rezervace.
		/// </summary>
		public int CustomerId
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje stav vytvořené rezervace.
		/// </summary>
		public string Status
		{
			get;
			set;
		}
	}
}
