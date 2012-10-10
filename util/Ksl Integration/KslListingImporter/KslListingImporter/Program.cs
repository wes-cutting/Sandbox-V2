using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rentler.Web.Configuration;
using Rentler.Repositories;
using Rentler.Data;
using System.IO;
using System.Data.Entity;
using LINQtoCSV;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.StorageClient;
using System.Configuration;
using System.Threading.Tasks;
using Rentler;
using System.Diagnostics;

namespace KslListingImporter
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

			List<User> users = RentlerConverter.ConvertToUsers(members).ToList();

			var buildings = RentlerConverter.ConvertToBuildings(
				homes, classifieds, homeCoolings,
				homeHeatings, homeProperties, users);

			//todo: remove this, just limiting
			//results so I can do this stuff more quickly.
			buildings = buildings.ToList();
			users = (from u in users
					 where buildings.Any(b => b.UserId.Equals(u.UserId))
					 select u).ToList();

			var landlordInfos = RentlerConverter.CreateLandlordInfos(users);

			//and put them in the db
			var rentlerContext = new RentlerDataContext();

			//find existing users and handle them
			var apiKey = new Guid("DA05C498-C338-4EE4-9A70-6872E38349CC");
			var currentUsers = (from u in rentlerContext.Users
								where u.AffiliateUser.ApiKey == apiKey
								select u).ToList();

			for(int i = 0; i < users.Count(); i++)
			{
				var existingUser = currentUsers.FirstOrDefault(u => u.Email.Equals(users[i].Email));

				if(existingUser == null)
					continue;

				//user exists already, update the building reference
				var buildingsToChange = buildings.Where(b => b.UserId.Equals(users[i].UserId)).ToList();

				foreach(var toChange in buildingsToChange)
					toChange.UserId = existingUser.UserId;

				//remove the user and landlord info from the new list.
				var oldLandlordInfo = landlordInfos.SingleOrDefault(l => l.UserId.Equals(users[i].UserId));
				if(oldLandlordInfo != null)
					landlordInfos.Remove(oldLandlordInfo);
				users.Remove(users[i]);
			}

			Console.WriteLine("Inserting {0} users...", users.Count());
			rentlerContext.Users.InsertAllOnSubmit(users);
			rentlerContext.SubmitChanges();

			Console.WriteLine("Inserting Landlord Info for {0} users...", landlordInfos.Count());
			rentlerContext.LandlordInfos.InsertAllOnSubmit(landlordInfos);
			rentlerContext.SubmitChanges();

			Console.WriteLine("Inserting {0} buildings...", buildings.Count);
			rentlerContext.Buildings.InsertAllOnSubmit(buildings);
			rentlerContext.SubmitChanges();

			Console.WriteLine("Uploading photos");

			//upload photos
			var tasksArray = new Task[buildings.Count()];
			foreach(var item in buildings)
			{
				//queue up all the tasks
				var t = new Task((b) =>
				{
					KslPhotoService.DownloadPhotos(
					(int)((Building)b).BuildingId,
					int.Parse(((Building)b).sid));
				}, item);

				tasksArray[buildings.ToList().IndexOf(item)] = t;
			}

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

			//finally, run the blob uploaders to get this stuff in the cloud
			Console.WriteLine("Launching Blob Uploaders...");
			var process = new Process();
			process.StartInfo.FileName =
				@"C:\Users\Dusda\Projects\Rentler\Ksl Integration\RentlerPhotoUploader\RentlerPhotoUploader\bin\Debug\RentlerPhotoUploader.exe";

			if(ImageCounter.ImageCount < 10000)
			{
				process.StartInfo.Arguments =
					@"""C:\Users\Dusda\Projects\Rentler\Ksl Integration\KslListingImporter\KslListingImporter\bin\Debug\userphotostest\0""";
				process.Start();
			}

			for(int i = 0; i < ImageCounter.ImageCount / 10000; i++)
			{
				process.StartInfo.Arguments =
					@"""C:\Users\Dusda\Projects\Rentler\Ksl%20Integration\KslListingImporter\KslListingImporter\bin\Debug\userphotostest\""" + i;
				process.Start();
			}
		}

		static List<Building> GetBuildingsPlaceholder()
		{
			var context = new RentlerDataContext();
			var buildings = from b in context.Buildings
							//where b.UserId == new Guid("CBDA9A94-5F52-47BF-891F-DCA097D48DA2")
							select b;

			return buildings.ToList();
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

		public static void BulkInsert<T>(List<T> data, string tableName, DbContext context)
		{
			Console.WriteLine("Inserting {0}'s, {1} rows...", tableName, data.Count);

			//set up datasets
			var ds = data.ToDataSet(tableName);
			var source = ds.Tables[tableName];

			//execute bulk copy
			using(SqlConnection conn = new SqlConnection(context.Database.Connection.ConnectionString))
			{
				conn.Open();

				using(SqlBulkCopy copy = new SqlBulkCopy(conn))
				{
					copy.DestinationTableName = tableName;
					copy.WriteToServer(source);
				}

				conn.Close();
			}
		}
	}

	public class KslDumpContext : DbContext
	{
		public KslDumpContext()
			: base("KslDump") { }

		public DbSet<Classified> Classifieds { get; set; }
		public DbSet<Home> Homes { get; set; }
		public DbSet<HomeCooling> HomeCoolings { get; set; }
		public DbSet<HomeHeating> HomeHeatings { get; set; }
		public DbSet<HomeProperty> HomeProperties { get; set; }
		public DbSet<Member> Members { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
		}
	}

	public class Classified
	{
		public int ClassifiedId { get; set; }

		[CsvColumn(FieldIndex = 0)]
		public string tid { get; set; }
		[CsvColumn(FieldIndex = 1)]
		public string cat_nid { get; set; }
		[CsvColumn(FieldIndex = 2)]
		public string nid { get; set; }
		[CsvColumn(FieldIndex = 3)]
		public string sid { get; set; }
		[CsvColumn(FieldIndex = 4)]
		public string status { get; set; }
		[CsvColumn(FieldIndex = 5)]
		public string userid { get; set; }
		[CsvColumn(FieldIndex = 6)]
		public string cmsid { get; set; }
		[CsvColumn(FieldIndex = 7)]
		public string email { get; set; }
		[CsvColumn(FieldIndex = 8)]
		public string title { get; set; }
		[CsvColumn(FieldIndex = 9)]
		public string description { get; set; }
		[CsvColumn(FieldIndex = 10)]
		public string address1 { get; set; }
		[CsvColumn(FieldIndex = 11)]
		public string address2 { get; set; }
		[CsvColumn(FieldIndex = 12)]
		public string city { get; set; }
		[CsvColumn(FieldIndex = 13)]
		public string state { get; set; }
		[CsvColumn(FieldIndex = 14)]
		public string zip { get; set; }
		[CsvColumn(FieldIndex = 15)]
		public string lat { get; set; }
		[CsvColumn(FieldIndex = 16)]
		public string lon { get; set; }
		[CsvColumn(FieldIndex = 17)]
		public string price { get; set; }
		[CsvColumn(FieldIndex = 18)]
		public string obo { get; set; }
		[CsvColumn(FieldIndex = 19)]
		public string image { get; set; }
		[CsvColumn(FieldIndex = 20)]
		public string create_time { get; set; }
		[CsvColumn(FieldIndex = 21)]
		public string expire_time { get; set; }
		[CsvColumn(FieldIndex = 22)]
		public string display_time { get; set; }
		[CsvColumn(FieldIndex = 23)]
		public string price_xtra { get; set; }
		[CsvColumn(FieldIndex = 24)]
		public string sold { get; set; }
		[CsvColumn(FieldIndex = 25)]
		public string business { get; set; }
		[CsvColumn(FieldIndex = 26)]
		public string market_type { get; set; }
		[CsvColumn(FieldIndex = 27)]
		public string stock_num { get; set; }
		[CsvColumn(FieldIndex = 28)]
		public string import_source { get; set; }
	}

	public class Home
	{
		public int HomeId { get; set; }

		[CsvColumn(FieldIndex = 0, Name = "homeid")]
		public string homekslid { get; set; }
		[CsvColumn(FieldIndex = 1)]
		public string aid { get; set; }
		[CsvColumn(FieldIndex = 2)]
		public string addr { get; set; }
		[CsvColumn(FieldIndex = 3)]
		public string city { get; set; }
		[CsvColumn(FieldIndex = 4)]
		public string state { get; set; }
		[CsvColumn(FieldIndex = 5)]
		public string zip { get; set; }
		[CsvColumn(FieldIndex = 6)]
		public string price { get; set; }
		[CsvColumn(FieldIndex = 7)]
		public string year { get; set; }
		[CsvColumn(FieldIndex = 8)]
		public string sqft { get; set; }
		[CsvColumn(FieldIndex = 9)]
		public string acres { get; set; }
		[CsvColumn(FieldIndex = 10)]
		public string bed { get; set; }
		[CsvColumn(FieldIndex = 11)]
		public string bath { get; set; }
		[CsvColumn(FieldIndex = 12)]
		public string garage { get; set; }
		[CsvColumn(FieldIndex = 13)]
		public string property { get; set; }
		[CsvColumn(FieldIndex = 14)]
		public string cooling { get; set; }
		[CsvColumn(FieldIndex = 15)]
		public string heating { get; set; }
		[CsvColumn(FieldIndex = 16)]
		public string foreclosure { get; set; }
		[CsvColumn(FieldIndex = 17)]
		public string saleby { get; set; }
		[CsvColumn(FieldIndex = 18)]
		public string member_id { get; set; }
		[CsvColumn(FieldIndex = 19)]
		public string status { get; set; }
		[CsvColumn(FieldIndex = 20)]
		public string importer { get; set; }
		[CsvColumn(FieldIndex = 21)]
		public string ctime { get; set; }
		[CsvColumn(FieldIndex = 22)]
		public string mtime { get; set; }
		[CsvColumn(FieldIndex = 23)]
		public string photo_count { get; set; }
		[CsvColumn(FieldIndex = 24)]
		public string featured { get; set; }
		[CsvColumn(FieldIndex = 25)]
		public string reservation_url { get; set; }
	}

	public class HomeCooling
	{
		[CsvColumn(FieldIndex = 0)]
		public int id { get; set; }
		[CsvColumn(FieldIndex = 1)]
		public string type { get; set; }
	}

	public class HomeHeating
	{
		[CsvColumn(FieldIndex = 0)]
		public int id { get; set; }
		[CsvColumn(FieldIndex = 1)]
		public string type { get; set; }
	}

	public class HomeProperty
	{
		[CsvColumn(FieldIndex = 0)]
		public int id { get; set; }
		[CsvColumn(FieldIndex = 1)]
		public string type { get; set; }
	}

	public class Member
	{
		[CsvColumn(FieldIndex = 0)]
		public int id { get; set; }
		[CsvColumn(FieldIndex = 1)]
		public string photo_id { get; set; }
		[CsvColumn(FieldIndex = 2)]
		public string group { get; set; }
		[CsvColumn(FieldIndex = 3)]
		public string status { get; set; }
		[CsvColumn(FieldIndex = 4)]
		public string first { get; set; }
		[CsvColumn(FieldIndex = 5)]
		public string last { get; set; }
		[CsvColumn(FieldIndex = 6)]
		public string street1 { get; set; }
		[CsvColumn(FieldIndex = 7)]
		public string street2 { get; set; }
		[CsvColumn(FieldIndex = 8)]
		public string city { get; set; }
		[CsvColumn(FieldIndex = 9)]
		public string state { get; set; }
		[CsvColumn(FieldIndex = 10)]
		public string zip { get; set; }
		[CsvColumn(FieldIndex = 11)]
		public string url { get; set; }
		[CsvColumn(FieldIndex = 12)]
		public string priPhone { get; set; }
		[CsvColumn(FieldIndex = 13)]
		public string altPhone { get; set; }
		[CsvColumn(FieldIndex = 14)]
		public string gender { get; set; }
		[CsvColumn(FieldIndex = 15)]
		public string dob { get; set; }
		[CsvColumn(FieldIndex = 16)]
		public string ssn { get; set; }
		[CsvColumn(FieldIndex = 17)]
		public string email { get; set; }
		[CsvColumn(FieldIndex = 18)]
		public string emailValid { get; set; }
		[CsvColumn(FieldIndex = 19)]
		public string emailAllow { get; set; }
		[CsvColumn(FieldIndex = 20)]
		public string emailHTML { get; set; }
		[CsvColumn(FieldIndex = 21)]
		public string emailFail { get; set; }
		[CsvColumn(FieldIndex = 22)]
		public string screenname { get; set; }
		[CsvColumn(FieldIndex = 23)]
		public string signature { get; set; }
		[CsvColumn(FieldIndex = 24)]
		public string password { get; set; }
		[CsvColumn(FieldIndex = 25)]
		public string passwordHash { get; set; }
		[CsvColumn(FieldIndex = 26)]
		public string persistent { get; set; }
		[CsvColumn(FieldIndex = 27)]
		public string loginTotal { get; set; }
		[CsvColumn(FieldIndex = 28)]
		public string loginTime { get; set; }
		[CsvColumn(FieldIndex = 29)]
		public string createTime { get; set; }
		[CsvColumn(FieldIndex = 30)]
		public string importId { get; set; }
		[CsvColumn(FieldIndex = 31)]
		public string importSrc { get; set; }
		[CsvColumn(FieldIndex = 32)]
		public string importRef { get; set; }
	}
}
