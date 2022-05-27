using Microsoft.AspNetCore.Mvc;
using Reservanto.CustomBooknow.Code;
using Reservanto.CustomBooknow.Code.ReservantoApiClient;
using Reservanto.CustomBooknow.Localization;
using Reservanto.CustomBooknow.Models;
using System;

namespace Reservanto.CustomBooknow.Controllers
{
	/// <summary>
	/// Kontroler obsluhující akce, specifické pro daný typ zaměření - OneToOne - jeden zákazník v jeden čas.
	/// </summary>
	public class OneToOneController : BaseController
	{
		public OneToOneController(ReservantoApiConnector reservantoApi) : base(reservantoApi)
		{
		}

		/// <summary>
		/// Vrací kalendář pro výběr termínu - vybírá se jakýkoliv volný termín.
		/// </summary>
		/// <param name="model">Model, obsahující formulářové hodonty.</param>
		public IActionResult Calendar(BookingViewModel model)
		{
			// Není vybrána žádná služba -> chyba.
			if (!model.IsServiceAndResourceSelected)
				return BadRequest(Resources.BookingResourceAndServiceMustBeSelected); // Vrací se 400 - formulář to zobrazí jako vylidační hlášku.

			// Zjištění další stránky.
			model.NextPage = WebPageSelector.GetNextPage(model.CurrentPage, model.SegmentType);

			return PartialView("_Calendar", model);
		}

		/// <summary>
		/// Vrací kalendář pro daný zdroj.
		/// </summary>
		/// <param name="day">Libovolný den v kalendářním týdnu. Určuje se tím, který týden se má vrátit.</param>
		/// <param name="bookingResourceId">Id vybraného zdroje.</param>
		/// <param name="bookingServiceId">Id vybrané služby.</param>
		/// <param name="bookingServiceLength">Délka rezervované služby.</param>
		public IActionResult GetCalendarForWeek(double day, int bookingResourceId, int bookingServiceId, int bookingServiceLength)
		{
			var model = new CalendarViewModel(bookingServiceLength);

			// Získání pondělí.
			var monday = day.UnixTimeStampToDateTime().Date.StartOfWeek(DayOfWeek.Monday);
			model.Monday = monday;

			// Získání pracovní doby pro rezervovaný zdroj.
			model.WorkingHours = ReservantoApi.GetWorkingHoursForBookingResource(bookingResourceId, monday, monday.AddDays(7));

			// Získání volných bloků k výběry termínu rezervace.
			model.FreeTimes = ReservantoApi.GetFreeSpacesForResource(bookingResourceId, bookingServiceId, monday, monday.AddDays(7));

			// Vytvoření buněk kalendáře.
			model.CreateTable();

			return PartialView("_ActualCalendar", model);
		}

		/// <summary>
		/// Vytváří rezervaci - poslední akce.
		/// </summary>
		/// <param name="model">Model, obsahující formulářové hodnoty, na základě kterých se vytvoří rezervace.</param>
		public IActionResult Finish(BookingViewModel model)
		{
			// Další stránka už není.
			model.NextPage = null;

			// Vytvoření/získání zákazníka.
			var response = ReservantoApi.GetOrCreateCustomer(model);
			
			// Pokud nastala chyba -> návrat na stránku se zadáním údajů.
			if(response.IsError)
				return BadRequest(response.ErrorMessage);

			model.CustomerId = response.Customer.Id;

			// Vytvoření rezervace.
			var bookingResponse = ReservantoApi.CreateOneToOneBooking(model);

			// Nastala chyba -> návrat na výběr termínu.
			if (bookingResponse.IsError)
				return BadRequest(response.ErrorMessage);

			// Vytvoření shrnutí rezervace.
			model.Summary = ReservantoApi.GetBookingSummary(bookingResponse.AppointmentId, model.CustomerId);

			return PartialView("_Finish", model);
		}
	}
}
