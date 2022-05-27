namespace Reservanto.ApiClient.Public
{
	/// <summary>
	/// Api model reprezentující cenovou hladinu.
	/// </summary>
	/// <remarks>
	/// Objekt sám o sobě moc smysl nemá, smysl dostává až při provázání s např. službou.
	/// </remarks>
	public class ItemPriceLevelModel
	{
		/// <summary>
		/// Vrací nebo nastavuje Id cenové hladiny.
		/// </summary>
		public int PriceLevelId
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje název cenové hladiny.
		/// </summary>
		public string Name
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje cenu, kterou tato hladina udává.
		/// </summary>
		public decimal Price
		{
			get;
			set;
		}
	}
}
