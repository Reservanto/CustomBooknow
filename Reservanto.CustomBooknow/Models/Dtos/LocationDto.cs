using Reservanto.ApiClient.Public;

namespace Reservanto.CustomBooknow.Models.Dtos
{
	/// <summary>
	/// Datový objekt nesoucí informace o provozovně.
	/// </summary>
	public class LocationDto : BaseIdentifiedDto
	{
		/// <summary>
		/// Vytvoří nový datový model, reprezentující provozovnu.
		/// </summary>
		/// <param name="locationModel">Model z API reprezentující provozovnu.</param>
		public LocationDto(LocationModel locationModel) : base(locationModel)
		{
			this.Name = locationModel.Name;
		}

		/// <summary>
		/// Vrací nebo nastavuje název provozovny.
		/// </summary>
		public string Name
		{
			get;
			set;
		}
	}
}
