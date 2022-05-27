namespace Reservanto.ApiClient.Public
{
	/// <summary>
	/// API model, nesoucí informace o zaměření.
	/// </summary>
	public class SegmentModel : ResponseResult
	{
		/// <summary>
		/// Vrací nebo nastavuje Id zaměření.
		/// </summary>
		public int Id
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje typ zaměření.
		/// </summary>
		public string SegmentType
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje pojmenování segmentu v Reservantu.
		/// </summary>
		public string InternalName
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje lokalizovaný název zaměření.
		/// </summary>
		public string LocalizedName
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací vlastní název zaměření.
		/// </summary>
		public string CustomizedName
		{
			get;
			set;
		}
	}
}
