using Reservanto.ApiClient.Public;
using Reservanto.CustomBooknow.Code;
using System;

namespace Reservanto.CustomBooknow.Models.Dtos
{
	/// <summary>
	/// Datový objekt nesoucí informace o pracovní době.
	/// </summary>
	public class WorkingHourDto
	{
		/// <summary>
		/// Vytvoří nový datový model, reprezentující pracovní hodiny.
		/// </summary>
		/// <param name="workingHoursApiModel">Model z API reprezentující pracovní hodiny.</param>
		public WorkingHourDto(WorkingHoursModel workingHoursApiModel)
		{
			this.Day = workingHoursApiModel.DayOfWeek;
			this.From = workingHoursApiModel.Start.UnixTimeStampToDateTime();
			this.To = workingHoursApiModel.End.UnixTimeStampToDateTime();
		}

		/// <summary>
		/// Vrací nebo nastavuje den na který se vztahuje tato otevírací doba.
		/// </summary>
		public DayOfWeek Day
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo natavuje počátek pracovní doby.
		/// </summary>
		public DateTime From
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje konec pracovní doby.
		/// </summary>
		public DateTime To
		{
			get;
			set;
		}
	}
}
