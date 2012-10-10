using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rentler.Adapters;
using Rentler.Web.Models;
using Rentler.Web.Axioms;
using Mvc.Mailer;

namespace Rentler.Web.Controllers
{
    /// <summary>
    /// Controller containing the home information for the application.
    /// </summary>
    public class HomeController : Controller
    {
        IListingAdapter listingAdapter;

        /// <summary>
        /// Initializes a new instance of the <see cref="HomeController"/> class.
        /// </summary>
        /// <param name="listingAdapter">The listing adapter to get
        /// general stats for the listings for the home page.</param>
        public HomeController(IListingAdapter listingAdapter)
        {
            this.listingAdapter = listingAdapter;
        }

        /// <summary>
        /// Returns the home page for the application.
        /// </summary>
        /// <remarks>Output cached for an hour for unauthenticated users.</remarks>
        /// <returns>The home page for the application</returns>
        [UnAuthenticatedCache(Duration = 3600)]
        public ActionResult Index()
        {
            HomeIndexModel model = new HomeIndexModel();
            return View(model);
        }

        /// <summary>
        /// Returns the privacy policy. 
        /// </summary>
        /// <remarks>Output cached for a day for unauthenticated users.</remarks>
        /// <returns>A view with the privacy policy.</returns>
        [UnAuthenticatedCache(Duration = 86400)]
        public ActionResult Privacy()
        {
            return View();
        }

        /// <summary>
        /// Returns a view of the about page.
        /// </summary>
        /// <remarks>Output cached for a day for unauthenticated users.</remarks>
        /// <returns>A view of the about page.</returns>
        [UnAuthenticatedCache(Duration = 86400)]
        public ActionResult About()
        {
            return View();
        }

        /// <summary>
        /// Returns a view of the terms page.
        /// </summary>
        /// <remarks>Output cached for a day for unauthenticated users.</remarks>
        /// <returns>A view of the terms page.</returns>
        [UnAuthenticatedCache(Duration = 86400)]
        public ActionResult Terms()
        {
            return View();
        }
    }
}
