namespace Reservanto.ApiClient.Requests
{
	/// <summary>
	/// Požadavek, který je omezen na konkrétního zákazníka.
	/// </summary>
	internal class CustomerRequest : Request
	{
		/// <summary>
		/// Vytvoří nový dotaz, pro získání informací o konkrétním zákazníkovi.
		/// </summary>
		/// <param name="customerId">Id požadovaného zákazníka.</param>
		public CustomerRequest(int customerId)
		{
			this.CustomerId = customerId;
		}

		/// <summary>
		/// Vrací nebo natavuje id zákazníka.
		/// </summary>
		public int CustomerId
		{
			get;
			set;
		}
	}
}
