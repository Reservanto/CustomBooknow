using Reservanto.ApiClient.Public;

namespace Reservanto.CustomBooknow.Models.Dtos
{
	/// <summary>
	/// Datový objekt, nesoucí informace o rezervované službě.
	/// </summary>
	public class BookingServiceDto : BaseIdentifiedDto
	{
		/// <summary>
		/// Vytvoří nový datový objekt, reprezentující rezervovatelnou službu.
		/// </summary>
		/// <param name="bookingServiceApiModel">Model z API reprezentující službu.</param>
		public BookingServiceDto(BookingServiceModel bookingServiceApiModel) : base(bookingServiceApiModel)
		{
			this.Name = bookingServiceApiModel.Name;
			this.Description = bookingServiceApiModel.Description;
			this.PriceWithVat = bookingServiceApiModel.Price ?? 0;
			this.Duration = bookingServiceApiModel.Duration;
		}

		/// <summary>
		/// Vrací nebo nastavuje název rezervované služby.
		/// </summary>
		public string Name
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje popisek rezervované služby.
		/// </summary>
		public string Description
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje cenu služby včetně DPH.
		/// </summary>
		public decimal PriceWithVat
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje délku služby.
		/// </summary>
		public int Duration
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje, kolik minut za rezervací musí být volných (tzv. pauza za rezervací).
		/// </summary>
		public int PaddingTime
		{
			get;
			set;
		}
	}
}
