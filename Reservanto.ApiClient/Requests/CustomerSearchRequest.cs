using Reservanto.ApiClient.Public.Interface;

namespace Reservanto.ApiClient.Requests
{
	/// <summary>
	/// Požadavek pro vyhledávání v zákaznících.
	/// </summary>
	internal class CustomerSearchRequest : Request
	{
		/// <summary>
		/// Vytvoří nový požadavek, pro vyhledávání mezi zákazníky.
		/// </summary>
		/// <param name="customer">Interface, nesoucí data o zákazníkovi.</param>
		public CustomerSearchRequest(ICustomer customer)
		{
			this.NameSearch = customer.GetFullName();
			this.EmailSearch = customer.Email?.Trim();
			this.PhoneSearch = customer.Phone?.Trim().Replace(" ", string.Empty);
		}

		/// <summary>
		/// Text který se hledá ve jméně zákazníků.
		/// </summary>
		public string NameSearch
		{
			get;
			set;
		}

		/// <summary>
		/// Text který se hledá v emailu zákazníků.
		/// </summary>
		public string EmailSearch
		{
			get;
			set;
		}

		/// <summary>
		/// Text, který se hledá v telefonu zákazníků.
		/// </summary>
		public string PhoneSearch
		{
			get;
			set;
		}
	}
}
