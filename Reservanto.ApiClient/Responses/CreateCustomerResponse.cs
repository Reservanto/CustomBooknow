using Reservanto.ApiClient.Public;

namespace Reservanto.ApiClient.Responses
{
	/// <summary>
	/// Odpověď API Reservanta na akci vytvoření zákazníka.
	/// </summary>
	public class CreateCustomerResponse : Response
	{
		/// <summary>
		/// Vrací nebo nastavuje nově vzniklého zákazníka, pokud nebyl (např. kvůli validačním chybám) vytvořen, je tato vlastnost null.
		/// </summary>
		public CustomerModel Customer
		{
			get;
			set;
		}
	}
}
