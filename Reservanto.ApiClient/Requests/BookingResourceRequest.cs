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
		/// <param name="bookingReosurceId">Id zdroje, který se má získat.</param>
		public BookingResourceRequest(int bookingReosurceId)
		{
			this.BookingResourceId = bookingReosurceId;
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
