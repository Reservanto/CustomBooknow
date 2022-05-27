using Reservanto.CustomBooknow.Code;
using Reservanto.CustomBooknow.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Reservanto.CustomBooknow.Models
{
	/// <summary>
	/// ViewModel pro zobrazení výběru v kalendáři.
	/// </summary>
	public class CalendarViewModel
	{
		/// <summary>
		/// Vytvoří novou instanci modelu pro zobrazení kalendáře.
		/// </summary>
		/// <param name="slotSize">Velikost (v minutách) jedné buňky v kalendáři.</param>
		/// <exception cref="ArgumentException">Pokud <paramref name="slotSize"/> je <= 0.</exception>
		public CalendarViewModel(int slotSize)
		{
			if (slotSize <= 0)
				throw new ArgumentException("slotSize must be bigger than zero!");

			this.SlotSize = slotSize;
		}

		/// <summary>
		/// Vrací velkost jedné buňky v kalendáři.
		/// </summary>
		public int SlotSize
		{
			get;
		}

		/// <summary>
		/// Vrací nebo nastavuje pondělí, jímž začíná týden.
		/// </summary>
		public DateTime Monday
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje buňky kalendáře.
		/// </summary>
		public CalendarTableCell[,] Cells
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje všechny volné časy.
		/// </summary>
		public List<DateTime> FreeTimes
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje pracovní hodiny.
		/// </summary>
		public ICollection<WorkingHourDto> WorkingHours
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje rezervovatelné události.
		/// </summary>
		public ICollection<ClassesAppointmentDto> AvailableAppointments
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací čas zavírací doby, která se za <see cref="WorkingHours"/> největší.
		/// </summary>
		public TimeSpan DaysEnd
		{
			get
			{
				return this.WorkingHours.Count > 0
					? this.WorkingHours.Max(wh => wh.To.TimeOfDay)
					: TimeSpan.FromHours(17);
			}
		}

		/// <summary>
		/// Vrací čas otevírací doby, která se za <see cref="WorkingHours"/> nejmenší..
		/// </summary>
		public TimeSpan DaysStart
		{
			get
			{
				return this.WorkingHours.Count > 0
					? this.WorkingHours.Min(wh => wh.From.TimeOfDay)
					: TimeSpan.FromHours(9);
			}
		}

		/// <summary>
		/// Vrací počet časových buňek, které jsou celkově dostupné pro rozmezí <see cref="DaysStart"/> až <see cref="DaysEnd"/>.
		/// </summary>
		public int DayIntervalCount
		{
			get
			{
				return (int)((this.DaysEnd - this.DaysStart).Ticks / TimeSpan.FromMinutes(this.SlotSize).Ticks);
			}
		}

		/// <summary>
		/// Vytvoří samotný kalendář, ve kterém lze vybrat dostupné termíny.
		/// </summary>
		public void CreateTable(bool useHorizontal = false)
		{
			var dayCount = 7;
			var now = DateTime.Now;

			this.Cells = new CalendarTableCell[7, this.DayIntervalCount];

			// procházím všechny dny
			for (int i = 0; i < dayCount; i++)
			{
				DateTime thisDay = this.Monday.AddDays(i).Date;
				TimeSpan time = this.DaysStart;
				TimeSpan timeMinutes = TimeSpan.FromMinutes(this.SlotSize);
				var hours = this.WorkingHours.Where(wh => wh.From.Date == thisDay);

				// všechny časy
				for (int j = 0; j < this.Cells.GetLength(1); time = time.Add(timeMinutes), j++)
				{
					var currentDateTime = thisDay.Add(time);
					var currentCell = new CalendarTableCell(currentDateTime);

					if (this.FreeTimes != null && currentDateTime >= now && hours.Any(wh => wh.From.TimeOfDay <= time && wh.To.TimeOfDay >= time))
					{
						// Zarezervovaná je, pokud ji nenajdu v kolikci s volnými časy.
						currentCell.IsBooked = !this.FreeTimes.Any(f => f == currentDateTime);
					}
					else
					{
						currentCell.IsAvailable = false;
					}

					this.Cells[i, j] = currentCell;
				}
			}

			// Nemám používat horizontální - otočím.
			if (!useHorizontal)
			{
				this.Cells = this.Cells.Rotate();
			}
		}
	}

	/// <summary>
	/// Model, reprezentující jednu buňku kalendáře.
	/// </summary>
	public class CalendarTableCell
	{
		public CalendarTableCell(DateTime time)
		{
			this.Time = time;
			this.IsAvailable = true;
		}

		/// <summary>
		/// Vrací nebo nastavuje čas, který buňka reprezentuje.
		/// </summary>
		public DateTime Time
		{
			get;
		}

		/// <summary>
		/// Vrací nebo nastavuje, zda-li je buňka dostupná.
		/// </summary>
		public bool IsAvailable
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje zda-li je buňka již zarezervovaná.
		/// </summary>
		public bool IsBooked
		{
			get;
			set;
		}
	}
}
