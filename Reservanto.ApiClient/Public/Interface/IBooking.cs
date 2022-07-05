using System;

namespace Reservanto.ApiClient.Public.Interface
{
	/// <summary>
	/// Interface reprezentující rezervaci.
	/// </summary>
	public interface IBooking
	{
		/// <summary>
		/// Vrací id zákazníka, který se rezervuje.
		/// </summary>
		int CustomerId
		{
			get;
		}

		/// <summary>
		/// Vrací poznámku k rezervaci.
		/// </summary>
		string CustomerNote
		{
			get;
		}

		/// <summary>
		/// Vrací kód voucheru, který je použit pro uhrazení rezervace.
		/// </summary>
		string Voucher
		{
			get;
		}

		#region Zaměření typu classes

		/// <summary>
		/// Vrací identifikátor události, kam se zákazník rezervuje.
		/// </summary>
		int AppointmentId
		{
			get;
		}

		/// <summary>
		/// Vrací počet osob, pro které se rezervace vytváří.
		/// </summary>
		int Count
		{
			get;
		}

		#endregion

		#region Zaměření typu one to one

		/// <summary>
		/// Vrací identifikátor rezervovaného zdroje.
		/// </summary>
		int BookingResourceId
		{
			get;
		}

		/// <summary>
		/// Vrací identifikátor rezervované služby.
		/// </summary>
		int BookingServiceId
		{
			get;
		}

		/// <summary>
		/// Vrací počátek rezervace.
		/// </summary>
		DateTime BookingTime
		{
			get;
		}

		#endregion
	}
}