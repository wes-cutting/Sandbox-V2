using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewNewRentler.Importers
{
	public class ListingCountsImporter : IRentlerImporter
	{
		/*
			1) "rentler:views:listingsearchviews"
			2) "rentler:views:listingviews"
		 */

		/*
			1) "rentler:authtokens"
			2) "rentler:apikeys"
			3) "rentler:views:listingsearchviews"
			4) "rentler:affiliateusers"
			5) "rentler:views:totalsearchviews"
			6) "rentler:views:listingviews"
			7) "rentler:zipinfos"
		 */

		public void Import()
		{
			//
			//	Listing Page Views
			//

			//connect to the source db
			ConnectionGateway.App = new OldApp();
			var con = ConnectionGateway.Current.GetNewConnection();

			//grab all of the listing page views
			Console.WriteLine("Grabbing page views from Redis...");
			var pageViewsTask = con.Hashes.GetAll(ConnectionGateway.App.RedisDatabase, "rentler:views:listingviews");
			var result = con.Wait(pageViewsTask);

			//switch to target db
			ConnectionGateway.App = new NewApp();
			con = ConnectionGateway.Current.GetNewConnection();

			//dump all of them into the target db
			Console.WriteLine("Dumping them into target Redis db...");
			var pageViewsSetTask = con.Hashes.Set(ConnectionGateway.App.RedisDatabase, "rentler:views:listingviews", result);
			con.Wait(pageViewsSetTask);

			//
			//	Listing Search Views
			//

			//connect to source db
			ConnectionGateway.App = new OldApp();
			con = ConnectionGateway.Current.GetNewConnection();

			//grab all listing search views
			Console.WriteLine("Grabbing search views from Redis...");
			var searchViewsTask = con.Hashes.GetAll(ConnectionGateway.App.RedisDatabase, "rentler:views:listingsearchviews");
			var searchViewsResult = con.Wait(searchViewsTask);

			//switch to target db
			ConnectionGateway.App = new NewApp();
			con = ConnectionGateway.Current.GetNewConnection();

			//dump all of them into the target db
			Console.WriteLine("Dumping them into target Redis db...");
			var searchViewsSetTask = con.Hashes.Set(ConnectionGateway.App.RedisDatabase, "rentler:views:listingsearchviews", searchViewsResult);
			con.Wait(searchViewsSetTask);
		}
	}
}
