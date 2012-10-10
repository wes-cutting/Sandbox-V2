using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;

namespace Rentler.Auth
{
	/// <summary>
	/// Forms authentication wrapper that allows user
	/// information to be passed.
	/// </summary>
	public class CustomAuthentication
	{
		/// <summary>
		/// Signs the user out. Passes through.
		/// </summary>
		public static void SignOut()
		{
			// expire the cookie
			var cookie = HttpContext.Current.Response.Cookies[FormsAuthentication.FormsCookieName];
			cookie.Expires = DateTime.Now;
			HttpContext.Current.Response.Cookies.Remove(FormsAuthentication.FormsCookieName);
			FormsAuthentication.SignOut();
		}

		/// <summary>
		/// Sets the auth cookie with a custom forms authentication
		/// ticket that utilizes the userid.
		/// </summary>
		/// <param name="username">The username.</param>
		/// <param name="userId">The user id.</param>
		/// <param name="isPersistant">if set to <c>true</c> [is persistant].</param>
		public static void SetAuthCookie(string username, int userId, bool isPersistant)
		{
			// create the ticket
			FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
				2, username, DateTime.Now, DateTime.Now.AddDays(30),
				isPersistant, userId.ToString(), FormsAuthentication.FormsCookiePath);

			// encrypt
			string encTicket = FormsAuthentication.Encrypt(ticket);

			// stor
			HttpContext.Current.Response.Cookies.Add(
				new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));

            // allow cross-browser auth cookie (IE8)
            HttpContext.Current.Response.AddHeader("p3p",
                "CP=\"IDC DSP COR ADM DEVi TAIi PSA PSD IVAi IVDi CONi HIS OUR IND CNT\"");
		}

		/// <summary>
		/// Gets the custom identity of a user.
		/// </summary>
		/// <returns>An anonymous identity if they aren't authenticated. Otherwise
		/// it returns an identity with the user id.</returns>
		/// <exception cref="System.InvalidCastException">If the identity isn't a FormsIdentity</exception>
		public static Identity GetIdentity()
		{
			var identity = HttpContext.Current.User.Identity;

			if (!identity.IsAuthenticated)
				return new Identity();

			if (identity is FormsIdentity)
			{
				var formsIdentity = identity as FormsIdentity;
				return new Identity(int.Parse(formsIdentity.Ticket.UserData), formsIdentity.Name);
			}

			throw new InvalidCastException("The identity is not in a valid format");
		}
	}
}
