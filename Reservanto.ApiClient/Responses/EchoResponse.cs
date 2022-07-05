namespace Reservanto.ApiClient.Responses
{
	/// <summary>
	/// Odpověď na požadavek typu "echo" (kontrola dostupnosti serveru).
	/// </summary>
	internal class EchoResponse : Response
	{
		/// <summary>
		/// Vrací nebo nastavuje obdržený text.
		/// </summary>
		public string ReceivedText
		{
			get;
			set;
		}
	}
}
