namespace Reservanto.CustomBooknow.Code.Enums
{
	/// <summary>
	/// Seznam všech podporovaných typů zaměření.
	/// </summary>
	public enum SegmentType
	{
		/// <summary>
		/// Jeden zákazník v libovolný čas.
		/// </summary>
		OneToOne = 1,

		/// <summary>
		/// Skupina zákazníků v pevně daný čas.
		/// </summary>
		Classes = 2
	}
}
