using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mvc.Mailer;
using System.Net.Mail;
using Rentler.Web.Axioms.Extensions;

namespace Rentler.Web.Email
{
	public class ServerOrderMailer : MailerBase, IOrderMailer
	{
		public Status<bool> Receipt(EmailOrderReceiptModel model)
		{
			//create message
			var message = new MailMessage { Subject = "Your Receipt" };
			message.To.Add(model.To);

			//create model for it
			ViewData = new System.Web.Mvc.ViewDataDictionary(model);

			//render it
			PopulateBody(message, viewName: "Receipt");

			//send it
			return this.SendMessage(message);
		}
	}

	public class ClientOrderMailer : MailerService, IOrderMailer
	{
		public Status<bool> Receipt(EmailOrderReceiptModel model)
		{
			return SendMail(model, "order/receipt");
		}
	}
}
