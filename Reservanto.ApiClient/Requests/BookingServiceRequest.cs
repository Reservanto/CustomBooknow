namespace Reservanto.ApiClient.Requests
{
	/// <summary>
	/// Požadavek, který je omezen na konkrétní službu. 
	/// </summary>
	internal class BookingServiceRequest : Request
	{
		/// <summary>
		/// Vytvoří nový dotaz, omezený na konkrétní službu.
		/// </summary>
		/// <param name="bookingServiceId">Služba, která se získává.</param>
		public BookingServiceRequest(int bookingServiceId)
		{
			this.BookingServiceId = bookingServiceId;
		}

		/// <summary>
		/// Vrací nebo nastavuje id provozovny.
		/// </summary>
		public int BookingServiceId
		{
			get;
			set;
		}
	}
}
