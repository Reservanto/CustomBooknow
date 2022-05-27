namespace Reservanto.ApiClient.Public
{
	/// <summary>
	/// Interface definující modely, které mají název a Id.
	/// </summary>
	public interface INameAndId
	{
		/// <summary>
		/// Vrací Id daného objektu.
		/// </summary>
		int Id
		{
			get;
		}

		/// <summary>
		/// Vrací název daného objektu.
		/// </summary>
		string Name
		{
			get;
		}
	}
}
