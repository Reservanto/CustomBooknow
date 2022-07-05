using Reservanto.ApiClient.Public;

namespace Reservanto.CustomBooknow.Models.Dtos
{
	/// <summary>
	/// Základní datový objekt, pro jakýkoliv objekt, potřebující svůj identifikátor.
	/// </summary>
	public class BaseIdentifiedDto
	{
		/// <summary>
		/// Vytvoří základní datový objekt (<c><see cref="Id"/> = 0</c>).
		/// </summary>
		public BaseIdentifiedDto()
		{
		}

		/// <summary>
		/// Vytvoří základní datový model a rovnou ho naplní Id.
		/// </summary>
		/// <param name="apiModel">Interface, obsahující Id a název objektu.</param>
		public BaseIdentifiedDto(INameAndId apiModel)
		{
			this.Id = apiModel.Id;
		}

		/// <summary>
		/// Vrací nebo nastavuje identifikátor objektu na straně Reservanta.
		/// </summary>
		public int Id
		{
			get;
			set;
		}
	}
}
