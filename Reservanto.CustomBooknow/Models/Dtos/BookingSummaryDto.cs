using Reservanto.ApiClient.Public;
using Reservanto.CustomBooknow.Code;
using System;

namespace Reservanto.CustomBooknow.Models.Dtos
{
	/// <summary>
	/// Datový objekt, reprezentující shrnující informace o rezervaci.
	/// </summary>
	public class BookingSummaryDto
	{
		/// <summary>
		/// Vytvoří nový datový objekt, reprezentující shrnutí (informace po vytvoření) rezervace.
		/// </summary>
		/// <param name="event">Model z API reprezentující rezervaci.</param>
		/// <param name="bookingResource">Model z API reprezentující zdroj.</param>
		/// <param name="location">Model z API reprezentující provozovnu.</param>
		public BookingSummaryDto(EventModel @event, BookingResourceModel bookingResource, LocationModel location)
		{
			this.BookingServiceName = @event.ServiceName;
			this.BookingResourceName = bookingResource.Name;
			this.StartsAt = @event.StartDate.UnixTimeStampToDateTime();
			this.Duration = @event.EndDate.UnixTimeStampToDateTime() - @event.StartDate.UnixTimeStampToDateTime();
			this.Price = (decimal)@event.Price;
			this.IsPaid = @event.IsPaid;

			var address = location.Address;

			this.City = address.City;
			this.Street = address.Street;
			this.ZipCode = address.ZipCode;
		}

		/// <summary>
		/// Vrací nebo nastavuje název zarezervovaní služby.
		/// </summary>
		public string BookingServiceName
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje název zarezervovaného zdroje.
		/// </summary>
		public string BookingResourceName
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje začátek rezervace.
		/// </summary>
		public DateTime StartsAt
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje délku rezervace.
		/// </summary>
		public TimeSpan Duration
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje cenu rezervace.
		/// </summary>
		public decimal Price 
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje jestli je rezervace uhrazena.
		/// </summary>
		public bool IsPaid
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje adresu rezervace - město.
		/// </summary>
		public string City
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje adresu rezervace - ulice a čp.
		/// </summary>
		public string Street
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje adresu rezervace - psč.
		/// </summary>
		public string ZipCode
		{
			get;
			set;
		}
	}
}
