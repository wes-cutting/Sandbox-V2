using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rentler.Web.Email;

namespace Rentler.Web.Areas.Email.Controllers
{
	public class AccountController : Controller
	{
		IAccountMailer mailer;

		public AccountController(IAccountMailer mailer)
		{
			this.mailer = mailer;
		}

		public ActionResult Register(EmailAccountRegisterModel model)
		{
			var status = mailer.Register(model);

			return Json(status, JsonRequestBehavior.AllowGet);
		}

		public ActionResult ChangePassword(EmailChangePasswordModel model)
		{
			var status = mailer.ChangePassword(model);

			return Json(status, JsonRequestBehavior.AllowGet);
		}
	}
}
