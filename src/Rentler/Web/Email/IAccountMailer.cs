using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rentler.Web.Email
{
	public interface IAccountMailer
	{
		Status<bool> Register(EmailAccountRegisterModel model);
		Status<bool> ChangePassword(EmailChangePasswordModel model);
        Status<bool> ForgotPassword(EmailForgotPasswordModel model);
        Status<bool> SendApplication(EmailSendApplicationModel model);
	}

	public class EmailAccountRegisterModel
	{
        public string To { get; set; }
		public string Name { get; set; }
	}

	public class EmailChangePasswordModel
	{
		public string To { get; set; }
		public string Name { get; set; }
	}

    public class EmailForgotPasswordModel
    {
        public string To { get; set; }
        public string Username { get; set; }
        public string NewPassword { get; set; }
    }

    public class EmailSendApplicationModel
    {
        public EmailSendApplicationModel(UserInterest lead)
        {
            LandlordEmail = lead.Building.User.Email;
            LandlordFirstName = lead.Building.User.FirstName;
            LeadName = string.Format("{0} {1}", lead.User.FirstName, lead.User.LastName);
            BuildingId = lead.BuildingId;
        }

        public string LandlordEmail { get; set; }
        public string LandlordFirstName { get; set; }
        public string LeadName { get; set; }
        public long BuildingId { get; set; }
    }
}
