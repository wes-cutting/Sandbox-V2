using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Transactions;
using NewRentlerImport.Extensions;

namespace NewRentlerImport
{
	public class BuildingsAndContactInfoPass
	{
		public void LetsDoThis()
		{
			//contexts
			var old = new RentlerBackupEntitiesExtensions();
			var db = new New.RentlerNewEntitiesExtensions();
			var withoutTracing = new New.RentlerNewEntities();

			//diagnostics
			Stopwatch watch = new Stopwatch();


			Console.WriteLine("Starting watch");
			watch.Start();
			Console.WriteLine("Getting old users...");
			//get old users, move them to the new shit
			var users = (from u in old.Users
						 select u).ToList().Select(s => s.ToNewUser()).ToList();

			Console.WriteLine("Adding them to the db...");
			foreach(var item in users)
				db.Users.AddObject(item);

			Console.WriteLine("Saving changes...");
			db.SaveChanges();
			Console.WriteLine("Done! Took " + new DateTime(watch.ElapsedTicks).ToString("HH:mm:ss") + ".");
			watch.Reset();


			Console.WriteLine("Starting watch");
			watch.Start();
			Console.WriteLine("Getting landlord infos, and making them not suck...");
			//get old landlord infos, convert them to contactinfos
			var contactinfos = (from u in old.LandlordInfoes
								select u).ToList().Select(s => s.ToNewContactInfo()).ToList();

			Console.WriteLine("Adding them to the db...");
			foreach(var item in contactinfos)
				db.ContactInfos.AddObject(item);
			db.SaveChanges();

			Console.WriteLine("Done! Took: " + new DateTime(watch.ElapsedTicks).ToString("HH:mm:ss") + ".");
			watch.Reset();


			Console.WriteLine("Starting watch...");
			watch.Start();
			Console.WriteLine("Getting buildings...");
			//get old buildings, with property info, and listing
			var buildings = (from b in old.Buildings
								 .Include("PropertyInfo").Include("Listings").Include("Listings.ListingCount")
							 select b).ToList().Select(s => s.ToOldInfo().ConvertToBuilding()).ToList();

			//add contact info id to it
			Console.WriteLine("Adding contact infos to the buildings...");
			foreach(var item in buildings)
			{
				var info = contactinfos.FirstOrDefault(c => c.UserId == item.UserId);
				if(info != null)
					item.ContactInfoId = info.ContactInfoId;
				else
				{
					Console.WriteLine("Found one without a landlord info, fixing it...");
					var user = users.Single(u => u.UserId == item.UserId);
					var inf = new New.ContactInfo
					{
						Email = user.Email,
						Name = user.FirstName + " " + user.LastName,
						PhoneNumber = user.PhoneNumber,
						DisplayPhoneNumber = false,
						Type = "Personal",
						UserId = user.UserId
					};
					db.ContactInfos.AddObject(inf);
					db.SaveChanges();
					item.ContactInfoId = inf.ContactInfoId;
					Console.WriteLine("Fixed. Moving on...");
				}
			}

			Console.WriteLine("Opening connection...");
			withoutTracing.Connection.Open();
			Console.WriteLine("Starting transaction...");
			var trans = withoutTracing.Connection.BeginTransaction();
			Console.WriteLine("Turning Identity Insert on");
			withoutTracing.ExecuteStoreCommand("SET IDENTITY_INSERT Buildings ON");

			//foreach(var item in buildings)
			//    withoutTracing.Buildings.AddObject(item);

			for(int i = 0; i < buildings.Count / 2; i++)
			{
				withoutTracing.Buildings.AddObject(buildings[i]);
			}

			Console.WriteLine("Storing buildings...");
			withoutTracing.SaveChanges(false);

			Console.WriteLine("Turning identity insert off...");
			withoutTracing.ExecuteStoreCommand("SET IDENTITY_INSERT Buildings OFF");

			Console.WriteLine("Committing transaction...");
			trans.Commit();
			Console.WriteLine("Accepting changes...");
			withoutTracing.AcceptAllChanges();
			Console.WriteLine("Closing connection...");
			withoutTracing.Connection.Close();

			watch.Stop();
			Console.WriteLine("Buildings are done, on to photos!" + new DateTime(watch.ElapsedTicks).ToString("HH:mm:ss"));



			Console.WriteLine("Opening connection...");
			withoutTracing.Connection.Open();
			Console.WriteLine("Starting transaction...");
			trans = withoutTracing.Connection.BeginTransaction();
			Console.WriteLine("Turning Identity Insert on");
			withoutTracing.ExecuteStoreCommand("SET IDENTITY_INSERT Buildings ON");


			for(int i = (buildings.Count / 2); i < buildings.Count; i++)
			{
				withoutTracing.Buildings.AddObject(buildings[i]);
			}

			Console.WriteLine("Storing buildings...");
			withoutTracing.SaveChanges(false);

			Console.WriteLine("Turning identity insert off...");
			withoutTracing.ExecuteStoreCommand("SET IDENTITY_INSERT Buildings OFF");

			Console.WriteLine("Committing transaction...");
			trans.Commit();
			Console.WriteLine("Accepting changes...");
			withoutTracing.AcceptAllChanges();
			Console.WriteLine("Closing connection...");
			withoutTracing.Connection.Close();

			watch.Stop();
			Console.WriteLine("Buildings are done, on to photos!" + new DateTime(watch.ElapsedTicks).ToString("HH:mm:ss"));


			//photos!
			watch.Reset();
			Console.WriteLine("Starting watch");
			watch.Start();
		}
	}
}
