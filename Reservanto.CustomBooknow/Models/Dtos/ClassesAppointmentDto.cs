using Reservanto.ApiClient.Public;
using Reservanto.CustomBooknow.Code;
using System;

namespace Reservanto.CustomBooknow.Models.Dtos
{
	/// <summary>
	/// Datový objekt, nesoucí informace o dostupné skupinové lekci.
	/// </summary>
	public class ClassesAppointmentDto : BaseIdentifiedDto
	{
		/// <summary>
		/// Vytvoří nový datový model, reprezentující skupinovou lekci.
		/// </summary>
		/// <param name="model">Model z API reprezentující skupinovou lekci.</param>
		public ClassesAppointmentDto(ClassAppointmentModel model)
		{
			this.Id = model.Id;
			this.StartDate = model.StartDate.UnixTimeStampToDateTime();
			this.EndDate = model.EndDate.UnixTimeStampToDateTime();
			this.BookingResourceName = model.BookingResourceName;
			this.BookingServiceName = model.BookingServiceName;
			this.IsAvailable = model.IsAvailable;
			this.Capacity = model.Capacity;
			this.OccupiedCapacity = model.OccupiedCapacity;
			this.FormattedAvailability = model.FormattedAvailability;
		}


		/// <summary>
		/// Vrací nebo nastavuje začátek hodina (UnixTimeStamp).
		/// </summary>
		public DateTime StartDate
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje konec hodina (UnixTimeStamp).
		/// </summary>
		public DateTime EndDate
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje název zdroje, kterému tato hodina patří.
		/// </summary>
		public string BookingResourceName
		{
			get;
			set;
		}


		/// <summary>
		/// Vrací nebo nastavuje název služby, na kterou tato hodina je.
		/// </summary>
		public string BookingServiceName
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje, zda je možné se na tuto hodinu rezervovat.
		/// </summary>
		public bool IsAvailable
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje kapacitu této hodiny.
		/// </summary>
		public int Capacity
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje kolik je obsazeno z <see cref="Capacity"/>.
		/// </summary>
		public int OccupiedCapacity
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje string popisující obsazení této hodiny (formát je customizovatelný obchodníkem).
		/// </summary>
		public string FormattedAvailability
		{
			get;
			set;
		}
	}
}
