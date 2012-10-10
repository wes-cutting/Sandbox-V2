﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rentler.Adapters;

namespace Rentler.Web.Areas.Dashboard.Controllers
{
    /// <summary>
    /// Routing for the dashboard items.
    /// </summary>
    [Authorize]
    public class HomeController : Controller
    {
        ISearchAdapter searchAdapter;

        /// <summary>
        /// Initializes a new instance of the <see cref="HomeController"/> class.
        /// </summary>
        public HomeController(ISearchAdapter searchAdapter)
        {
            this.searchAdapter = searchAdapter;
        }

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            // if they have a property then go to my properties
            var result = this.searchAdapter.SearchUserProperties(
                User.Identity.Name, new PropertySearch() { ResultsPerPage = 1 });

            if (result.StatusCode != 200 || result.Result.Results.Count < 1)
                return Redirect("/dashboard/listing/favorites");
            else
                return Redirect("/dashboard/property/search");
        }
    }
}
