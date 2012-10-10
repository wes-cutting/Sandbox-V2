using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mvc.Mailer;
using System.Net.Mail;
using Rentler.Web.Axioms.Extensions;

namespace Rentler.Web.Email
{
    public class ServerPropertyMailer : MailerBase, IPropertyMailer
    {
        public Status<bool> RequestApplication(EmailRequestApplicationModel model)
        {
            //create message
            var message = new MailMessage { Subject = model.LandlordEmail };
            message.To.Add(model.LeadEmail);

            //create model for it
            ViewData = new System.Web.Mvc.ViewDataDictionary(model);

            //render it
            PopulateBody(message, viewName: "RequestApplication");

            //send it
            return this.SendMessage(message);
        }
    }
}
