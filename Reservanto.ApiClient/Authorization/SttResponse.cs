using Reservanto.ApiClient.Responses;

namespace Reservanto.ApiClient.Authorization
{
	/// <summary>
	/// Odpověď API na požadavek o zíkání Short Time Tokenu.
	/// </summary>
	internal class SttResponse : Response
	{
		/// <summary>
		/// Vrací nebo nastavuje získaný Short Time Token pro komunikaci.
		/// </summary>
		public string ShortTimeToken
		{
			get;
			set;
		}
	}
}
