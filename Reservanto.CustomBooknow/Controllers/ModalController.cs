using Microsoft.AspNetCore.Mvc;
using Reservanto.CustomBooknow.Code;
using Reservanto.CustomBooknow.Code.Enums;
using Reservanto.CustomBooknow.Code.ReservantoApiClient;
using Reservanto.CustomBooknow.Models;

namespace Reservanto.CustomBooknow.Controllers
{
	/// <summary>
	/// Kontroler obsluhující vstup do rezervačního formuláře.
	/// </summary>
	public class ModalController : BaseController
	{
		public ModalController(ReservantoApiConnector reservantoApi) : base(reservantoApi)
		{
		}

		/// <summary>
		/// Vrací úvodní stránku rezervačního formuláře, podle předaných parametrů.
		/// </summary>
		/// <param name="args">Parametry pro vstup do rezervačního formuláře.</param>
		public IActionResult Index(ModalArguments args)
		{
			// Získání dostupných zaměření.
			var availableSegments = ReservantoApi.GetSegments();

			var model = new BookingViewModel();

			// vstupní stránka do formuláře.
			model.CurrentPage = WebPageSelector.GetEntryPoint(availableSegments, args, model);

			return View(model);
		}

		/// <summary>
		/// Vrací url, pro stránku, která následuje po <paramref name="currentPage"/>.
		/// </summary>
		/// <param name="currentPage">Aktuální stránka, ne které se rezervační formulář nachází.</param>
		/// <param name="segmentType">Aktuální typ segmentu, se kterým rezervační formulář pracuje.</param>
		public IActionResult GetNextPageUrl(WebPage currentPage, SegmentType segmentType)
		{
			// Získání další stránky.
			var nextPage = WebPageSelector.GetNextPage(currentPage, segmentType);
			
			// Získání url.
			var url = nextPage.GetPageUrl(segmentType, Url);

			return Json(new 
			{ 
				url,
				nextPage = nextPage.ToString()
			});
		}
	}
}
