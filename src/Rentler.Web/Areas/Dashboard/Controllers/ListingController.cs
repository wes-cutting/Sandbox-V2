using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rentler.Adapters;
using Rentler.Web.Areas.Dashboard.Models;
using Rentler.Web.Axioms.Extensions;

namespace Rentler.Web.Areas.Dashboard.Controllers
{
    /// <summary>
    /// Controller for managing the properties that you
    /// have saved.
    /// </summary>
    [Authorize]
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
        /// Action for displaying the user's saved properties.
        /// </summary>
        /// <returns>View of the user's saved properties.</returns>
        [Authorize]
        public ActionResult Favorites(int? page, int? pageSize)
        {
            var result = this.listingAdapter.GetFavoritesForUser(User.Identity.Name, page, pageSize);
            return View(result.Result);
        }
    }
}
