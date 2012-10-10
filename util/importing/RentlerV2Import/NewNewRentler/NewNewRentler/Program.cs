using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewNewRentler.Importers;
using System.Diagnostics;

namespace NewNewRentler
{
	class Program
	{
		static void Main(string[] args)
		{
			//make sure line 38 of the New.Rentler.edmx xml, this one:
			//<Property Name="BuildingId" Type="bigint" Nullable="false" StoreGeneratedPattern="None" />
			//has StoreGeneratedPattern set to None. Otherwise identity_insert won't work and the world collapses.

			var importers = new List<IRentlerImporter> 
			{ 
				new UsersImporter(),
				new ApiKeysImporter(),
				new AffiliateUsersImporter(),
				new ContactInfosImporter(),
				new BuildingsAndRibbonsImporter(),
				new PhotosImporter(),
				new AmenitiesImporter(),
				new FeaturedListingsImporter(),
				new SavedBuildingsImporter(),
				new OrdersImporter(),
				new UserCreditCardsImporter(),
				new UserInterestsImporter(),
				new ListingCountsImporter()
			};

			var watch = new Stopwatch();
			var times = new Dictionary<string, TimeSpan>();

			var blah = TimeSpan.FromMinutes(2.5);
			for (int i = 0; i < importers.Count; i++)
			{
				Console.WriteLine("Running: {0}", importers[i].GetType().FullName);
				watch.Start();
				importers[i].Import();
				watch.Stop();
				Console.WriteLine("Done running {0}", importers[i].GetType().FullName);
				times.Add(importers[i].GetType().FullName, watch.Elapsed);
				Console.WriteLine("Took: {0}", watch.Elapsed.ToPrettyString());
				Console.WriteLine();
				watch.Reset();
			}

			Console.WriteLine("All done!");
			foreach (var item in times)
				Console.WriteLine("{0} took {1}",
					item.Key, item.Value.ToPrettyString());
			Console.WriteLine();

			TimeSpan totalTime = times.Select(t => t.Value).Aggregate((t1, t2) => t1.Add(t2));
			Console.WriteLine("Total time: {0}", totalTime.ToPrettyString());
			Console.ReadLine();
		}
	}
}
