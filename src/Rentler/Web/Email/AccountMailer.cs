using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mvc.Mailer;
using System.Net.Mail;
using Rentler.Web.Axioms.Extensions;

namespace Rentler.Web.Email
{
	public class ServerAccountMailer : MailerBase, IAccountMailer
	{
		public Status<bool> Register(EmailAccountRegisterModel model)
		{
			//create message
			var message = new MailMessage { Subject = "Welcome to Rentler!" };
			message.To.Add(model.To);

			//create model for it
			ViewData = new System.Web.Mvc.ViewDataDictionary(model);

			//render it
			PopulateBody(message, viewName: "Register");

			//send it
			return this.SendMessage(message);
		}

		public Status<bool> ChangePassword(EmailChangePasswordModel model)
		{
			//create message
			var message = new MailMessage { Subject = "Your password has changed" };
			message.To.Add(model.To);

			//create model for it
			ViewData = new System.Web.Mvc.ViewDataDictionary(model);

			//render it
			PopulateBody(message, viewName: "ChangePassword");

			//send it
			return this.SendMessage(message);
		}

        public Status<bool> ForgotPassword(EmailForgotPasswordModel model)
        {
            //create message
            var message = new MailMessage { Subject = "Your password has been reset" };
            message.To.Add(model.To);            

            //create model for it
            ViewData = new System.Web.Mvc.ViewDataDictionary(model);

            //render it
            PopulateBody(message, viewName: "ForgotPassword");

            //send it
            return this.SendMessage(message);
        }

        public Status<bool> SendApplication(EmailSendApplicationModel model)
        {
            //create message
            var message = new MailMessage { Subject = "Application Submitted" };
            message.To.Add(model.LandlordEmail);

            //create model for it
            ViewData = new System.Web.Mvc.ViewDataDictionary(model);

            //render it
            PopulateBody(message, viewName: "SendApplication");

            //send it
            return this.SendMessage(message);
        }
    }

	public class ClientAccountMailer : MailerService, IAccountMailer
	{
		public Status<bool> Register(EmailAccountRegisterModel model)
		{
			return SendMail(model, "account/register");
		}

		public Status<bool> ChangePassword(EmailChangePasswordModel model)
		{
			return SendMail(model, "account/changepassword");
		}

        public Status<bool> ForgotPassword(EmailForgotPasswordModel model)
        {
            return SendMail(model, "account/forgotpassword");
        }
        
        public Status<bool> SendApplication(EmailSendApplicationModel model)
        {
            return SendMail(model, "account/sendapplication");
        }
    }
}
