using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Transactions;
using NewRentlerImport.Extensions;

namespace NewRentlerImport
{
	public class PhotosPass
	{
		public void LetsDoThis()
		{
			Console.WriteLine("Done with the first set");

			//contexts
			var old = new RentlerBackupEntitiesExtensions();
			var db = new New.RentlerNewEntitiesExtensions();
			var withoutTracing = new New.RentlerNewEntities();

			//diagnostics
			Stopwatch watch = new Stopwatch();

			var buildings = db.Buildings.ToList();

			var photos = old.Photos.ToList()
				.Select(s => s.ToNewPhoto()).OrderBy(o => o.BuildingId).ToList();

			//set sort order for the photos,
			//building by building
			long buildingId = 0;
			int sortOrder = 0;
			foreach(var item in photos)
			{
				if(buildingId == item.BuildingId)
					sortOrder++;
				else
				{
					sortOrder = 0;
					buildingId = item.BuildingId;
				}

				item.SortOrder = sortOrder;
			}

			//store the damn things!
			Console.WriteLine("Adding photos to new db...");
			foreach(var item in photos)
				withoutTracing.Photos.AddObject(item);

			withoutTracing.SaveChanges();
			watch.Stop();
			Console.WriteLine("Done with that. Took {0}", new DateTime(watch.ElapsedTicks).ToString("HH:mm:ss") + ".");
		}
	}
}
