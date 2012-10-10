using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rentler.Web.Models;
using Rentler.Adapters;

namespace Rentler.Web.Controllers
{
	/// <summary>
	/// Controller for users to search for listings.
	/// </summary> 
	public class SearchController : Controller
	{
		ISearchAdapter searchAdapter;

		/// <summary>
		/// Initializes a new instance of the <see cref="SearchController"/> class.
		/// </summary>
		/// <param name="searchAdapter">The search adapter.</param>
		public SearchController(ISearchAdapter searchAdapter) 
		{
			this.searchAdapter = searchAdapter; 
		}

		/// <summary>
		/// The default page for searching.
		/// </summary>
		/// <param name="search">The search to perform.</param>
		/// <returns>Page with a list of search results.</returns>
		public ActionResult Index(Search search)
		{
			var result = this.searchAdapter.Search(search);

			if(Request.IsAjaxRequest())
				return PartialView("SearchResults", result.Result);

			return View(result.Result);
		}

        public ActionResult AjaxSearch(Search search)
        {
            var result = this.searchAdapter.Search(search);

            return Json(result.Result, JsonRequestBehavior.AllowGet);
        }
	}
}
