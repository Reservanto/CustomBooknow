using Microsoft.AspNetCore.Mvc;
using Reservanto.CustomBooknow.Code.ReservantoApiClient;
using System.Web;

namespace Reservanto.CustomBooknow.Controllers
{
	/// <summary>
	/// Kontroler, starající se o propojení s ReservantoApi.
	/// </summary>
	public class ApiController : BaseController
	{
		public ApiController(ReservantoApiConnector reservantoApi) : base(reservantoApi)
		{
		}

		/// <summary>
		/// Vratí na přesměrování autorizace vůči reservantímu api.
		/// </summary>
		public IActionResult Redirect()
		{
			var redirectUrl = Url.Action("Callback", "Api", null, Url.ActionContext.HttpContext.Request.Scheme);

			return Redirect(ReservantoApi.CreateRedirectUrl(redirectUrl));
		}

		public IActionResult Callback()
		{
			// Přečtu odpověď.
			ReservantoApi.ParseResponse(HttpUtility.ParseQueryString(Request.QueryString.Value));

			return Content("OK");
		}
	}
}
