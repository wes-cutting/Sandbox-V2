using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rentler.Adapters;
using System.Web.Security;
using Rentler.Web.Models;
using Rentler.Web.Axioms.Extensions;

namespace Rentler.Web.Areas.Ksl.Controllers
{
    /// <summary>
    /// Controller managing listing functions for 
    /// the KSL integration.
    /// </summary>
    public class ListingController : Controller
    {
        IListingAdapter listingAdapter;
		IAuthAdapter authAdapter;

        public ListingController(
			IListingAdapter listingAdapter,
			IAuthAdapter authAdapter)
        {
            this.listingAdapter = listingAdapter;
			this.authAdapter = authAdapter;
        }

		[HttpGet]
        public ActionResult Index(long ad, Guid? token)
        {
			if(!User.Identity.IsAuthenticated && token.HasValue)
			{
				var user = authAdapter.ValidateAuthToken(token.Value);

				if(user.StatusCode == 200)
					FormsAuthentication.SetAuthCookie(user.Result.Username, true);
			}

			var status = this.listingAdapter.GetListing(ad);

			// this is ok because the adapter will return 0 if count cannot
			// be retrieved
			var viewCount = this.listingAdapter.GetListingViews(ad).Result;

			var userHasSaved = this.listingAdapter.ListingWasSavedBy(ad, User.Identity.Name).Result;

			var model = new ListingIndexModel();
			model.Listing = status.Result;
			model.ListingViews = viewCount;
			model.UserHasSaved = userHasSaved;

			return View(model);
        }

		[HttpPost]
		public ActionResult Favorite(long buildingId, string email)
		{
			var result = listingAdapter.CreateSavedBuilding(buildingId, email);

			return this.NewtonJson(result);
		}

		[HttpPost]
		public ActionResult UnFavorite(long buildingId, string email)
		{
			var result = listingAdapter.DeleteSavedBuilding(buildingId, email);

			return this.NewtonJson(result);
		}
    }
}
