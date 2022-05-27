using System.Collections.Generic;

namespace Reservanto.ApiClient.Responses
{
	/// <summary>
	/// Odpověď na dotaz, který zjišťuje všechny možné počátky rezervací (volné místa).
	/// </summary>
	internal class FreeSpacesResponse : Response
	{
		/// <summary>
		/// Vrací nebo nastavuje seznam všech časů ve formátu UnixTimeStamp.
		/// </summary>
		public List<double> Starts
		{
			get;
			set;
		}
	}
}
