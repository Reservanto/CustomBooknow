using System;

namespace Reservanto.CustomBooknow.Code.ReservantoApiClient
{
	/// <summary>
	/// Výjimka, informující o špatně zadaném (chybějícím) komunikačním tokenu.
	/// </summary>
	public class InvalidLongTimeTokenException : Exception
	{
		public InvalidLongTimeTokenException()
			: base("Invalid LTT, you can refresh LTT on /Api/ConnectToAPI")
		{
			
		}
	}
}
