using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data.EntityClient;

namespace NewNewRentler.Importers
{
	public class UsersImporter : IRentlerImporter
	{
		public void Import()
		{
			var oldUsers = new List<Old.User>();

			Console.WriteLine("Getting old users...");
			using(var oldContext = new Old.RentlerNewEntities())
				oldUsers = oldContext.Users.ToList();

			var newUsers = new List<New.User>();

			Console.WriteLine("Converting them to the new stuff...");
			foreach(var item in oldUsers)
				newUsers.Add(new New.User
				{
					CreateDateUtc = item.CreateDate,
					Email = item.Email,
					FirstName = item.FirstName,
					IsDeleted = item.IsDeleted,
					LastName = item.LastName,
					PasswordHash = item.PasswordHash,
					UpdateDateUtc = item.UpdateDate ?? item.CreateDate,
					UpdatedBy = item.UpdatedBy ?? "sql script",
					ReferenceId = item.UserId,
					Username = item.Username
				});

			Console.WriteLine("Inserting {0} new users", newUsers.Count);

			using(var context = new New.RentlerEntities())
				context.BulkInsert(newUsers);

			Console.WriteLine("Done with that noise! Woo.");
		}
	}
}
