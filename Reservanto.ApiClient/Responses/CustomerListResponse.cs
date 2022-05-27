using Reservanto.ApiClient.Public;
using System.Collections.Generic;

namespace Reservanto.ApiClient.Responses
{
	/// <summary>
	/// Odpověď např. na dotaz pro vyhledávání v zákazních, obsahující kolekci zákazníků.
	/// </summary>
	internal class CustomerListResponse : Response
	{
		/// <summary>
		/// Vrací nebo nastavuje seznam vrácených zákazníků.
		/// </summary>
		public List<CustomerModel> Customers
		{
			get;
			set;
		}
	}
}
