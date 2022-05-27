using Microsoft.Extensions.Options;
using Reservanto.CustomBooknow.Models.Dtos;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Reservanto.CustomBooknow.Code.Enums;
using Reservanto.ApiClient;
using Reservanto.ApiClient.Public.Interface;
using Reservanto.ApiClient.Responses;
using System;

namespace Reservanto.CustomBooknow.Code.ReservantoApiClient
{
	/// <summary>
	/// Klient, komunikující s rozhraním API na straně Reservanta.
	/// </summary>
	public class ReservantoApiConnector
	{
		/// <summary>
		/// Vrací instanci Reservanto API, používanou pro komunikaci s Reservantem.
		/// </summary>
		private ReservantoApi ReservantoApi
		{
			get
			{
				// Long Time Token není -> vrací null.
				if (string.IsNullOrEmpty(this.configuration.Value.LongTimeToken))
					return null;

				// Jinak vytváří novou instanci Reservanto API.
				return new ReservantoApi(
					this.configuration.Value.ApiUrl,
					this.configuration.Value.ClientId,
					this.configuration.Value.LongTimeToken
				);
			}
		}

		/// <summary>
		/// Nastavení Reservanto API, načtené z appsettings.json.
		/// </summary>
		private readonly IOptions<ReservantoApiConfiguration> configuration;

		/// <summary>
		/// Konstruktor pouze z modelu.
		/// </summary>
		/// <param name="model"></param>
		public ReservantoApiConnector(IOptions<ReservantoApiConfiguration> configuration)
		{
			this.configuration = configuration;
		}

		/// <summary>
		/// Konstruktor z modelu a callbacku.
		/// </summary>
		/// <param name="queryString"></param>
		public void ParseResponse(NameValueCollection queryString)
		{
			ReservantoApiAuthorize.ParseCallback(queryString, out var ltt);
			this.configuration.Value.LongTimeToken = ltt;
		}

		/// <summary>
		/// Vrací adresu pro přesměrování na Reservanto API.
		/// </summary>
		public string CreateRedirectUrl(string redirectUrl)
		{
			var configValue = this.configuration.Value;

			return ReservantoApiAuthorize.GetRedirectUrl(
				configValue.ApiUrl,
				configValue.ClientId,
				redirectUrl,
				configValue.RequestedRights
			).ToString();
		}

		/// <summary>
		/// Zajišťuje validní LTT token.
		/// </summary>
		/// <exception cref="System.Exception">Nastává, pokud není LTT nastaven.</exception>
		private void EnsureLTTOrThrow()
		{
			if (string.IsNullOrEmpty(this.configuration.Value.LongTimeToken))
				throw new Exception("Invalid LTT, you can refresh LTT on /Api/Redirect");
		}

		#region Dotazy na API

		/// <summary>
		/// Vrací seznam všech zaměření od obchodníka, které zároveň rezervační formulář podporuje.
		/// </summary>
		/// <returns>Seznam podporovaných zaměření.</returns>
		public ICollection<SegmentDto> GetSegments()
		{
			this.EnsureLTTOrThrow();

			var response = this.ReservantoApi.Segment_GetList();

			if (response != null)
			{
				return response
					.Where(s => Enum.TryParse<SegmentType>(s.SegmentType, out _)) // pouze zaměření, která jsou podporována formulářem
					.Select(s => new SegmentDto(s))
					.ToList();
			}

			return new List<SegmentDto>();
		}

		/// <summary>
		/// Vrací seznam všech provozoven, které má daný obchodník.
		/// </summary>
		/// <returns>Seznam dostupných provozoven.</returns>
		public ICollection<LocationDto> GetLocations()
		{
			this.EnsureLTTOrThrow();

			var response = this.ReservantoApi.Location_GetList();

			if (response != null)
			{ 
				return response.Select(l => new LocationDto(l)).ToList();
			}

			return new List<LocationDto>();
		}

