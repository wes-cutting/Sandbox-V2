using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EFTracingProvider;
using EFCachingProvider;
using System.Diagnostics;
using System.Transactions;
using NewRentlerImport.Extensions;

namespace NewRentlerImport
{
	class Program
	{
		static void Main(string[] args)
		{
			//tracing
			EFTracingProviderConfiguration.RegisterProvider();
			EFCachingProviderConfiguration.RegisterProvider();
			EFTracingProviderConfiguration.LogToConsole = false;

			//execution order
			//BuildingsAndContactInfoPass (buildings, contact infos, users
			//AffiliateUsersPass (apiKeys, affiliateUsers)
			//PhotosPass (takes a really long fucking time)
			//PrimaryPhotosPass
			//AmenitiesPass (basic, withOptions, custom)
			//BuildingAmenitiesPass (probably gonna take a while, 330,000 of them in there.)
			//BuildingAmenitiesWithOptionsPass

			var first = new BuildingsAndContactInfoPass();
			first.LetsDoThis();
			first = null;

			var second = new AffiliateUsersPass();
			second.LetsDoThis();
			second = null;

			Console.WriteLine("Now would be a good time to import the applications, dude.");
			Console.ReadLine();

			var third = new PhotosPass();
			third.LetsDoThis();
			third = null;

			var fourth = new PrimaryPhotosPass();
			fourth.LetsDoThis();
			fourth = null;

			//stop here and run the amenities import.sql script
			Console.WriteLine("Pause here and run the amenities import.sql script");
			Console.ReadLine();

			var fifth = new CustomAmenitiesPass();
			fifth.LetsDoThis();
			fifth = null;

			var sixth = new BuildingAmenitiesPass();
			sixth.LetsDoThis();
			sixth = null;

			var seventh = new BuildingAmenitiesWithOptions();
			seventh.LetsDoThis();
			seventh = null;
		}
	}
}
