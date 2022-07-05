using Microsoft.AspNetCore.Mvc;
using Reservanto.CustomBooknow.Code.ReservantoApiClient;
using System.Web;
using Microsoft.Extensions.Options;

namespace Reservanto.CustomBooknow.Controllers
{
	/// <summary>
	/// Kontroler, starající se o propojení s ReservantoApi.
	/// </summary>
	public class ApiController : BaseController
	{
		private readonly ReservantoApiConfiguration configuration;

		public ApiController(ReservantoApiConnector reservantoApi, IOptions<ReservantoApiConfiguration> configuration)
			: base(reservantoApi)
		{
			this.configuration = configuration.Value;
		}

		/// <summary>
		/// Vrací stránku s návodem, jak aplikaci propojit s API.
		/// </summary>
		/// <returns></returns>
		public IActionResult ConnectToAPI()
		{
			return View(this.configuration);
		}

		/// <summary>
		/// Vrací přesměrování na autorizaci vůči reservantímu api.
		/// </summary>
		public IActionResult Redirect()
		{
			var redirectUrl = Url.Action("Callback", "Api", null, Url.ActionContext.HttpContext.Request.Scheme);

			return Redirect(ReservantoApi.CreateRedirectUrl(redirectUrl));
		}

		/// <summary>
		/// Vrací výsledek autorizace vůči reservantímu api.
		/// </summary>
		public IActionResult Callback()
		{
			// Přečtu odpověď.
			ReservantoApi.ParseResponse(HttpUtility.ParseQueryString(Request.QueryString.Value));

			return Content("OK");
		}
	}
}
