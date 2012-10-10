using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rentler.Web.Email
{
    public interface IPropertyMailer
    {
        Status<bool> RequestApplication(EmailRequestApplicationModel model);
    }

    public class EmailRequestApplicationModel
    {
        public EmailRequestApplicationModel() { }

        public EmailRequestApplicationModel(UserInterest interest)
        {            
            this.Address1 = interest.Building.Address1;
            this.Address2 = interest.Building.Address2;
            this.City = interest.Building.City;
            this.State = interest.Building.State;
            this.Zip = interest.Building.Zip;
            this.LandlordEmail = interest.Building.User.Email;            
            this.LeadEmail = interest.User.Email;
            this.LeadFirstName = interest.User.FirstName;
            this.LeadId = interest.UserInterestId;
            this.Text = interest.ResponseMessage;
        }
        
        public string LandlordEmail { get; set; }
        
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
       
        public int LeadId { get; set; }
        public string LeadFirstName { get; set; }
        public string LeadEmail { get; set; }

        public string Text { get; set; }
    }
}
