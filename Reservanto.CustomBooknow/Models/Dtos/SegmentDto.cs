using Reservanto.ApiClient.Public;
using Reservanto.CustomBooknow.Code.Enums;
using System;

namespace Reservanto.CustomBooknow.Models.Dtos
{
	/// <summary>
	/// Datový objekt, nesoucí informace o zaměření.
	/// </summary>
	public class SegmentDto : BaseIdentifiedDto
	{
		/// <summary>
		/// Vytvoří nový datový model, reprezentující zaměření.
		/// </summary>
		/// <param name="segment">Model z API reprezentující zaměření.</param>
		public SegmentDto(SegmentModel segment)
		{
			this.Id = segment.Id;
			this.Name = segment.CustomizedName ?? segment.LocalizedName;

			Enum.TryParse<SegmentType>(segment.SegmentType, out var segmentType);
			this.SegmentType = segmentType;
		}

		/// <summary>
		/// Vrací nebo nastavuje název zaměření.
		/// </summary>
		public string Name
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje typ zaměření.
		/// </summary>
		public SegmentType SegmentType
		{
			get;
			set;
		}
	}
}
