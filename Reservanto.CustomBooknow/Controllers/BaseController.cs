using Microsoft.AspNetCore.Mvc;
using Reservanto.CustomBooknow.Code.ReservantoApiClient;

namespace Reservanto.CustomBooknow.Controllers
{
	/// <summary>
	/// Bázový kontroler, obsahující společné vlastnosti.
	/// </summary>
	public class BaseController : Controller
	{
		private readonly ReservantoApiConnector reservantoApi;

		public BaseController(ReservantoApiConnector reservantoApi)
		{
			this.reservantoApi = reservantoApi;
		}

		/// <summary>
		/// Vrací API klienta pro komunikaci s Reservantem.
		/// </summary>
		protected ReservantoApiConnector ReservantoApi => this.reservantoApi;
	}
}
