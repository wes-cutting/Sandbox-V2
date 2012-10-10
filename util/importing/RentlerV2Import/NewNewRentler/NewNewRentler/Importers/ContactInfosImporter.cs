using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewNewRentler.Importers
{
	public class ContactInfosImporter : IRentlerImporter
	{
		public void Import()
		{
			//get users for reference
			var newUsers = new Dictionary<Guid, int>();
			using(var newContext = new New.RentlerEntities())
				newUsers = newContext.Users
					.Select(u => new { Key = u.ReferenceId, Value = u.UserId })
					.ToDictionary(x => x.Key, y => y.Value);

			//grab the old contacts
			var oldContacts = new List<Old.ContactInfo>();
			using(var oldContext = new Old.RentlerNewEntities())
				oldContacts = oldContext.ContactInfos.ToList();

			//convert the old to the new
			var newContacts = new List<New.ContactInfo>();
			foreach(var item in oldContacts)
			{
				newContacts.Add(new New.ContactInfo
				{
					CompanyName = item.CompanyName,
					ContactInfoId = (int)item.ContactInfoId,
					Email = item.Email,
					ContactInfoTypeCode = (int)New.ContactInfoTypeConverter.Convert(item.Type),
					Name = item.Name,
					PhoneNumber = item.PhoneNumber,
					ShowEmailAddress = true,
					ShowPhoneNumber = item.DisplayPhoneNumber,
					UserId = newUsers[item.UserId]
				});
			}

			//commit them to the new db, enabling identity_insert
			//for a moment so we can keep the same ids.
			using(var context = new New.RentlerEntities())
				context.BulkInsert(newContacts, true);
		}
	}
}
