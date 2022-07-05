using Reservanto.ApiClient.Public;

namespace Reservanto.ApiClient.Responses
{
	/// <summary>
	/// Odpověď na požadavek na získání konkrétních pracovních hodin.
	/// </summary>
	internal class WorkingHoursResponse : ListResponse<WorkingHoursModel>
	{
		/// <summary>
		/// Vrací nebo nastavuje zdroj, pro který byly vráceny pracovní hodiny.
		/// </summary>
		public int? BookingResourceId
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje provozovnu, pro kterou byly vráceny pracovní hodiny.
		/// </summary>
		public int? LocationId
		{
			get;
			set;
		}
	}
}
