using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rentler.Adapters;
using Rentler.Web.Models;

namespace Rentler.Web.Areas.Api.Controllers
{
    /// <summary>
    /// Api for managing buildings as seen as listings. We aren't
    /// creating a building controller because this is specific to
    /// people assuming we are dealing with listings.
    /// </summary>
    public class ListingController : Controller
    {
        IListingAdapter listingAdapter;
		IListingStatsAdapter statsAdapter;

        /// <summary>
        /// Initializes a new instance of the <see cref="ListingController"/> class.
        /// </summary>
        /// <param name="listingAdapter">The listing adapter.</param>
        public ListingController(
			IListingAdapter listingAdapter,
			IListingStatsAdapter statsAdapter)
        {
            this.listingAdapter = listingAdapter;
			this.statsAdapter = statsAdapter;
        }

        /// <summary>
        /// Adds a single view to the listing.
        /// </summary>
        /// <returns>Json status result of whether or not the listing
        /// view count was incremented successfully.</returns>
        [HttpPost]
        public ActionResult AddView(long id)
        {
            // Stop any cross-domain hacking by
            // requiring javascript request.
            if (!Request.IsAjaxRequest())
                return Json(Status.UnAuthorized<bool>());

			statsAdapter.IncrementListingStat(id);

            return Json(Status.OK<bool>(true));
        }

        /// <summary>
        /// Gets the total views for all the listings.
        /// </summary>
        /// <returns>Json status of the listing views.</returns>
        [HttpPost]
        public ActionResult TotalViews()
        {
            // Stop any cross-domain hacking by
            // requiring javascript request.
            if (!Request.IsAjaxRequest())
                return Json(Status.UnAuthorized<long>());

            return Json(/*this.listingAdapter.AddListingView(id)*/Status.OK<User>(null));
        }

        /// <summary>
        /// Reports the specified id as malicious
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="message">The message.</param>
        /// <returns>A Json status result of reporting.</returns>
        [HttpPost]
        public ActionResult Report(long id, string message)
        {
            // Stop any cross-domain hacking by
            // requiring javascript request.
            if (!Request.IsAjaxRequest())
                return Json(Status.UnAuthorized<long>());

            return Json(this.listingAdapter.ReportListing(id, message));
        }

        /// <summary>
        /// Gets the total search views for all the listings.
        /// </summary>
        /// <returns>Json status of the listing views.</returns>
        [HttpPost]
        public ActionResult TotalSearchViews()
        {
            // Stop any cross-domain hacking by
            // requiring javascript request.
            if (!Request.IsAjaxRequest())
                return Json(Status.UnAuthorized<long>());

            return Json(/*this.listingAdapter.AddListingView(id)*/Status.OK<User>(null));
        }

		[HttpPost]
		public ActionResult Favorite(long id)
		{
			// Stop any cross-domain hacking by
			// requiring javascript request.
			if(!Request.IsAjaxRequest() || !User.Identity.IsAuthenticated)
				return Json(Status.UnAuthorized<long>());

			var status = listingAdapter.CreateSavedBuilding(id, User.Identity.Name);

			return Json(status);
		}

		[HttpPost]
		public ActionResult Unfavorite(long id)
		{
			// Stop any cross-domain hacking by
			// requiring javascript request.
			if(!Request.IsAjaxRequest() || !User.Identity.IsAuthenticated)
				return Json(Status.UnAuthorized<long>());

			var status = listingAdapter.DeleteSavedBuilding(id, User.Identity.Name);

			return Json(status);
		}

		[HttpPost]
		public ActionResult Interested(ListingInterestedModel model)
		{            
            if (Request.IsAjaxRequest())
            {
                var status = this.listingAdapter.CreateInterestInBuilding(
                    User.Identity.Name, 
                    model.BuildingId, 
                    model.Content
                );
                
                if (status.StatusCode != 200)
                    return Json(Status.Error<bool>(status.Message, false));

                return Json(Status.OK<bool>(true));
            }
            
			return Redirect("/listing/" + model.BuildingId);
		}
    }
}
