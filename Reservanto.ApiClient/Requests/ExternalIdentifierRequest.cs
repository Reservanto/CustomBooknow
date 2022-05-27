namespace Reservanto.ApiClient.Requests
{
	/// <summary>
	/// Požadavek pro přidání/smazání/editaci externího identifikátoru.
	/// </summary>
	internal class ExternalIdentifierRequest : Request
	{
		private ExternalIdentifierRequest()
		{
		}

		/// <summary>
		/// Vrací nebo nastavuje hodnotu externího identifikátoru.
		/// </summary>
		public string ExternalId
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje identifikátor na straně Reservanta (pro účely provázání, případně smazání).
		/// </summary>
		public int ReservantoId
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje typ objektu na straně Reservanta.
		/// </summary>
		public string ObjectType
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje data externího identifikátoru.
		/// </summary>
		public string ExternalData
		{
			get;
			set;
		}

		#region Factory methods

		/// <summary>
		/// Vytvoří nový požadavek na přidání externího identifikátoru.
		/// </summary>
		/// <param name="externalId">Hodnota externího identifikátoru.</param>
		/// <param name="reservantoId">Id entity na straně Reservanta.</param>
		/// <param name="objectType">Typ objektu, kterému náleží Reservantí identifikátor.</param>
		/// <param name="externalData">Dodatečná data, které identifikátor může nést.</param>
		public static ExternalIdentifierRequest CreateExternalIdentifier(string externalId, int reservantoId, string objectType, string externalData)
		{
			var request = new ExternalIdentifierRequest()
			{
				ExternalId = externalId,
				ReservantoId = reservantoId,
				ObjectType = objectType,
				ExternalData = externalData
			};

			return request;
		}

		/// <summary>
		/// Vytvoří nový požadavek na editaci externího identifikátoru.
		/// </summary>
		/// <param name="externalId">Hodnota externího identifikátoru.</param>
		/// <param name="objectType">Typ objektu, kterému náleží Reservantí identifikátor.</param>
		/// <param name="externalData">Dodatečná data, které identifikátor může nést.</param>
		public static ExternalIdentifierRequest EditExternalIdentifier(string externalId, string objectType, string externalData)
		{
			var request = new ExternalIdentifierRequest()
			{
				ExternalId = externalId,
				ObjectType = objectType,
				ExternalData = externalData
			};

			return request;
		}

		/// <summary>
		/// Vytvoří nový požadavek na smazání externího identifikátoru.
		/// </summary>
		/// <param name="externalId">Hodnota externího identifikátoru.</param>
		/// <param name="objectType">Typ objektu, kterému náleží Reservantí identifikátor.</param>
		public static ExternalIdentifierRequest DeleteExternalIdentifier(string externalId, string objectType)
		{
			var request = new ExternalIdentifierRequest()
			{
				ExternalId = externalId,
				ObjectType = objectType,
			};

			return request;
		}

		/// <summary>
		/// Vytvoří nový požadavek na získání objektu, dle externího identifikátoru.
		/// </summary>
		/// <param name="externalId">Hodnota externího identifikátoru.</param>
		/// <param name="objectType">Typ objektu, kterému náleží Reservantí identifikátor.</param>
		public static ExternalIdentifierRequest GetByExternalIdentifier(string externalId, string objectType)
		{
			var request = new ExternalIdentifierRequest()
			{
				ExternalId = externalId,
				ObjectType = objectType,
			};

			return request;
		}

		#endregion

	}
}
