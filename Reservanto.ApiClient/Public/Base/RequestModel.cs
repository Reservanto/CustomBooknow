namespace Reservanto.ApiClient.Public
{
	/// <summary>
	/// Interface definující model pro request.
	/// </summary>
	public interface IRequestModel { }

	/// <summary>
	/// Bázový model pro modely implementující rozhraní <see cref="IRequestModel"/>.
	/// </summary>
	public abstract class RequestModel : IRequestModel
	{
		/// <summary>
		/// Vrací prázdný požadavek na API.
		/// </summary>
		public static RequestModel Empty => new EmptyModel();
	}

	/// <summary>
	/// Interní model pro RequestModel - prázdný
	/// </summary>
	internal class EmptyModel : RequestModel { }
}
