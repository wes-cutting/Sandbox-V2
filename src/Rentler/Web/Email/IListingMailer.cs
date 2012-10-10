using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rentler.Web.Email
{
	public interface IListingMailer
	{
		Status<bool> Interested(EmailListingInterestedModel model);
	}

	public class EmailListingInterestedModel
	{
        public EmailListingInterestedModel() { }
        
        public EmailListingInterestedModel(UserInterest interest)
        {            
            this.Address1 = interest.Building.Address1;
            this.Address2 = interest.Building.Address2;
            this.City = interest.Building.City;
            this.State = interest.Building.State;
            this.Zip = interest.Building.Zip;
            this.LandlordEmail = interest.Building.User.Email;            
            this.LandlordName = string.Format("{0} {1}", interest.Building.User.FirstName, interest.Building.User.LastName);
            this.LeadEmail = interest.User.Email;
            this.LeadName = string.Format("{0} {1}", interest.User.FirstName, interest.User.LastName);
            this.LeadId = interest.UserInterestId;
            this.Text = interest.Message;
        }

        public string LandlordName { get; set; }
        public string LandlordEmail { get; set; }
        
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
       
        public int LeadId { get; set; }
        public string LeadName { get; set; }
        public string LeadEmail { get; set; }

        public string Text { get; set; }
	}
}
