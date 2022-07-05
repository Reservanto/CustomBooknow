using Reservanto.ApiClient.Public;

namespace Reservanto.CustomBooknow.Models.Dtos
{
	/// <summary>
	/// Datový objekt, nesoucí informace o rezervovaném zdroji.
	/// </summary>
	public class BookingResourceDto : BaseIdentifiedDto
	{
		/// <summary>
		/// Vytvoří nový datový objekt, reprezentující rezervovatelný zdroj.
		/// </summary>
		/// <param name="bookingResourceApiModel">Model z API reprezentující zdroj.</param>
		/// <param name="location">Model z API reprezentující provozovnu.</param>
		public BookingResourceDto(BookingResourceModel bookingResourceApiModel, LocationDto location) : base(bookingResourceApiModel)
		{
			this.Name = bookingResourceApiModel.Name;
			this.Location = location;
		}

		/// <summary>
		/// Vrací nebo nastavuje název zdroje.
		/// </summary>
		public string Name
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje v jaké provozovně tento zdroj pracuje.
		/// </summary>
		public LocationDto Location
		{
			get;
			set;
		}
	}
}
