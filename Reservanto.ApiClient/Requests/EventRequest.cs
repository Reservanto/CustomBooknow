namespace Reservanto.ApiClient.Requests
{
	/// <summary>
	/// Požadavek, které je omezen na konkrétní událost.
	/// </summary>
	internal class EventRequest : Request
	{
		/// <summary>
		/// Vytvoří nový dotaz, pro získání informací o konkrétní rezervace.
		/// </summary>
		/// <param name="appointmentId">Id požadované události, spolu s <paramref name="customerId"/> identifikuje rezervaci.</param>
		/// <param name="customerId">Id požadovaného zákazníka, spolu s <paramref name="appointmentId"/> identifikuje rezervaci.</param>
		public EventRequest(int appointmentId, int customerId)
		{
			this.AppointmentId = appointmentId;
			this.CustomerId = customerId;
		}

		/// <summary>
		/// Vrací nebo nastavuje id události.
		/// Spolu s <see cref="CustomerId"/> tvoří samotnou rezervaci.
		/// </summary>
		public int AppointmentId
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje id zákazníka.
		/// Spolu s <see cref="AppointmentId"/> tvoří samotnou rezervaci.
		/// </summary>
		public int CustomerId
		{
			get;
			set;
		}
	}
}
