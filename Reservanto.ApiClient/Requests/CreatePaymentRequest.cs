namespace Reservanto.ApiClient.Requests
{
	/// <summary>
	/// Požadavek, pro vytvoření platby na danou rezervaci. 
	/// </summary>
	internal class CreatePaymentRequest : AppointmentRequest
	{
		/// <summary>
		/// Vytvoří nový dotaz, pro vytvoření platby za rezervaci.
		/// </summary>
		/// <param name="appointmentId">Id požadované události, spolu s <paramref name="customerId"/> identifikuje rezervaci.</param>
		/// <param name="customerId">Id požadovaného zákazníka, spolu s <paramref name="appointmentId"/> identifikuje rezervaci.</param>
		/// <param name="paymentMethodId">Id platební metody, se kterým se platba vytvoří.</param>
		public CreatePaymentRequest(int appointmentId, int customerId, int paymentMethodId) : base(appointmentId)
		{
			this.CustomerId = customerId;
			this.PaymentMethodId = paymentMethodId;
		}

		/// <summary>
		/// Vrací nebo nastavuje id zákazníka.
		/// Spolu s <see cref="AppointmentRequest.AppointmentId"/> tvoří samotnou rezervaci.
		/// </summary>
		public int CustomerId
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje id platební metody, která se použije pro vytvoření platby.
		/// </summary>
		public int PaymentMethodId
		{
			get;
			set;
		}
	}
}
