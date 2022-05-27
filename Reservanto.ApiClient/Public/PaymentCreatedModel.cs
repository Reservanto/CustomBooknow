namespace Reservanto.ApiClient.Public
{
	/// <summary>
	/// Model API, informující o vytvořené platbě k rezervaci.
	/// </summary>
	public class PaymentCreatedModel : ResponseResult
	{
		/// <summary>
		/// Vrací nebo nastavuje identifikátor rezervace.
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
		/// Vrací nebo natavuje identifikátor platby.
		/// </summary>
		public int PaymentId
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje částku, která bude zaplacena.
		/// </summary>
		public decimal Amount
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje metodu, která bude použita.
		/// </summary>
		public int PaymentMethodId
		{
			get;
			set;
		}
	}
}
