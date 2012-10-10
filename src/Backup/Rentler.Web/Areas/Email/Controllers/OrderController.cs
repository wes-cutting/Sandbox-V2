using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rentler.Web.Email;
using Rentler.Web.Axioms.Extensions;
using Rentler.Web.Axioms;

namespace Rentler.Web.Areas.Email.Controllers
{
    public class OrderController : Controller
    {
		IOrderMailer mailer;

		public OrderController(IOrderMailer mailer)
		{
			this.mailer = mailer;
		}

        public ActionResult Receipt(EmailOrderReceiptModel model)
        {
			return Json(mailer.Receipt(model));
        }
    }
}
