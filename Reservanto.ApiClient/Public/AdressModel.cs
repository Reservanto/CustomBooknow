namespace Reservanto.ApiClient.Public
{
	/// <summary>
	/// Model, nesoucí informace o adrese např. provozovny.
	/// </summary>
	public class AdressModel
	{
		/// <summary>
		/// Vrací nebo nastavuje Název (firmy)
		/// </summary>
		public string Name
		{
			get;
			set;
		}

		/// <summary>
		/// vrací nebo nastavuje ulici.
		/// </summary>
		public string Street
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje město.
		/// </summary>
		public string City
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje PSČ.
		/// </summary>
		public string ZipCode
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje stát (jen v USA).
		/// </summary>
		public string State
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje zemi.
		/// </summary>
		public string Country
		{
			get;
			set;
		}
	}
}
