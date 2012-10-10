using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewRentlerImport.Extensions
{
	public static class OldLandlordInfoExtensions
	{
		public static New.ContactInfo ToNewContactInfo(this LandlordInfo info)
		{
			return new New.ContactInfo
			{
				DisplayPhoneNumber = info.AllowPhone,
				Email = info.User.Email,
				Name = info.User.FirstName + " " + info.User.LastName,
				PhoneNumber = info.PhoneNumber,
				UserId = info.UserId,
				Type = "Personal"
			};
		}
	}
}
