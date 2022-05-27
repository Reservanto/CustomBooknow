using System;

namespace Reservanto.CustomBooknow.Code
{
	/// <summary>
	/// Třída, obsahující extension metody.
	/// </summary>
	public static class Extensions
	{
		/// <summary>
		/// Zjistí počátek týdne, ve kterém se nachází <paramref name="dt"/>.
		/// </summary>
		/// <param name="dt">Aktuální datum a čas.</param>
		/// <param name="startOfWeek">Den, který značí počátek týdne.</param>
		/// <returns>Počátek aktuálního týdne.</returns>
		public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startOfWeek)
		{
			int diff = (7 + (dt.DayOfWeek - startOfWeek)) % 7;
			return dt.AddDays(-1 * diff).Date;
		}

		/// <summary>
		/// Konvertuje Unix Time Stamp na DateTime.
		/// </summary>
		/// <param name="unixTimeStamp">Unix Time Stamp, ze které vytvářím datum a čas.</param>
		/// <returns>Datum a čas, který vznikl z <paramref name="unixTimeStamp"/>.</returns>
		public static DateTime UnixTimeStampToDateTime(this double unixTimeStamp)
		{
			// Unix timestamp is seconds past epoch
			DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
			dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
			return dtDateTime;
		}

		/// <summary>
		/// Konvertuje DateTime na Unix Time Stamp.
		/// </summary>
		/// <param name="dateTime">Datum a čas, který se konvertuje na časové razítko.</param>
		/// <returns>Unix Time Stamp, vytvořenou z <paramref name="dateTime"/>.</returns>
		public static double ToUnixTimeStamp(this DateTime dateTime)
		{
			var old = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
			// UTC timestamp.
			return (dateTime.ToUniversalTime() - old).TotalSeconds;
		}

		/// <summary>
		/// Otočí matici (2d pole) o 90°.
		/// </summary>
		/// <typeparam name="T">Typ matice, která se otáčí.</typeparam>
		/// <param name="table">Matice, která se otáčí.</param>
		/// <returns><paramref name="table"/> otočenou o 90°</returns>
		public static T[,] Rotate<T>(this T[,] table)
		{
			// Zjistí veliksoti.
			int length0 = table.GetLength(0);
			int length1 = table.GetLength(1);

			// Vytvoří návratovou matici.
			var ret = new T[length1, length0];

			// A naplní matici.
			for (int i = 0; i < length0; i++)
			{
				for (int j = 0; j < length1; j++)
				{
					ret[j, i] = table[i, j];
				}
			}

			return ret;
		}
	}
}
