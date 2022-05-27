namespace Reservanto.CustomBooknow.Code.Enums
{
	/// <summary>
	/// Výčet všech dostupných stránek ve formuláři.
	/// </summary>
	public enum WebPage
	{
		/// <summary>
		/// Výběr segmentu.
		/// </summary>
		SegmentSelect,

		/// <summary>
		/// Výběr provozovny.
		/// </summary>
		LocationSelect,

		/// <summary>
		/// Výběr zdroje a služby.
		/// </summary>
		BookingResourceAndServiceSelect,

		/// <summary>
		/// Výběř termínu.
		/// </summary>
		Calendar,

		/// <summary>
		/// Zadání platby.
		/// </summary>
		CustomerDetails,

		/// <summary>
		/// Vytvoření rezervace.
		/// </summary>
		Finish
	}
}
