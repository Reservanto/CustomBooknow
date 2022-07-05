using Microsoft.AspNetCore.Mvc;
using Reservanto.CustomBooknow.Code.ReservantoApiClient;
using Reservanto.CustomBooknow.Models;

namespace Reservanto.CustomBooknow.Controllers
{
	/// <summary>
	/// Kontroler, pro práci se službami.
	/// </summary>
	public class BookingServiceController : BaseController
	{
		public BookingServiceController(ReservantoApiConnector reservantoApi) : base(reservantoApi)
		{
		}

		/// <summary>
		/// Vrací všechny služby, který poskytuje daný zdroj.
		/// </summary>
		/// <param name="model">Model formuláře.</param>
		[HttpPost]
		public IActionResult Select(BookingViewModel model)
		{
			// Naplním služby.
			model.BookingServices = ReservantoApi.GetBookingServicesForResource(model.BookingResourceId, model.SegmentId);

			// A vrátím partial view s výběrem služby.
			return PartialView("_Select", model);
		}
	}
}
