using System.ComponentModel.DataAnnotations;

namespace Reservanto.ApiClient.Public
{
	/// <summary>
	/// Model pro výběr jakéhokoliv objektu, který lze vybrat pomocí Id.
	/// </summary>
	public class SelectorModel : RequestModel
	{
		public SelectorModel() { }

		/// <summary>
		/// Vytvoří novou instanci požadavku, pro výběr jakéhokoliv objektu pomocí Id.
		/// </summary>
		/// <param name="id">Id požadovaného objektu.</param>
		public SelectorModel(int id)
		{
			this.Id = id;
		}

		/// <summary>
		/// Vrací nebo nastavuje Id výběru čehokoliv.
		/// </summary>
		[Required]
		public int Id
		{
			get;
			set;
		}
	}
}
