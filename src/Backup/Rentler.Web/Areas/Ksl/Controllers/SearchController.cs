using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rentler.Adapters;
using System.Web.Security;

namespace Rentler.Web.Areas.Ksl.Controllers
{
	public class SearchController : Controller
	{
		ISearchAdapter searchAdapter;
		IAuthAdapter authAdapter;
		IAccountAdapter accountAdapter;

		public SearchController(
			ISearchAdapter searchAdapter,
			IAuthAdapter authAdapter,
			IAccountAdapter accountAdapter)
		{
			this.searchAdapter = searchAdapter;
			this.authAdapter = authAdapter;
			this.accountAdapter = accountAdapter;
		}

		[HttpGet]
		public ActionResult Index(Search search, Guid? token)
		{
			if(!User.Identity.IsAuthenticated && token.HasValue)
			{
				var user = authAdapter.ValidateAuthToken(token.Value);

				if(user.StatusCode == 200)
					FormsAuthentication.SetAuthCookie(user.Result.Username, true);
			}

			return View();
		}

		[HttpGet]
		public ActionResult ListingCount(Guid? apiKey)
		{
			var isValid = authAdapter.ValidateApiKey(apiKey);

			if(isValid.StatusCode != 200)
				return Json(isValid, JsonRequestBehavior.AllowGet);

			return Json(
				new ListingCountModel
				{
					Apartment = 5,
					CondoTownhome = 6,
					HorseLivestock = 10,
					Loft = 343,
					ManufacturedHome = 343,
					MultiFamilyHome = 36745,
					SingleFamilyHome = 8330,
					SingleRoom = 332
				}, JsonRequestBehavior.AllowGet);
		}
	}
}
