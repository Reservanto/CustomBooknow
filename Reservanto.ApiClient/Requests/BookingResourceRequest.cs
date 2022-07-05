namespace Reservanto.ApiClient.Requests
{
	/// <summary>
	/// Request, který je omezen na konkrétní zdroj.
	/// </summary>
	internal class BookingResourceRequest : Request
	{
		/// <summary>
		/// Vytvoří nový dotaz, omezený na konkrétní zdroj.
		/// </summary>
		/// <param name="bookingResourceId">Id zdroje, který se má získat.</param>
		public BookingResourceRequest(int bookingResourceId)
		{
			this.BookingResourceId = bookingResourceId;
		}

		/// <summary>
		/// Vrací nebo nastavuje identifikátor zdroje.
		/// </summary>
		public int BookingResourceId
		{
			get;
			set;
		}
	}
}
