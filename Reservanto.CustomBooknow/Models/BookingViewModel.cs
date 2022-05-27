using Reservanto.ApiClient.Public.Interface;
using Reservanto.CustomBooknow.Code.Enums;
using Reservanto.CustomBooknow.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Reservanto.CustomBooknow.Models
{
	/// <summary>
	/// Model, starající se o celou rezervaci.
	/// </summary>
	public class BookingViewModel : ICustomer, IBooking
	{
		#region Formulářové prvky

		/// <summary>
		/// Vrací nebo nastavuje 
		/// </summary>
		public int SegmentId
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje typ aměření.
		/// </summary>
		public SegmentType SegmentType
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje vybranou službu.
		/// </summary>
		public int BookingServiceId
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje délku rezervované služby.
		/// </summary>
		public int BookingServiceLength
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje vybraný zdroj.
		/// </summary>
		public int BookingResourceId
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje id provozovny.
		/// </summary>
		public int LocationId
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje id již existující události, na kterou se rezervuji.
		/// </summary>
		public int AppointmentId
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje počátek rezervace.
		/// </summary>
		public DateTime BookingTime
		{
			get;
			set;
		}
		
		/// <summary>
		/// Vrací nebo nastavuje error, který nastal ve formuláři.
		/// </summary>
		public string Error 
		{ 
			get;
			set;
		}

		#region Zákazník

		/// <summary>
		/// Vrací nebo nastavuje id zákazníka, který si rezervaci vytváří.
		/// </summary>
		public int CustomerId
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje křestní jméno zákazníka.
		/// </summary>
		public string CustomerFirstName
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje přijmění zákazníka.
		/// </summary>
		public string CustomerLastName
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje e-mailový kontakt na zákazníka.
		/// </summary>
		public string CustomerEmail
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje telefonní kontakt na zákazníka.
		/// </summary>
		public string CustomerPhone
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje poznámku zákazníka k rezervaci.
		/// </summary>
		public string CustomerNote
		{
			get;
			set;
		}

		#endregion

		#endregion

		#region Informace o stránkách

		/// <summary>
		/// Vrací nebo nastavuje předhozí stránku formuláře, pokud je null, znamená to, že předcházející stránka neexistuje.
		/// </summary>
		public WebPage? PrevPage
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací, zda-li existuje předchozí stránka.
		/// </summary>
		public bool HasPrevPage => this.PrevPage.HasValue;

		/// <summary>
		/// Vrací nebo nastavuje aktuální stránku formuláře.
		/// </summary>
		public WebPage CurrentPage
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje následující stránku formuláře, pokud je null, znamená to že následující stránka neexistuje (např. zavření formuláře).
		/// </summary>
		public WebPage? NextPage
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací, zda-li existuje následující stránka.
		/// </summary>
		public bool HasNextPage => this.NextPage.HasValue;

		#endregion

		/// <summary>
		/// Vrací nebo nastavuje všechny dostupné segmenty.
		/// </summary>
		public ICollection<SegmentDto> Segments
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje všechny dostupné provozovny.
		/// </summary>
		public ICollection<LocationDto> Locations
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje všechny zdroje, které je možno vybrat.
		/// </summary>
		public Dictionary<LocationDto, List<BookingResourceDto>> BookingResources
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje služby, které poskytuje vybraný zdroj.
		/// </summary>
		public ICollection<BookingServiceDto> BookingServices
		{ 
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje shrnutí rezervace.
		/// </summary>
		public BookingSummaryDto Summary
		{
			get;
			set;
		}

		#region Validační metody

		/// <summary>
		/// Vrací jestli je vybrána rezervovaná službu a zdroj.
		/// </summary>
		public bool IsServiceAndResourceSelected => this.BookingResourceId > 0 && this.BookingServiceId > 0;

		/// <summary>
		/// Vrací jestli je vybrána rezervovaná událost.
		/// </summary>
		public bool IsAppointmentOrTimeSelected => this.AppointmentId > 0 || this.BookingTime > DateTime.Now;

		#endregion

		#region Implementace rozhraní ICustomer

		string ICustomer.FirstName => this.CustomerFirstName;
		
		string ICustomer.LastName => this.CustomerLastName;

		string ICustomer.Email => this.CustomerEmail;

		string ICustomer.Phone => this.CustomerPhone;

		#endregion

		#region Předvýběr

		public void PreSelectSegment()
		{
			if (this.SegmentId == 0)
			{
				var segment = this.Segments.First();

				// A předvyberu ho
				this.SegmentId = segment.Id;
				this.SegmentType = segment.SegmentType;
			}
		}

		public void PreSelectLocation()
		{
			if(this.LocationId == 0 && this.Locations.Count > 0)
			{
				this.LocationId = this.Locations.First().Id;
			}
		}

		public void PreSelectBookingResource()
		{
			if(this.BookingResourceId == 0 && BookingResources.Count > 0)
			{
				// Předvyberu první zdroj.
				this.BookingResourceId = this.BookingResources.First().Value.First().Id;
			}
		}

		public void PreSelectBookingService()
		{
			if (this.BookingServiceId == 0 && this.BookingServices.Count > 0)
			{
				var service = BookingServices.FirstOrDefault();

				if(service != null)
				{
					this.BookingServiceId = service.Id;
					this.BookingServiceLength = service.Duration;
				}
			}
		}

		#endregion

		#region Nepoužívané implementace

		public string Voucher => null;

		public int Count => 1;

		#endregion
	}
}
