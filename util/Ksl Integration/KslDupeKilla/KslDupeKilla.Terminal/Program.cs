using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KslDupeKilla.Terminal
{
	class Program
	{
		static RentlerDataContext context;
		static List<Building> buildings;
		static List<UserContract> userContracts;
		static List<UserInterest> userInterests;
		static List<SavedListing> savedListings;

		class Tester
		{
			public string Bacon { get; set; }
		}

		static void Main(string[] args)
		{
			Tester test = new Tester();
			if (test.Bacon.Equals(""))
				Console.WriteLine("Black");

			Console.ReadLine();
			//context = new RentlerDataContext();

			//// get all the duplicate emails
			//Console.WriteLine("Getting users");

			//var query =
			//    (from u in context.Users
			//    group u by u.Email into g
			//    where g.Count() > 1
			//    select g).ToList();

			//Console.WriteLine("found {0} duplicate emails", query.Count);

			//Console.WriteLine("Getting buildings");

			//buildings = (from b in context.Buildings
			//             select b).ToList();

			//Console.WriteLine("Getting contracts");

			//userContracts = (from uc in context.UserContracts
			//                 select uc).ToList();

			//Console.WriteLine("Getting user interests");

			//userInterests = (from ui in context.UserInterests
			//                 select ui).ToList();

			//Console.WriteLine("Getting saved listings");

			//savedListings = (from ui in context.SavedListings
			//                 select ui).ToList();

			//// iterate the groups
			//foreach(var userGroup in query)
			//{
			//    FixUserGroup(userGroup);
			//}


			//Console.ReadLine();
		}

		static void FixUserGroup(IGrouping<string, User> userGroup)
		{
			// sort them descending
			var list = userGroup.ToList();

			var users = (from l in list
						 orderby l.CreateDate
						 descending
						 select l).ToList();

			Console.WriteLine("Processing {0}", users[0].Email);

			List<UserContract> userConts = new List<UserContract>();
			List<Building> userBuildings = new List<Building>();
			List<UserInterest> landlordInterests = new List<UserInterest>();
			List<UserInterest> tenantInterests = new List<UserInterest>();
			List<SavedListing> userSavedListings = new List<SavedListing>();


			for (int x = 1; x < users.Count; ++x)
			{
				userBuildings.AddRange((from b in buildings
										where b.UserId == users[x].UserId
										select b).ToList());

				userConts.AddRange((from uc in userContracts
										where uc.UserId == users[x].UserId
										select uc).ToList());

				tenantInterests.AddRange((from ui in userInterests
										  where ui.UserId == users[x].UserId
										  select ui).ToList());

				landlordInterests.AddRange((from ui in userInterests
										  where ui.LandlordUserId == users[x].UserId
										  select ui).ToList());

				userSavedListings.AddRange((from ui in savedListings
											where ui.UserId == users[x].UserId
											select ui).ToList());
			}

			Console.WriteLine("Fixing buildings and contracts...");

			for (int x = 0; x < userBuildings.Count; ++x)
			{
				userBuildings[x].UserId = users[0].UserId;
			}
			for (int x = 0; x < userConts.Count; ++x)
			{
				userConts[x].UserId = users[0].UserId;
			}
			for (int x = 0; x < tenantInterests.Count; ++x)
			{
				tenantInterests[x].UserId = users[0].UserId;
			}
			for (int x = 0; x < landlordInterests.Count; ++x)
			{
				landlordInterests[x].LandlordUserId = users[0].UserId;
			}
			for (int x = 0; x < userSavedListings.Count; ++x)
			{
				userSavedListings[x].UserId = users[0].UserId;
			}
			context.SubmitChanges();


			Console.WriteLine("Fixing Affiliate, Auth, and Roles");

			var affiliateUsers = (from a in context.AffiliateUsers
								  where a.User.Email == users[0].Email &&
								  a.User.UserId != users[0].UserId
								  select a).ToList();
			context.AffiliateUsers.DeleteAllOnSubmit(affiliateUsers);

			var authTokens = (from a in context.AuthTokens
								  where a.User.Email == users[0].Email &&
								  a.User.UserId != users[0].UserId
								  select a).ToList();
			context.AuthTokens.DeleteAllOnSubmit(authTokens);

			var roleUsers = (from a in context.RoleUsers
							  where a.User.Email == users[0].Email &&
							  a.User.UserId != users[0].UserId
							  select a).ToList();
			context.RoleUsers.DeleteAllOnSubmit(roleUsers);

			var landlordInfo = (from a in context.LandlordInfos
							 where a.User.Email == users[0].Email &&
							 a.User.UserId != users[0].UserId
							 select a).ToList();
			context.LandlordInfos.DeleteAllOnSubmit(landlordInfo);

			var applicationInfo = (from a in context.ApplicationInfos
								where a.User.Email == users[0].Email &&
								a.User.UserId != users[0].UserId
								select a).ToList();
			context.ApplicationInfos.DeleteAllOnSubmit(applicationInfo);

			var alerts = (from a in context.Alerts
								   where a.User.Email == users[0].Email &&
								   a.User.UserId != users[0].UserId
								   select a).ToList();
			context.Alerts.DeleteAllOnSubmit(alerts);

			for (int x = 1; x < users.Count; ++x)
			{
				context.Users.DeleteOnSubmit(users[x]);
			}

			Console.WriteLine("Committing");

			context.SubmitChanges();
		}
	}
}
