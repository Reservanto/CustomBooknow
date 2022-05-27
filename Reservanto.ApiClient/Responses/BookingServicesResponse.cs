using Reservanto.ApiClient.Public;
using System.Collections.Generic;

namespace Reservanto.ApiClient.Responses
{

	/// <summary>
	/// Odpověď na požadavek na všechny služby, které obchodník poskytuje.
	/// </summary>
	internal class BookingServicesResponse : Response
	{
		/// <summary>
		/// Vrací nebo nastavuje seznam služeb.
		/// </summary>
		public List<BookingServiceModel> BookingServices
		{
			get;
			set;
		}
	}
}
