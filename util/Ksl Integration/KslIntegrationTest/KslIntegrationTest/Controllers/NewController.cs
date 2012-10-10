using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KslIntegrationTest.Controllers
{
    public class NewController : Controller
    {
        //
        // GET: /New/

        public ActionResult Index(long? id)
        {
            return View();
        }

		public ActionResult Minified(long? id)
		{
			return View();
		}

    }
}
