using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Transactions;
using NewRentlerImport.Extensions;

namespace NewRentlerImport
{
	public class CustomAmenitiesPass
	{
		public void LetsDoThis()
		{
			//contexts
			var old = new RentlerBackupEntities();
			var db = new New.RentlerNewEntities();

			//diagnostics
			Stopwatch watch = new Stopwatch();

			var custom = (from s in old.Buildings
						  from p in s.PropertyInfo.CustomAmenities
						  select new { Custom = p, BuildingId = s.BuildingId }).ToList()
							.Select(e => e.Custom.ToNewCustomAmenity(e.BuildingId)).ToList();


			db.Connection.Open();
			var trans = db.Connection.BeginTransaction();
			Console.WriteLine("Turning Identity Insert on");
			db.ExecuteStoreCommand("SET IDENTITY_INSERT CustomAmenities ON");

			foreach(var item in custom)
				db.CustomAmenities.AddObject(item);

			db.SaveChanges(false);

			Console.WriteLine("Turning identity insert off...");
			db.ExecuteStoreCommand("SET IDENTITY_INSERT CustomAmenities OFF");

			Console.WriteLine("Committing transaction...");
			trans.Commit();
			Console.WriteLine("Accepting changes...");
			db.AcceptAllChanges();
			Console.WriteLine("Closing connection...");
			db.Connection.Close();
			Console.WriteLine("Custom Amenities are in.");
			Console.ReadLine();
		}
	}
}
