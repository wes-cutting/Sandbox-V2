using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Transactions;
using NewRentlerImport.Extensions;

namespace NewRentlerImport
{
	public class PrimaryPhotosPass
	{
		public void LetsDoThis()
		{
			//contexts
			var old = new RentlerBackupEntitiesExtensions();
			var db = new New.RentlerNewEntitiesExtensions();
			var withoutTracing = new New.RentlerNewEntities();

			//diagnostics
			Stopwatch watch = new Stopwatch();

			Console.WriteLine("Getting photos");
			var photos = withoutTracing.Photos.ToList();
			var buildings = withoutTracing.Buildings.ToList();


			watch.Reset();
			Console.WriteLine("Starting watch");
			watch.Start();
			//add primary photos to buildings
			Console.WriteLine("Adding primary photos to buildings...");
			var primary = photos.Where(p => p.SortOrder == 0).ToList();

			foreach(var item in primary)
			{
				var b = buildings.FirstOrDefault(f => f.BuildingId == item.BuildingId);
				if(b != null)
				{
					b.PrimaryPhotoExtension = item.Extension;
					b.PrimaryPhotoId = item.PhotoId;
				}
			}
			Console.WriteLine("There we go, submitting changes...");
			withoutTracing.SaveChanges();
			Console.WriteLine("Done! Took {0}", new DateTime(watch.ElapsedTicks).ToString("HH:mm:ss") + ".");

			Console.ReadLine();
		}
	}
}
