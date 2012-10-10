using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rentler.Adapters;

namespace Rentler.Web.Areas.Ksl.Controllers
{
	public class AuthController : Controller
	{
		IAuthAdapter authAdapter;

		public AuthController(
			IAuthAdapter authAdapter)
		{
			this.authAdapter = authAdapter;
		}

		public ActionResult Index(Guid? apiKey, string affiliateUserKey,
			string email, string passwordHash,
			string firstName, string lastName, string username)
		{
			var key = authAdapter.ValidateApiKey(apiKey);

			if(key.StatusCode == 403)
				return Json(key);

			var token = authAdapter.GetAuthToken(
				key.Result, affiliateUserKey, email, 
				passwordHash, firstName, lastName, username);

			return Json(token);
		}
	}
}
