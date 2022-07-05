namespace Reservanto.ApiClient.Requests
{
	/// <summary>
	/// Požadavek, který je omezen na konkrétní provozovnu. 
	/// </summary>
	internal class LocationRequest : Request
	{
		/// <summary>
		/// Vytvoří nový dotaz, omezený na konkrétní provozovnu.
		/// </summary>
		/// <param name="locationId">Provozovna, která se získává.</param>
		public LocationRequest(int locationId)
		{
			this.LocationId = locationId;
		}

		/// <summary>
		/// Vrací nebo nastavuje id provozovny.
		/// </summary>
		public int LocationId
		{
			get;
			set;
		}
	}
}
