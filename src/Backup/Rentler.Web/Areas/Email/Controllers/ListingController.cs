using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rentler.Web.Email;

namespace Rentler.Web.Areas.Email.Controllers
{
	public class ListingController : Controller
	{
		IListingMailer mailer;

		public ListingController(IListingMailer mailer)
		{
			this.mailer = mailer;
		}

		public ActionResult Interested(EmailListingInterestedModel model)
		{
			return Json(mailer.Interested(model));
		}
	}
}