		/// <summary>
		/// Vrací seznam všech dostupných zdrojů, které daný obchodním má pro konkrétní zaměření.
		/// </summary>
		/// <param name="currentSegmentId">Zaměření, ze kterého se zdroje získávají.</param>
		/// <returns>Seznam všech zdrojů v daném zaměření.</returns>
		public ICollection<BookingResourceDto> GetBookingResources(int currentSegmentId)
		{
			this.EnsureLTTOrThrow();

			var response = this.ReservantoApi.BookingResource_GetList();

			if (response != null)
			{
				// Získání provozoven.
				var locations = GetLocations().ToDictionary(k => k.Id, v => v);

				// Vrací se seznam všech zdrojů, rovnou i naplněn provozovnami.
				return response
					.Where(br => br.SegmentIds.Contains(currentSegmentId) && br.BookingServiceIds.Count > 0) // Pouze ty ze stejného zaměření, kteří poskytují nějaké služby.
					.Select(br => new BookingResourceDto(br, locations[br.LocationId])).ToList();
			}

			return new List<BookingResourceDto>();
		}

		/// <summary>
		/// Vrací seznam všech služeb, které poskytuje daný zdroj v daném zaměření.
		/// </summary>
		/// <param name="bookingResourceId">Zdroj, jehož služby je potřeba získat.</param>
		/// <param name="currentSegmentId">Zameření, ve kterém jsou služby.</param>
		/// <returns>Seznam všech služeb, které daný zdroj poskytuje v daném zaměření.</returns>
		public ICollection<BookingServiceDto> GetBookingServicesForResource(int bookingResourceId, int currentSegmentId)
		{
			this.EnsureLTTOrThrow();

			var response = this.ReservantoApi.BookingService_GetForResource(bookingResourceId);

			if (response != null)
			{
				var locations = GetLocations().ToDictionary(k => k.Id, v => v);

				return response
					.Where(bs => bs.SegmentId == currentSegmentId) // Pouze ty ze stejného zaměření.
					.Select(bs => new BookingServiceDto(bs)).ToList();
			}

			return new List<BookingServiceDto>();
		}

		/// <summary>
		/// Vrací nebo vytváří zákazníka, který se rezervuje.
		/// </summary>
		/// <param name="customer">Objekt zákazníka.</param>
		/// <returns>Dohledaného nebo nově založeného zákazníka.</returns>
		public CreateCustomerResponse GetOrCreateCustomer(ICustomer customer)
		{
			this.EnsureLTTOrThrow();

			// Pokus o dohledání zákazníka.
			var search = this.ReservantoApi.Customer_Search(customer);

			// Pokud se něco dohledalo...
			if (search?.Count > 0)
			{
				// pak se projdou výsledky vyhledávání.
				foreach(var searchResult in search)
				{
					// Pokud se shoduje e-mail a výsledek obsahuje i telefonní číslo
					// (kontroluji takhle, kvůli konfliktu 123456789 vs +420123456789)
					if (searchResult.Email == customer.Email?.Trim() &&
						searchResult.Phone?.Contains(customer.Phone ?? string.Empty) == true)
					{
						// A vrátím odpověď se zákazníkem.
						return new CreateCustomerResponse() { Customer = searchResult };
					}
				}
			}

			// Jinak se založí nový.
			return this.ReservantoApi.Customer_Create(customer);
		}

		/// <summary>
		/// Vytváří rezervaci pro daného zákazníka v zameření typu OneToOne.
		/// </summary>
		/// <param name="booking">Rozhraní, nesoucí informace o vytvářené rezervaci.</param>
		/// <returns>Vrací identifikátory a status nově vytvořené rezervace.</returns>
		public BookingStatusResponse CreateOneToOneBooking(IBooking booking)
		{
			this.EnsureLTTOrThrow();

			return this.ReservantoApi.OneToOne_CreateBooking(booking);
		}

		/// <summary>
		/// Vytváří rezervaci pro daného zákazníka v zameření typu Classes.
		/// </summary>
		/// <param name="booking">Rozhraní, nesoucí informace o vytvářené rezervaci.</param>
		/// <returns>Vrací identifikátory a status nově vytvořené rezervace.</returns>
		public BookingStatusResponse CreateClassesBooking(IBooking booking)
		{
			this.EnsureLTTOrThrow();

			return this.ReservantoApi.Classes_CreateBooking(booking);
		}

