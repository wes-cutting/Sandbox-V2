using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Rentler.Web.Areas.Api.Controllers
{
    public class AuthController : Controller
    {
        //
        // GET: /Api/Auth/

        public ActionResult IsAuthenticated()
        {
			return Json(Status.OK<bool>(User.Identity.IsAuthenticated));
        }

    }
}
