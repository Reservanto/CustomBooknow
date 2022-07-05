namespace Reservanto.ApiClient.Public.Interface
{
	/// <summary>
	/// Interface, nesoucí údaje o zákazníkovi.
	/// </summary>
	public interface ICustomer
	{
		/// <summary>
		/// Vrací křestní jméno zákazníka.
		/// </summary>
		string FirstName
		{
			get;
		}

		/// <summary>
		/// Vrací příjmení zákazníka.
		/// </summary>
		string LastName
		{
			get;
		}

		/// <summary>
		/// Vrací email zákazníka.
		/// </summary>
		string Email
		{
			get;
		}

		/// <summary>
		/// Vrací telefon zákazníka.
		/// </summary>
		string Phone
		{
			get;
		}
	}

	/// <summary>
	/// Třída s rozšiřujícími metodami pro interface <see cref="ICustomer"/>.
	/// </summary>
	public static class CustomerExtensions
	{
		/// <summary>
		/// Vrací celé jméno zákazníka, tj. jméno + příjmení.
		/// </summary>
		public static string GetFullName(this ICustomer customer)
		{
			return string.Format("{0} {1}", customer.FirstName?.Trim(), customer.LastName?.Trim()).Trim();
		}
	}
}
