namespace Reservanto.ApiClient.Requests
{
	/// <summary>
	/// Požadavek, které je omezen na konkrétní událost.
	/// </summary>
	internal class AppointmentRequest : Request
	{
		/// <summary>
		/// Vytvoří nový dotaz, pro získání informací o konkrétní rezervace.
		/// </summary>
		/// <param name="appointmentId">Id požadované události.</param>
		public AppointmentRequest(int appointmentId)
		{
			this.AppointmentId = appointmentId;
		}

		/// <summary>
		/// Vrací nebo nastavuje id události.
		/// </summary>
		public int AppointmentId
		{ 
			get;
			set;
		}
	}
}
