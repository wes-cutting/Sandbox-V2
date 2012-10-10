using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.StorageClient;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Configuration;
using System.Diagnostics;
using System.Net;
using System.Threading;
using System.Text.RegularExpressions;

namespace RentlerPhotoUploader
{
	class Program
	{
		static CloudStorageAccount account;
		static CloudBlobClient client;
		static CloudBlobContainer container;

		static void Main(string[] args)
		{
			if(args == null)
				return;

			//var path = "C:\\Users\\Dusda\\Projects\\KslIntegration\\KslListingImporter\\KslListingImporter\\bin\\Debug\\userphotostest\\0";
			var path = Path.Combine(System.Reflection.Assembly.GetExecutingAssembly().Location).Replace("RentlerPhotoUploader.exe", "");


			//configure azure blob storage
			CloudStorageAccount.SetConfigurationSettingPublisher((configName, configSetter) =>
			{
				configSetter(ConfigurationManager.ConnectionStrings[configName].ConnectionString);
			});

			CreateContainer();

			//disable Nagling, to boost latency performance
			//for high volume, small file size transfers
			ServicePoint blobServicePoint = ServicePointManager.FindServicePoint(account.BlobEndpoint);
			blobServicePoint.UseNagleAlgorithm = false;


			Console.WriteLine("Enumerating images...this will take a while...");
			var files = new List<string>();
			var folders = Directory.EnumerateDirectories(path).ToList();

			foreach(var item in folders)
				files.AddRange(Directory.EnumerateFiles(item).ToList());

			Console.WriteLine("Starting upload...");
			var page = 1;
			var size = 10;
			var hasNextPage = true;
			PaginatedList<string> fileset = null;

			//set up a stopwatch to estimate ETA
			var stopwatch = new Stopwatch();
			var execTimes = new List<double>();
			while(hasNextPage)
			{
				//file looks like C:\\blah\\userphotostest\\0\\{BuildingId}\\blah.jpg

				fileset = new PaginatedList<string>(files.AsQueryable(), page, size);
				Console.WriteLine("Page {0} of {1}...", page, fileset.TotalPages);

				stopwatch.Reset();
				stopwatch.Start();

				foreach(var item in fileset)
				{
					var img = Image.FromFile(item);
					string buildingId = new FileInfo(item).Directory.Name;
					UploadPhoto(buildingId, img, item);

					Console.Write("\rFinished image: {0}", Path.GetFileName(item));
				}
				Console.WriteLine();

				stopwatch.Stop();
				execTimes.Add(stopwatch.Elapsed.TotalSeconds);
				Console.WriteLine("Took {0}sec, ETA is in {1}hrs, {2}mins, at {3}",
					stopwatch.Elapsed.TotalSeconds.ToString("N"),
					DateTime.Now.AddSeconds(execTimes.Average() * (fileset.TotalPages - page)).Subtract(DateTime.Now).Hours,
					DateTime.Now.AddSeconds(execTimes.Average() * (fileset.TotalPages - page)).Subtract(DateTime.Now).Minutes,
					String.Format("{0:f}", DateTime.Now.AddSeconds(execTimes.Average() * (fileset.TotalPages - page))));
				Console.WriteLine("Average Execution: {0} seconds", execTimes.Average().ToString("N"));

				hasNextPage = fileset.HasNextPage;
				page++;
			}
		}

		static void CreateContainer()
		{
			//get blob container
			account = CloudStorageAccount.FromConfigurationSetting("BlobConnectionString");
			client = account.CreateCloudBlobClient();
			container = client.GetContainerReference("userphotos");
			container.CreateIfNotExist();

			BlobContainerPermissions permissions = new BlobContainerPermissions()
			{
				PublicAccess = BlobContainerPublicAccessType.Blob
			};

			container.SetPermissions(permissions);
		}

		static void UploadPhoto(string buildingId, Image pic, string filename)
		{
			try
			{
				using(MemoryStream ms = new MemoryStream())
				{
					pic.Save(ms, pic.RawFormat);
					ms.Position = 0;

					//create blob
					var blob = container.GetBlobReference(buildingId + "/" + Path.GetFileName(filename));
					blob.Metadata["Filename"] = Path.GetFileName(filename);
					blob.Properties.ContentType = MimeHandler.GetContentType(Path.GetExtension(filename));

					BlobRequestOptions options = new BlobRequestOptions();
					options.RetryPolicy = RetryPolicies.RetryAlways();

					blob.UploadFromStream(ms, options);

					blob.SetMetadata();
					blob.SetProperties();
				}
				pic.Dispose();
				pic = null;
			}
			catch(Exception exc)
			{
				Console.WriteLine(exc.Message);
				//probably got an IO Exception, from hammering
				//Azure. No problem, just wait a few minutes.
				Console.WriteLine("Sleeping for 3 minutes...");
				Thread.Sleep(180000);
				UploadPhoto(buildingId, pic, filename);
			}
		}
	}

	public class UploadCallbackObject
	{
		public string Filename { get; set; }
		public CloudBlob Blob { get; set; }
		public List<string> FileSet { get; set; }
	}
}
