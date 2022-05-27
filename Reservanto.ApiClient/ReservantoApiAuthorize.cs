using System;
using System.Collections.Specialized;

namespace Reservanto.ApiClient
{
	/// <summary>
	/// Pomocná třída pro autorizaci api klienta s přihlášeným obchodníkem.
	/// </summary>
	public static class ReservantoApiAuthorize
	{
		/// <summary>
		/// Vrací Uri, na kterou má dojít k přesměrování při žádosti obchodníka o autorizaci.
		/// </summary>
		/// <param name="baseUrl">Bázová url api.</param>
		/// <param name="clientId">Id api klienta.</param>
		/// <param name="redirectUrl">Url zpět, na které se bude odchytávat callback (<see cref="ParseCallback"/>).</param>
		/// <param name="rights">Pole oprávnění, o které tento klient žádá.</param>
		public static Uri GetRedirectUrl(string baseUrl, string clientId, string redirectUrl, string[] rights)
		{
			var builder = new UriBuilder(baseUrl);
			builder.Path += (builder.Path.EndsWith("/") ? "" : "/") + "Authorize/GetLongTimeToken";

			var query = $"clientId={clientId}";
			foreach (var right in rights)
			{
				query += $"&rights={right}";
			}
			query += "&redirectUrl=" + redirectUrl;

			builder.Query = query;

			return builder.Uri;
		}

		/// <summary>
		/// Zparsuje příchozí callback z API reservanto.
		/// </summary>
		/// <param name="queryString">Typicky např.: Request.QueryString</param>
		/// <param name="ltt">V případě, že uživatel žádost potvrdil, vrací Long time token.</param>
		/// <returns>Vrací <c>true</c> pokud uživatel žádost potvrdil, jinak vrací <c>false</c>.</returns>
		public static bool ParseCallback(NameValueCollection queryString, out string ltt)
		{
			const string statusName = "accepted";
			const string tokenName = "token";

			if (queryString.Get(statusName) == "1")
			{
				ltt = queryString.Get(tokenName);
				return true;
			}

			ltt = null;
			return false;
		}
	}
}
