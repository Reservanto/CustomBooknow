using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Reservanto.CustomBooknow.Code.ReservantoApiClient;

namespace Reservanto.CustomBooknow.Code
{
	/// <summary>
	/// Filtr, který přesměruje na nastavení aplikace pokud nastane výjimka typu <see cref="LongTimeTokenExceptionFilter"/>.
	/// </summary>
	public class LongTimeTokenExceptionFilter : IExceptionFilter
	{
		public void OnException(ExceptionContext context)
		{
			if (context.Exception is InvalidLongTimeTokenException)
			{
				context.Result = RedirectToAction("ConnectToAPI", "Api");
			}
		}

		/// <summary>
		/// Vrací přesměrování na danou akci.
		/// </summary>
		/// <param name="action">Akce.</param>
		/// <param name="controller">Controller.</param>
		/// <returns>Vrací <see cref="RedirectToRouteResult">přesměrování</see> na danou akci v daném kontroleru.</returns>
		private ActionResult RedirectToAction(string action, string controller)
		{
			return new RedirectToRouteResult(
				new RouteValueDictionary(
					new
					{
						action,
						controller,
						area = ""
					}
				)
			);
		}
	}
}
