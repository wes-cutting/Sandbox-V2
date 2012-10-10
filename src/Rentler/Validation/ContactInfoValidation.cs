using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Rentler.Validation
{
    public class ContactInfoValidation
    {
        public static ValidationResult ValidatePhoneNumber(ContactInfo contact)
        {
            bool isValid = !contact.ShowPhoneNumber || !string.IsNullOrEmpty(contact.PhoneNumber);

            if (isValid)            
                return ValidationResult.Success;            
            else            
                return new ValidationResult("The Phone field is required when it is made visible on the listing.");            
        }
    }
}