		/// <summary>
		/// Vrací všechy možné termíny rezervací pro daný zdroj a službu v určitém časovém rozmezí.
		/// </summary>
		/// <param name="bookingResourceId">Id zdroje.</param>
		/// <param name="bookingServiceId">Id služby.</param>
		/// <param name="from">Počátek intervalu možných termínů.</param>
		/// <param name="to">Konec intervalu možných termínů.</param>
		/// <returns>Seznam všech možných časů, kdy rezervace může začínat.</returns>
		public List<DateTime> GetFreeSpacesForResource(int bookingResourceId, int bookingServiceId, DateTime from, DateTime to)
		{
			this.EnsureLTTOrThrow();

			var response = this.ReservantoApi.OneToOne_GetAvailableStarts(bookingResourceId, bookingServiceId, from, to);

			if (response != null)
			{
				return response.Select(s => s.UnixTimeStampToDateTime()).ToList();
			}

			return new List<DateTime>();
		}

		/// <summary>
		/// Vrací pracovní hodiny pro daný zdroj v určitém časovém rozmezí.
		/// </summary>
		/// <param name="bookingResourceId">Id zdroje.</param>
		/// <param name="from">Počátek intervalu načtení pracovních hodin.</param>
		/// <param name="to">Konec intervalu načtení pracovních hodin.</param>
		/// <returns>Seznam všech pracovních hodin v daném období.</returns>
		public ICollection<WorkingHourDto> GetWorkingHoursForBookingResource(int bookingResourceId, DateTime from, DateTime to)
		{
			this.EnsureLTTOrThrow();

			var response = this.ReservantoApi.WorkingHours_GetForPeriod(locationId: null, bookingResourceId, from, to);

			if (response != null)
			{
				return response.Select(wh => new WorkingHourDto(wh)).ToList();
			}

			return new List<WorkingHourDto>();
		}


		/// <summary>
		/// Vrací pracovní hodiny pro danou provozovnu v určitém časovém rozmezí.
		/// </summary>
		/// <param name="locationId">Id provozovny.</param>
		/// <param name="from">Počátek intervalu načtení pracovních hodin.</param>
		/// <param name="to">Konec intervalu načtení pracovních hodin.</param>
		/// <returns>Seznam všech pracovních hodin v daném období.</returns>
		public ICollection<WorkingHourDto> GetWorkingHoursForLocation(int locationId, DateTime from, DateTime to)
		{
			this.EnsureLTTOrThrow();

			var response = this.ReservantoApi.WorkingHours_GetForPeriod(locationId, bookingResourceId: null, from, to);

			if (response != null)
			{
				return response.Select(wh => new WorkingHourDto(wh)).ToList();
			}

			return new List<WorkingHourDto>();
		}

		/// <summary>
		/// Vrací všechy rezervovatelné události pro zaměření typu Classes.
		/// </summary>
		/// <param name="segmentId">Id zaměření.</param>
		/// <param name="locationId">Id provozovny.</param>
		/// <param name="from">Počátek intervalu načtení událostí.</param>
		/// <param name="to">Počátek intervalu načtení událostí.</param>
		/// <returns>Seznam všech rezervovatelných událostí.</returns>
		public ICollection<ClassesAppointmentDto> GetClassesAppointments(int segmentId, int locationId, DateTime from, DateTime to)
		{
			this.EnsureLTTOrThrow();

			var response = this.ReservantoApi.Classes_GetAvailableAppointments(segmentId, locationId, from, to);

			if (response != null)
			{
				return response.Select(a => new ClassesAppointmentDto(a)).ToList();
			}

			return new List<ClassesAppointmentDto>();
		}

		/// <summary>
		/// Vrací shrnutí požadované rezervace.
		/// </summary>
		/// <param name="appointmentId">Id požadované události, spolu s <paramref name="customerId"/> identifikuje rezervaci.</param>
		/// <param name="customerId">Id požadovaného zákazníka, spolu s <paramref name="appointmentId"/> identifikuje rezervaci.</param>
		/// <returns>Objekt s informacemi o rezervaci.</returns>
		public BookingSummaryDto GetBookingSummary(int appointmentId, int customerId)
		{
			this.EnsureLTTOrThrow();

			var @event = this.ReservantoApi.Event_Get(appointmentId, customerId);
			var resource = this.ReservantoApi.BookingResource_Get(@event.SourceId);
			var location = this.ReservantoApi.Location_Get(resource.LocationId);

			if(@event != null && resource != null && location != null)
			{
				return new BookingSummaryDto(@event, resource, location);
			}

			return null;
		}

		#endregion
	}
}
