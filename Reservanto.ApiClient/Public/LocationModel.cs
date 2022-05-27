using System.Collections.Generic;

namespace Reservanto.ApiClient.Public
{
	/// <summary>
	/// Api model reprezentující provozovnu.
	/// </summary>
	public class LocationModel : ResponseResult, INameAndId
	{
		/// <summary>
		/// Vrací nebo nastavuje Id této provozovny.
		/// </summary>
		public int Id
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje název provozovny.
		/// </summary>
		public string Name
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje čas vytvoření.
		/// </summary>
		public double CreatedAt
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje čas úpravy.
		/// </summary>
		public double ModifiedAt
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje telefon na provozovnu.
		/// </summary>
		public string Phone
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje Adresu provozovny.
		/// </summary>
		public AdressModel Address
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje seznam pracovních hodin této provozovny.
		/// </summary>
		public List<WorkingHoursModel> WorkingHours
		{
			get;
			set;
		}
	}
}
