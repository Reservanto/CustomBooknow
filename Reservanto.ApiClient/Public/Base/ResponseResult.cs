namespace Reservanto.ApiClient.Public
{
	/// <summary>
	/// Interface definující model pro odpověď serveru.
	/// </summary>
	public interface IResponseResult { }

	/// <summary>
	/// Bázový model pro třídy implementující <see cref="IResponseResult"/>.
	/// </summary>
	public abstract class ResponseResult : IResponseResult
	{
	}
}
