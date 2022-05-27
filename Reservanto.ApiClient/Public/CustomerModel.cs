using System.Collections.Generic;

namespace Reservanto.ApiClient.Public
{
	/// <summary>
	/// Model API reprezentující zákazníka.
	/// </summary>
	public class CustomerModel : ResponseResult, INameAndId
	{
		/// <summary>
		/// Vrací nebo nastavuje Id tohoto zákazníka.
		/// </summary>
		public int Id
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje datum vytvoření zákazníka (UnixTimeStamp).
		/// </summary>
		public double CreatedAt
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje jméno zákazníka.
		/// </summary>
		public string Name
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje telefon zákazníka.
		/// </summary>
		public string Phone
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje e-mail zákazníka.
		/// </summary>
		public string Email
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje pole s kódem(kódy) médií, které identifikují zákazníka (věrnostní karta, apod.).
		/// </summary>
		public string MediasField
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje příznak, že je zákazník smazaný.
		/// </summary>
		public bool Deleted
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje seznam štítků, které na sobě zákazník má.
		/// </summary>
		public List<int> TagIds
		{
			get;
			set;
		}
	}
}
