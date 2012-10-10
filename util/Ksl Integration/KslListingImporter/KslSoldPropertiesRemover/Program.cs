using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LINQtoCSV;

namespace KslSoldPropertiesRemover
{
	class Program
	{
		static void Main(string[] args)
		{
			//csv
			var desc = new CsvFileDescription()
			{
				SeparatorChar = ',',
				FirstLineHasColumnNames = true
			};
			var csvContext = new CsvContext();

			//read the stuff in
			Console.WriteLine("Reading CSV files...");
			var classifieds = ReadCsv<Classified>("classifieds.csv", csvContext);
			var homes = ReadCsv<Home>("homes.csv", csvContext);
			var homeCoolings = ReadCsv<HomeCooling>("homes_cooling.csv", csvContext);
			var homeHeatings = ReadCsv<HomeHeating>("homes_heating.csv", csvContext);
			var homeProperties = ReadCsv<HomeProperty>("homes_property.csv", csvContext);
			var members = ReadCsv<Member>("member.csv", csvContext);

			var sold = classifieds.Where(m => m.sold == "1").Select(s => s.sid).ToList();

			var dbContext = new RentlerDataContext();

			Console.WriteLine(dbContext.Connection.ConnectionString);

			var listingsThatShouldHaveBeenMarkedAsSoldInTheFirstPlace =
				(from l in dbContext.Listings
				 where sold.Contains(l.Building.sid) &&
						l.IsActive
				 select l).ToList();

			//mark 'em as deactivated
			Console.WriteLine("Found {0} listings that should be deactivated", listingsThatShouldHaveBeenMarkedAsSoldInTheFirstPlace.Count);
			Console.ReadKey(true);
			Console.WriteLine("Fixing them...");
			listingsThatShouldHaveBeenMarkedAsSoldInTheFirstPlace.ForEach(m => m.IsActive = false);
			dbContext.SubmitChanges();
			Console.WriteLine("Done!");
			Console.ReadKey(true);
		}

		public static List<T> ReadCsv<T>(string filename, CsvContext context) where T : new()
		{
			var desc = new CsvFileDescription()
			{
				SeparatorChar = ',',
				FirstLineHasColumnNames = true
			};

			Console.WriteLine("Reading CSV file {0}...", filename);
			var data = context.Read<T>(string.Format("Csvs\\{0}", filename), desc).ToList();

			return data;
		}


	}
}
