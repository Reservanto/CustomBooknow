using System.Collections.Generic;

namespace Reservanto.ApiClient.Public
{
	/// <summary>
	/// API model reprezentující rezervovanou službu.
	/// </summary>
	public class BookingServiceModel : ResponseResult, INameAndId
	{
		/// <summary>
		/// Vrací nebo nastavuje Id této služby.
		/// </summary>
		public int Id
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje název služby.
		/// </summary>
		public string Name
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje popis služby.
		/// </summary>
		public string Description
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje trvání služby.
		/// </summary>
		public int Duration
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje cenu služby.
		/// </summary>
		public decimal? Price
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje zaměření, ve kterém se služba nachází.
		/// </summary>
		public int SegmentId
		{
			get;
			set;
		}

		/// <summary>
		/// Sazba DPH pro tuto položku.
		/// Pokud je obchodník neplátcem nebo služba nemá cenu, tak je null.
		/// </summary>
		public decimal? VatRateNullable
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje název segmentu služby.
		/// </summary>
		public string SegmentName
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje seznam Id zdrojů, které tuto službu poskytují.
		/// </summary>
		public List<int> BookingResourceIds
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje seznam případných cenových hladin vztahujících se k této službě.
		/// </summary>
		public List<ItemPriceLevelModel> AvailablePriceLevels
		{
			get;
			set;
		}
	}
}
