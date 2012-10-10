using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewNewRentler.Importers
{
	public class AffiliateUsersImporter : IRentlerImporter
	{
		public void Import()
		{
			var users = new List<New.User>();

			Console.WriteLine("Grabbing users to get affiliates");
			using(var context = new New.RentlerEntities())
				users = context.Users.ToList();

			var oldaf = new List<Old.AffiliateUser>();
			using(var context = new Old.RentlerNewEntities())
				oldaf = context.AffiliateUsers.ToList();

			Console.WriteLine("Building reference map...");
			var dict = users.Select(u => new { Key = u.ReferenceId, Value = u.UserId })
				.ToDictionary(x => x.Key, x => x.Value);

			Console.WriteLine("Building new Affiliates...");
			var newaf = new List<New.AffiliateUser>();
			foreach(var item in oldaf)
			{
				newaf.Add(new New.AffiliateUser
				{
					AffiliateUserKey = item.AffiliateUserKey,
					ApiKey = item.ApiKey,
					IsDeleted = item.IsDeleted,
					PasswordHash = item.PasswordHash,
					UserId = dict[item.UserId]
				});
			}

			Console.WriteLine("Adding them to the new db...");

			using(var context = new New.RentlerEntities())
				context.BulkInsert(newaf);
		}
	}
}
