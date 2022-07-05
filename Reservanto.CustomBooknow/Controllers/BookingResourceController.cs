using Microsoft.AspNetCore.Mvc;
using Reservanto.CustomBooknow.Code;
using Reservanto.CustomBooknow.Code.ReservantoApiClient;
using Reservanto.CustomBooknow.Models;
using System.Linq;

namespace Reservanto.CustomBooknow.Controllers
{
	/// <summary>
	/// Kontroler, starající se o práci se zdroji.
	/// </summary>
	public class BookingResourceController : BaseController
	{
		public BookingResourceController(ReservantoApiConnector reservantoApi) : base(reservantoApi)
		{
		}

		/// <summary>
		/// Akce, sloužící pro výběr zdroje a služby.
		/// </summary>
		/// <param name="model">Model, obsahující formulářové hodnoty.</param>
		public IActionResult BookingServiceSelect(BookingViewModel model)
		{
			// Mohu připravit výběr zdroje a služby.
			var bookingResources = ReservantoApi.GetBookingResources(model.SegmentId);

			// Naplním model.
			model.BookingResources = bookingResources
				.GroupBy(br => br.Location.Id) // Spojím podle id.
				// A do slovníku si roztřídím tak, že klíčem mám název provozovny a hodnotou všechny zdroje.
				.ToDictionary(
					k => k.First().Location,
					v => v.ToList()
				);

			// Zajistím, že zdroj je vybraný.
			model.PreSelectBookingResource();

			// A doplním rovnou i služby.
			model.BookingServices = ReservantoApi.GetBookingServicesForResource(model.BookingResourceId, model.SegmentId);

			// Zajistím že služba je vybraná.
			model.PreSelectBookingService();

			// A zjistím si další stránku.
			model.NextPage = WebPageSelector.GetNextPage(model.CurrentPage, model.SegmentType);

			return PartialView("_BookingServiceSelect", model);
		}
	}
}
