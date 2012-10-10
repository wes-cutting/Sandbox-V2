using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rentler.Adapters;
using Rentler.Web.Email;
using Rentler.Redis;
using Newtonsoft.Json;
using System.IO;
using Rentler.Extensions;
using System.Diagnostics;

namespace Rentler.Fiddle
{
	class Program
	{
		static void Main(string[] args)
		{
			var listingAdapter = new ListingAdapter(
				new FriendlyZipAdapter(),
				new ServerListingMailer());

			var settings = new JsonSerializerSettings
			{
				PreserveReferencesHandling = PreserveReferencesHandling.Objects
			};

			var con = ConnectionGateway.Current.GetConnection();

			var all = listingAdapter.GetListings();

			//foreach (var item in all.Result)
			//{
			//    Console.WriteLine(item.BuildingId);
			//    var building = item;



			//    var toStore = JsonConvert.SerializeObject(building, settings);

			//    var task = con.Strings.Set(4,
			//        CacheKeys.LISTING + building.BuildingId,
			//        toStore);
			//    con.Wait(task);
			//}

			var watch = new Stopwatch();

			watch.Start();
			foreach (var item in all.Result)
			{
				var building = item;
				//try to get it
				var getTask = con.Strings.GetString(4, CacheKeys.LISTING + building.BuildingId);
				var result = con.Wait(getTask);

				Building b = JsonConvert.DeserializeObject<Building>(result, settings);

				Console.WriteLine(b.BuildingId + ": " + b.Address1);
			}
			watch.Stop();
			Console.WriteLine(all.Result.Count + " buildings");
			Console.WriteLine(watch.Elapsed.TotalMilliseconds / all.Result.Count + " per property");
			Console.ReadLine();
		}
	}
}
