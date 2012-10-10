using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rentler.Adapters;
using System.Web.Security;
using Rentler.Auth;

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
					CustomAuthentication.SetAuthCookie(user.Result.Username, user.Result.UserId, true);
			}


            // Fix for php sending goofy data to us.
            if (Request["Amenities[]"] != null)
            {
                if(search.Amenities == null)
                {
                    List<string> strings = new List<string>(
                        Request["Amenities[]"].Split(",".ToCharArray()));
                    search.Amenities = strings.ToArray();                    
                }
            }

            // Fix for php sending goofy data to us.
            if (Request["Terms[]"] != null)
            {
                if (search.Terms == null)
                {
                    List<string> strings = new List<string>(
                        Request["Terms[]"].Split(",".ToCharArray()));
                    search.Terms = strings.ToArray();

                }
            }

            var result = this.searchAdapter.Search(search);

            if (Request.IsAjaxRequest())
                return PartialView("SearchResults", result.Result);

            return View(result.Result);
		}

		[HttpGet]
		public ActionResult ListingCount(Guid? apiKey)
		{
			var isValid = authAdapter.ValidateApiKey(apiKey);

			if (isValid.StatusCode != 200)
				return Json(isValid, JsonRequestBehavior.AllowGet);

			var result = searchAdapter.GetListingCounts();

			return Json(result.Result, JsonRequestBehavior.AllowGet);
		}
	}
}
