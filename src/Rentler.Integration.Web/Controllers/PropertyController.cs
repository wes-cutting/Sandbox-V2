using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Rentler.Integration.Web.Controllers
{
    public class PropertyController : Controller
    {
        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Edit(long id)
        {
            ViewBag.Id = id;
            return View();
        }        
    }
}
