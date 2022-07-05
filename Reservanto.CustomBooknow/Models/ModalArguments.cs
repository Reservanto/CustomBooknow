namespace Reservanto.CustomBooknow.Models
{
	/// <summary>
	/// Třída, nesoucí všechny vstupní parametry pro rezervační formulář.
	/// </summary>
	public class ModalArguments
	{
		#region Veřejné prvky, zadavatelné z url.
		
		/// <summary>
		/// Vrací nebo nastavuje identifikátor zaměření, které se má vybrat.
		/// </summary>
		public int sId
		{
			get;
			set;
		}

		#endregion

		/// <summary>
		/// Vrací identifikátor zaměření, které se má vybrat.
		/// </summary>
		public int SegmentId => this.sId;
	}
}
