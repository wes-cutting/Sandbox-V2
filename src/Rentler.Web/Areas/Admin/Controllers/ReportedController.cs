using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rentler.Facades;
using Rentler.Web.Areas.Admin.Models;

namespace Rentler.Web.Areas.Admin.Controllers
{
    [Authorize(Roles="Admin")]
    public class ReportedController : Controller
    {
        IListingFacade listingFacade;

        public ReportedController(IListingFacade listingFacade)
        {
            this.listingFacade = listingFacade;
        }

        public ActionResult Search(int? page)
        {
            ReportedSearchModel model = new ReportedSearchModel();
            var result = this.listingFacade.GetReportedListings(page);
            if (result.StatusCode != 200)
                return this.NotFoundException();
            model.Listings = result.Result;
            model.Page = page.HasValue ? page.Value : 1;
            return View(model);
        }

        public ActionResult RemoveFlag(int? id)
        {
            if (!id.HasValue)
                return this.NotFoundException();
            var result = this.listingFacade.RemoveFlag(id.Value);
            if (result.StatusCode != 200)
                return this.NotFoundException();
            return Redirect("/admin/reported/search");
        }

        public ActionResult Deactivate(int? id)
        {
            if (!id.HasValue)
                return this.NotFoundException();
            var result = this.listingFacade.RemoveFlagAndDeactivate(id.Value);
            if (result.StatusCode != 200)
                return this.NotFoundException();
            return Redirect("/admin/reported/search");
        }

        public ActionResult Remove(int? id)
        {
            if (!id.HasValue)
                return this.NotFoundException();
            var result = this.listingFacade.RemoveByAdmin(id.Value);
            if (result.StatusCode != 200)
                return this.NotFoundException();
            return Redirect("/admin/reported/search");
        }
    }
}
