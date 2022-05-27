namespace Reservanto.ApiClient.Requests
{
	/// <summary>
	/// Požadavek, který obsahuje pouze text.
	/// </summary>
	internal class TextRequest : Request
	{
		/// <summary>
		/// Vrací nebo nastavuje text požadavku.
		/// </summary>
		public string Text
		{
			get;
			set;
		}
	}
}
