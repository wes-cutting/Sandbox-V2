using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rentler.Adapters;
using Rentler.Web.Models;
using Rentler.Web.Axioms;

namespace Rentler.Web.Controllers
{
	/// <summary>
	/// Controller for viewing listings.
	/// </summary>
	public class ListingController : Controller
	{
		IListingAdapter listingAdapter;

		/// <summary>
		/// Initializes a new instance of the <see cref="ListingController"/> class.
		/// </summary>
		/// <param name="listingAdapter">The listing adapter.</param>
		public ListingController(IListingAdapter listingAdapter)
		{
			this.listingAdapter = listingAdapter;
		}

		/// <summary>
		/// View a listing by its id.
		/// </summary>
		/// <param name="id">The id of the listing to view.</param>
		/// <returns>The listing page</returns>
		[UnAuthenticatedCache(Duration = 3600)]
		public ActionResult Index(long id)
		{
			var status = this.listingAdapter.GetListing(id);

			if(status.StatusCode != 200)
				return HttpNotFound();

			// this is ok because the adapter will return 0 if count cannot
			// be retrieved
			var viewCount = this.listingAdapter.GetListingViews(id).Result;
			var userHasSaved = this.listingAdapter.ListingWasSavedBy(id, User.Identity.Name).Result;

			var model = new ListingIndexModel();
			model.Listing = status.Result;
			model.ListingViews = viewCount;
			model.UserHasSaved = userHasSaved;

			return View(model);
		}

        [UnAuthenticatedCache(Duration = 3600)]
        public ActionResult Quick(long id)
        {
            var status = this.listingAdapter.GetListing(id);

            if (status.StatusCode != 200)
                return HttpNotFound();

            // this is ok because the adapter will return 0 if count cannot
            // be retrieved
            var viewCount = this.listingAdapter.GetListingViews(id).Result;
            var userHasSaved = this.listingAdapter.ListingWasSavedBy(id, User.Identity.Name).Result;

            var model = new ListingIndexModel();
            model.Listing = status.Result;
            model.ListingViews = viewCount;
            model.UserHasSaved = userHasSaved;

            return PartialView("ListingDetails", model);
        }

		[ActionName("Report")]
		public ActionResult ReportListing(long id)
		{
			ViewBag.Id = id;
			return View();
		}

		[HttpPost, ActionName("Report")]
		public ActionResult ReportListing(long id, string reportedText)
		{
			var status = this.listingAdapter.ReportListing(id, reportedText);

			if(status.StatusCode != 200)
			{
				string msg = string.Format("Failed to report this listing: {0}", status.Message);
				return RedirectToAction("ReportError", new { id = id, error = msg });
			}

			return RedirectToAction("Index", new { id = id });
		}

		public ActionResult ReportError(long id, string error)
		{
			ViewBag.Id = id;
			ViewBag.Error = error;

			return View();
		}

		[HttpGet]
		public ActionResult Interested(long id)
		{
			var status = this.listingAdapter.GetListing(id);

			//if (status.StatusCode != 200)            
			//    return HttpNotFound();

			return View(status.Result);
		}
	}
}
