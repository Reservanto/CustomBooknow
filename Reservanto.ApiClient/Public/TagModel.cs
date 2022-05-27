namespace Reservanto.ApiClient.Public
{
	public class TagModel : ResponseResult, INameAndId
	{
		/// <summary>
		/// Vrací nebo nastavuje Id štítku v systému Reservanto.
		/// </summary>
		public int Id
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje název štítku.
		/// </summary>
		public string Name
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje barvu popředí (textu) štítku.
		/// </summary>
		public string ForegroundColor
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje barvu pozadí štítku.
		/// </summary>
		public string BackgroundColor
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje počet položek označených tímto štítkem.
		/// </summary>
		public int MarkedItemsCount
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
