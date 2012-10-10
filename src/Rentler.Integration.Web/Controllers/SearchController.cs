using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rentler.Integration.Web.Models;

namespace Rentler.Integration.Web.Controllers
{
    public class SearchController : Controller
    {
        public ActionResult Index(Search model)
        {
            model.Query = Request.Url.Query;

            return View(model);
        }
    }
}
