using Microsoft.AspNetCore.Mvc;
using Reservanto.CustomBooknow.Code;
using Reservanto.CustomBooknow.Code.ReservantoApiClient;
using Reservanto.CustomBooknow.Models;
using System;

namespace Reservanto.CustomBooknow.Controllers
{
	/// <summary>
	/// Kontroler obsluhující akce, specifické pro daný typ zaměření - one to one - jeden zákazník v jeden čas.
	/// </summary>
	public class ClassesController : BaseController
	{
		public ClassesController(ReservantoApiConnector reservantoApi) : base(reservantoApi)
		{
		}

		/// <summary>
		/// Vrací kalendář pro výběr termínu - vybírá se již existující lekce.
		/// </summary>
		/// <param name="model">Model, obsahující formulářové hodnoty.</param>
		public IActionResult Calendar(BookingViewModel model)
		{
			// Získám si další stránku.
			model.NextPage = WebPageSelector.GetNextPage(model.CurrentPage, model.SegmentType);

			// A vrátím view.
			return PartialView("_Calendar", model);
		}

		/// <summary>
		/// Vrací samotný kalendář, již pro danou provozovnu.
		/// </summary>
		/// <param name="day">Jakýkoliv den z týdnu, který chci zobrazit.</param>
		/// <param name="segmentId">Identifikátor zaměření, pro které vytvářím kalendář.</param>
		/// <param name="locationId">Identifikátor provozovny, pro kterou vytvářím kalendář.</param>
		public IActionResult GetCalendarForWeek(double day, int segmentId, int locationId)
		{
			var model = new CalendarViewModel(60);

			// Musím získat pondělí.
			var monday = day.UnixTimeStampToDateTime().Date.StartOfWeek(DayOfWeek.Monday);
			model.Monday = monday;

			// Získám pracovní hodiny.
			model.WorkingHours = ReservantoApi.GetWorkingHoursForLocation(locationId, monday, monday.AddDays(7));

			// Získám rezervovatelné události.
			model.AvailableAppointments = ReservantoApi.GetClassesAppointments(segmentId, locationId, monday, monday.AddDays(7));

			// Vytvořím tabulku.
			model.CreateTable(true);

			return PartialView("_ActualCalendar", model);
		}

		/// <summary>
		/// Samotné vytvoření rezervace - poslední akce.
		/// </summary>
		/// <param name="model">Model, obsahující formulářové hodnoty, na základě kterých se vytvoří rezervace.</param>
		public IActionResult Finish(BookingViewModel model) 
		{
			// Další stránka už není.
			model.NextPage = null;

			// Zkusím vytvořit zákoše.
			var response = ReservantoApi.GetOrCreateCustomer(model);

			// Pokud nastal error -> vracím na stránku se zadáním údajů.
			if (response.IsError)
				return BadRequest(response.ErrorMessage);

			model.CustomerId = response.Customer.Id;

			// Vytvořím rezervaci.
			var bookingResponse = ReservantoApi.CreateClassesBooking(model);

			// Nastal error -> vrátím na výběr termínu.
			if (bookingResponse.IsError)
				return BadRequest(response.ErrorMessage);

			// Vytvořím shrnutí rezervace.
			model.Summary = ReservantoApi.GetBookingSummary(bookingResponse.AppointmentId, model.CustomerId);

			return PartialView("_Finish", model);
		}
	}
}
