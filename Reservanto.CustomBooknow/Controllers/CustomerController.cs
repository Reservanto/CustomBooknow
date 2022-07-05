using Microsoft.AspNetCore.Mvc;
using Reservanto.CustomBooknow.Code;
using Reservanto.CustomBooknow.Localization;
using Reservanto.CustomBooknow.Models;

namespace Reservanto.CustomBooknow.Controllers
{
	/// <summary>
	/// Kontroler, pro práci s údaji o rezervované osobě.
	/// </summary>
	public class CustomerController : Controller
	{
		/// <summary>
		/// Akce, pro zadání informací o rezervované osobě.
		/// </summary>
		/// <param name="model">Model, obsahující formulářové hodonty.</param>
		public IActionResult Details(BookingViewModel model)
		{
			// Nevybral jsem žádnou službu -> chyba.
			if (!model.IsAppointmentOrTimeSelected)
				return BadRequest(Resources.AppointmentMustBeSelected); // Mohu vrátit 400 - formulář to zobrazí jako validační hlášku.

			// Zjistím další stránku.
			model.NextPage = WebPageSelector.GetNextPage(model.CurrentPage, model.SegmentType);

			return PartialView("_Details", model);
		}
	}
}
