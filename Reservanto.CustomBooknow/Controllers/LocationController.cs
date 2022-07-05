using Microsoft.AspNetCore.Mvc;
using Reservanto.CustomBooknow.Code;
using Reservanto.CustomBooknow.Code.ReservantoApiClient;
using Reservanto.CustomBooknow.Models;

namespace Reservanto.CustomBooknow.Controllers
{
	/// <summary>
	/// Kontroler starající se o práci s provozovnami.
	/// </summary>
	public class LocationController : BaseController
	{
		public LocationController(ReservantoApiConnector reservantoApi) : base(reservantoApi)
		{
		}

		/// <summary>
		/// Akce sloužící pro výběr provozovny.
		/// </summary>
		/// <param name="model">Model, obsahující formulářové hodnoty.</param>
		public IActionResult Select(BookingViewModel model)
		{
			// Získám dostupné provozovny.
			model.Locations = ReservantoApi.GetLocations();

			// Předvyberu provozovnu.
			model.PreSelectLocation();

			// Zajistím že mám další a předchozí stránku.
			model.NextPage = WebPageSelector.GetNextPage(model.CurrentPage, model.SegmentType);

			// A vrátím pohled.
			return PartialView("_Select", model);
		}
	}
}
