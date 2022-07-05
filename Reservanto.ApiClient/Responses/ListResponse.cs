using System.Collections.Generic;
using Reservanto.ApiClient.Public;

namespace Reservanto.ApiClient.Responses
{
	/// <summary>
	/// Odpověď API pro získání seznamu objektů (např. seznam segmentů).
	/// </summary>
	internal class ListResponse<T> : Response where T : IResponseResult
	{
		/// <summary>
		/// Vrací nebo nastavuje všechny vrácení objekty.
		/// </summary>
		public List<T> Items
		{
			get;
			set;
		}
	}
}
