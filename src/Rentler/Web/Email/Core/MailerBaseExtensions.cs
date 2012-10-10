using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using Mvc.Mailer;

namespace Rentler.Web.Axioms.Extensions
{
	public static class MailerBaseExtensions
	{
		public static Status<bool> SendMessage(this MailerBase mailer, MailMessage message)
		{
			try
			{
				message.Send();
			}
			catch(Exception exc)
			{
				return Status.Error(exc.Message, false);
			}

			return Status.OK(true);
		}
	}
}