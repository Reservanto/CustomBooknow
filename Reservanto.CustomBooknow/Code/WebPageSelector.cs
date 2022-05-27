using Microsoft.AspNetCore.Mvc;
using Reservanto.CustomBooknow.Code.Enums;
using Reservanto.CustomBooknow.Controllers;
using Reservanto.CustomBooknow.Models;
using Reservanto.CustomBooknow.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Reservanto.CustomBooknow.Code
{
	/// <summary>
	/// Třída zapouzdřující rozhodování o posloupnosti stránek.
	/// </summary>
	public static class WebPageSelector
	{
		/// <summary>
		/// Získává první stránku rezervačního formuláře.
		/// </summary>
		/// <param name="segments">Všechna zaměření, která jsou dostupná.</param>
		/// <param name="arguments">Argumenty vstupu do rezervačního formuláře.</param>
		public static WebPage GetEntryPoint(ICollection<SegmentDto> segments, ModalArguments arguments, BookingViewModel model)
		{
			// Předání argumentů do modelu.
			SetArgumentsToModel(arguments, model);

			// Pokus o získání zaměření - první, kde se shoduje id.
			var segment = segments.FirstOrDefault(s => s.Id == model.SegmentId);

			// Pokud je zvoleno zaměření již v argumentech...
			if (segment != null || segments.Count == 1)
			{
				// Pokud není zaměření nastaveno (je splněna druhá část podmínky), nastaví se první.
				if (segment == null)
					segment = segments.First();

				// Předání informací o zaměření do modelu.
				model.SegmentId = segment.Id;
				model.SegmentType = segment.SegmentType;

				// Rozhodnutí o první stránce.
				switch (segment.SegmentType)
				{
					// Podle typu zaměření vrací správný krok.
					case SegmentType.OneToOne:
						return WebPage.BookingResourceAndServiceSelect;
					case SegmentType.Classes:
						return WebPage.LocationSelect;
				}
			}
			// Pokud je na výběr více zaměření -> přechod na stránku s výběrem.
			else if (segments.Count > 1)
			{
				return WebPage.SegmentSelect;
			}

			throw new Exception("Invalid segment");
		}

		/// <summary>
		/// Vrací stránku, která následuje aktuální stránce.
		/// </summary>
		public static WebPage GetNextPage(WebPage currentPage, SegmentType segmentType)
		{
			switch (currentPage)
			{
				// Po výběru zaměření se rozhoduje podle zvoleného zaměření.
				case WebPage.SegmentSelect:
					switch (segmentType)
					{
						case SegmentType.OneToOne:
							return WebPage.BookingResourceAndServiceSelect;
						case SegmentType.Classes:
							return WebPage.LocationSelect;
						default:
							throw new Exception($"Unknown segment type {segmentType}");
					}
				// Po výběru provozovny nebo služby následuje kalendář.
				case WebPage.LocationSelect:
				case WebPage.BookingResourceAndServiceSelect:
					return WebPage.Calendar;
				// Po kalendáři se zadávají vždy kontaktní údaje.
				case WebPage.Calendar:
					return WebPage.CustomerDetails;
				// A po kontaktních údajích se jde na založení rezervace a shrnutí.
				case WebPage.CustomerDetails:
					return WebPage.Finish;
				default:
					throw new Exception($"Unknown web page {currentPage}");
			}
		}

		/// <summary>
		/// Vrací stránku, která předchází aktuální stránce.
		/// </summary>
		public static void GetPrevPage()
		{
		}

		/// <summary>
		/// Vrací url k přesměrování na konkrétní stránku formuláře a plně respekuje i zaměření.
		/// </summary>
		/// <param name="currentPage">Stránka formuláře z výčtu <see cref="WebPage"/>.</param>
		/// <param name="segmentType">Typ zaměření.</param>
		public static string GetPageUrl(this WebPage? currentPage, SegmentType segmentType, IUrlHelper urlHelper)
		{
			if (currentPage == null)
				return string.Empty;

			return currentPage.Value.GetPageUrl(segmentType, urlHelper);
		}

		/// <summary>
		/// Vrací url k přesměrování na konkrétní stránku formuláře a plně respekuje i zaměření.
		/// </summary>
		/// <param name="currentPage">Stránka formuláře z výčtu <see cref="WebPage"/>.</param>
		/// <param name="segmentType">Typ zaměření.</param>
		public static string GetPageUrl(this WebPage currentPage, SegmentType segmentType, IUrlHelper urlHelper)
		{
			string url;

			switch (currentPage)
			{
				case WebPage.SegmentSelect:
					url = urlHelper.Action(nameof(SegmentController.Select), nameof(SegmentController).ToController());
					break;
				case WebPage.LocationSelect:
					url = urlHelper.Action(nameof(LocationController.Select), nameof(LocationController).ToController());
					break;
				case WebPage.BookingResourceAndServiceSelect:
					url = urlHelper.Action(nameof(BookingResourceController.BookingServiceSelect), nameof(BookingResourceController).ToController());
					break;
				case WebPage.Calendar:
					switch (segmentType)
					{
						case SegmentType.Classes:
							url = urlHelper.Action(nameof(ClassesController.Calendar), nameof(ClassesController).ToController());
							break;
						case SegmentType.OneToOne:
							url = urlHelper.Action(nameof(OneToOneController.Calendar), nameof(OneToOneController).ToController());
							break;
						default:
							throw new Exception($"Unknown segment type {segmentType}");
					}
					break;
				case WebPage.CustomerDetails:
					url = urlHelper.Action(nameof(CustomerController.Details), nameof(CustomerController).ToController());
					break;
				case WebPage.Finish:
					switch (segmentType)
					{
						case SegmentType.Classes:
							url = urlHelper.Action(nameof(ClassesController.Finish), nameof(ClassesController).ToController());
							break;
						case SegmentType.OneToOne:
							url = urlHelper.Action(nameof(OneToOneController.Finish), nameof(OneToOneController).ToController());
							break;
						default:
							throw new Exception($"Unknown segment type {segmentType}");
					}
					break;
				default:
					throw new Exception($"Unknown web page {currentPage}");
			}

			return url;
		}

		#region helpers

		/// <summary>
		/// Vrací předaný text (<paramref name="controller"/>), ze kterého je odebráno slovo "Controller" - kvůli použití nameof i u kontrolerů.
		/// </summary>
		/// <param name="controller">Název kontroleru.</param>
		private static string ToController(this string controller)
		{
			return controller.Replace(nameof(Controller), string.Empty);
		}

		/// <summary>
		/// Nastavuje model argumenty, předanými do formuláře.
		/// </summary>
		/// <param name="args">Předané argumenty.</param>
		/// <param name="model">Model formuláře.</param>
		private static void SetArgumentsToModel(ModalArguments args, BookingViewModel model)
		{
			model.SegmentId = args.SegmentId;
		}

		#endregion
	}
}
