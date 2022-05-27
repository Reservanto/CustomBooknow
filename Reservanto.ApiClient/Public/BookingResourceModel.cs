using System.Collections.Generic;

namespace Reservanto.ApiClient.Public
{
	/// <summary>
	/// API model reprezentující zdroj.
	/// </summary>
	public class BookingResourceModel : ResponseResult, INameAndId
	{
		/// <summary>
		/// Vrací nebo nastavuje Id tohoto zdroje.
		/// </summary>
		public int Id
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje id provozovny, ke které se vytvoří zdroj.
		/// </summary>
		public int LocationId
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje jméno zdroje.
		/// </summary>
		public string Name
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje e-mail zdroje.
		/// </summary>
		public string Email
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje telefon zdroje.
		/// </summary>
		public string Phone
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastasvuje popisek zdroje.
		/// </summary>
		public string Description
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje minimální časový úsek rozdělení kalendáře pro tento zdroj.
		/// V minutách.
		/// </summary>
		public int MinTimeUnit
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastsavuje adresu k obrázku tohoto zdroje.
		/// </summary>
		public string ImageUrl
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje, zda tento zdroj má vlastní obrázek nastavený.
		/// </summary>
		public bool HasCustomImage
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje segmenty, ve kterých se zdroj vyskytuje.
		/// </summary>
		public List<int> SegmentIds
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje seznam služeb, které zdroj poskytuje.
		/// </summary>
		public List<int> BookingServiceIds
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje seznam štítků, které na sobě zdroj má.
		/// </summary>
		public List<int> TagIds
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje kapacitu zdroje (případně jeho dostupný počet).
		/// </summary>
		public int Capacity
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje datum a čas od kterého je možné tento zdroj rezervovat. Pokud není nastaveno, tak počátek není omezen.
		/// </summary>
		public double? AvailableFrom
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje datum a čas do kterého je možné tento zdroj rezervovat. Pokud není nastaveno, tak konec není omezen.
		/// </summary>
		public double? AvailableTo
		{
			get;
			set;
		}

		public override string ToString()
		{
			return $"{this.Name} (ID:{this.Id})";
		}
	}
}
