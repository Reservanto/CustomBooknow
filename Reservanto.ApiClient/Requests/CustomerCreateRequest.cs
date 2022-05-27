using Reservanto.ApiClient.Public.Interface;

namespace Reservanto.ApiClient.Requests
{
	/// <summary>
	/// Požadavek pro vytvoření zákazníka.
	/// </summary>
	internal class CustomerCreateRequest : Request
	{
		/// <summary>
		/// Vytvoří dotaz, pro založení nového zákazníka.
		/// </summary>
		/// <param name="customer">Interface, nesoucí data o novém zákazníkovi.</param>
		public CustomerCreateRequest(ICustomer customer)
		{
			this.FirstName = customer.FirstName?.Trim();
			this.LastName = customer.LastName?.Trim();
			this.Email = customer.Email?.Trim();
			this.Phone = customer.Phone?.Trim();
		}

		/// <summary>
		/// Vrací nebo nastavuje křestní jméno zákazníka.
		/// </summary>
		public string FirstName
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje přijmení zákazníka.
		/// </summary>
		public string LastName
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje email zákazníka.
		/// </summary>
		public string Email
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
	}
}
