using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Transactions;
using NewRentlerImport.Extensions;

namespace NewRentlerImport
{
	public class AffiliateUsersPass
	{
		public void LetsDoThis()
		{
			//contexts
			var old = new RentlerBackupEntities();
			var db = new New.RentlerNewEntities();

			//diagnostics
			Stopwatch watch = new Stopwatch();

			Console.WriteLine("Getting apiKeys");
			var apiKeys = old.ApiKeys.ToList().Select(s => new New.ApiKey
			{
				ApiKeyId = s.ApiKeyId,
				IpAddress = s.IpAddress,
				IsDeleted = s.IsDeleted,
				Name = s.Name,
				UserId = s.UserId
			});

			Console.WriteLine("Adding them to the db.");
			foreach(var item in apiKeys)
				db.ApiKeys.AddObject(item);

			db.SaveChanges();
			Console.WriteLine("Done, moving on to affiliate users");

			var affiliateUsers = old.AffiliateUsers.ToList().Select(s => new New.AffiliateUser
			{
				AffiliateUserKey = s.AffiliateUserKey,
				ApiKey = s.ApiKey,
				IsDeleted = s.IsDeleted,
				PasswordHash = s.PasswordHash,
				UserId = s.UserId
			});

			Console.WriteLine("Got 'em, adding to db");
			foreach(var item in affiliateUsers)
				db.AffiliateUsers.AddObject(item);

			db.SaveChanges();
			Console.WriteLine("Done!");
		}
	}
}
