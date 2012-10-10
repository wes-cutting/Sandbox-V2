using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewRentlerImport.Extensions
{
	public static class OldUserExtensions
	{
		public static New.User ToNewUser(this User user)
		{
			return new New.User
			{
				UserId = user.UserId,
				Username = user.Username,
				PasswordHash = user.PasswordHash,
				Email = user.Email,
				FirstName = user.FirstName,
				LastName = user.LastName,
				PhoneNumber = user.PhoneNumber,
				UpdateDate = user.UpdateDate,
				UpdatedBy = user.UpdatedBy,
				CreateDate = user.CreateDate,
				CreatedBy = user.CreatedBy,
				IsLandlord = user.IsLandlord,
				IsDeleted = user.IsDeleted
			};
		}
	}
}
