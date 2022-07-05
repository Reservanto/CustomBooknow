using Reservanto.ApiClient.Responses;

namespace Reservanto.ApiClient.Authorization
{
	/// <summary>
	/// Odpověď API na požadavek o získání Long Time Tokenu.
	/// </summary>
	internal class LttResponse : Response
	{
		/// <summary>
		/// Vrací nebo nastavuje jestli ověření API bylo přijato obchodníkem.
		/// </summary>
		public bool Accepted
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje Long Time Token (hodnotu má pouze pokud <c><see cref="LttResponse.Accepted"/> = true</c>.
		/// </summary>
		public string Token
		{
			get;
			set;
		}
	}
}
