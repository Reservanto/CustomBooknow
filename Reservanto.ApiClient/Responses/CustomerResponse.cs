using Reservanto.ApiClient.Public;

namespace Reservanto.ApiClient.Responses
{
	/// <summary>
	/// Odpověď např. na dotaz pro získání konkrétního zákazníka.
	/// </summary>
	internal class CustomerResponse : Response
	{
		/// <summary>
		/// Vrací nebo nastavuje nalezeného zákazníka.
		/// </summary>
		public CustomerModel Customer
		{
			get;
			set;
		}
	}
}
