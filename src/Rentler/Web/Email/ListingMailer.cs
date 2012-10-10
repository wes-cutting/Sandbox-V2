using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mvc.Mailer;
using System.Net.Mail;
using Rentler.Web.Axioms.Extensions;

namespace Rentler.Web.Email
{
	public class ServerListingMailer : MailerBase, IListingMailer
	{
		public Status<bool> Interested(EmailListingInterestedModel model)
		{
			//create message
			var message = new MailMessage { Subject = model.LandlordEmail };
			message.To.Add(model.LandlordEmail);

			//create model for it
			ViewData = new System.Web.Mvc.ViewDataDictionary(model);

			//render it
			PopulateBody(message, viewName: "Interested");

			//send it
			return this.SendMessage(message);
		}
	}

	public class ClientListingMailer : MailerService, IListingMailer
	{
		public Status<bool> Interested(EmailListingInterestedModel model)
		{
			return SendMail(model, "listing/interested");
		}
	}
}
