using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rentler.Data;
using Rentler;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.WindowsAzure;
using System.Configuration;

namespace KslImageRepair
{
	class Program
	{
		static void Main(string[] args)
		{
			//configure azure blob storage
			CloudStorageAccount.SetConfigurationSettingPublisher((configName, configSetter) =>
			{
				configSetter(ConfigurationManager.ConnectionStrings[configName].ConnectionString);
			});

			var context = new RentlerDataContext();

			//pull buildings
			var buildings = (from b in context.Buildings
							 where b.sid != null
							 select b).ToList();

			//download images using matched buildingid and sid
			var tasksArray = new Task[buildings.Count()];
			foreach(var item in buildings)
			{
				//queue up all the tasks
				var t = new Task((b) =>
				{
					//store them in the archive the photo uploader understands
					KslPhotoService.DownloadPhotos(
						((Building)b).BuildingId,
						int.Parse(((Building)b).sid));
				}, item);

				tasksArray[buildings.ToList().IndexOf(item)] = t;
			}

			//Run that script on the right over there ----->
			
			//get it started! 5 at a time.
			int page = 1;
			PaginatedList<Task> tasks = null;
			var hasNextPage = true;
			var stopwatch = new Stopwatch();
			var execTimes = new List<double>();
			while(hasNextPage)
			{
				tasks = new PaginatedList<Task>(tasksArray.AsQueryable(), page, 5);
				Console.WriteLine("Processing page {0} of {1}, {2} at a time",
					page, tasks.TotalPages, tasks.PageSize);

				stopwatch.Reset();
				stopwatch.Start();

				tasks.Each(t => t.Start());
				Task.WaitAll(tasks.ToArray());
				hasNextPage = tasks.HasNextPage;
				page++;

				stopwatch.Stop();

				execTimes.Add(stopwatch.Elapsed.TotalSeconds);
				Console.WriteLine("Done, took {0} seconds, estimated completion time is {1}",
					stopwatch.Elapsed.TotalSeconds,
					DateTime.Now.AddSeconds(execTimes.Average() * (tasks.TotalPages - page)).ToLongTimeString());
				Console.WriteLine("Average execution time is {0}", execTimes.Average());
				Console.WriteLine("{0} images processed", ImageCounter.ImageCount);
			}

			Console.WriteLine();
			Console.WriteLine("Done!");

			//???

			//profit
		}
	}
}
