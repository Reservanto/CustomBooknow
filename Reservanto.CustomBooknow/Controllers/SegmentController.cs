using Microsoft.AspNetCore.Mvc;
using Reservanto.CustomBooknow.Code;
using Reservanto.CustomBooknow.Code.ReservantoApiClient;
using Reservanto.CustomBooknow.Models;

namespace Reservanto.CustomBooknow.Controllers
{
	/// <summary>
	/// Kontroler starající se o výběr zaměření.
	/// </summary>
	public class SegmentController : BaseController
	{
		public SegmentController(ReservantoApiConnector reservantoApi) : base(reservantoApi)
		{
		}

		/// <summary>
		/// Akce sloužící pro výběr zaměření.
		/// </summary>
		/// <param name="model">Model, obsahující formulářové hodnoty.</param>
		public IActionResult Select(BookingViewModel model)
		{
			// Stáhnu segmenty pro výběr.
			model.Segments = ReservantoApi.GetSegments();

			// Vytáhnu si první segment
			model.PreSelectSegment();

			// A rovou zjistím i další stránku.
			model.NextPage = WebPageSelector.GetNextPage(model.CurrentPage, model.SegmentType);

			// Vrátím.
			return PartialView("_Select", model);
		}
	}
}
