﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewNewRentler.New
{
	/// <summary>
	/// Configuration for all the amenities used within the system.
	/// Implemented as a Singleton for universal access and multithreaded
	/// instantiation.
	/// </summary>
	public sealed class Amenities
	{
		private static volatile Amenities instance;
		private static object syncRoot = new Object();

		public Dictionary<string, string> Property { get; set; }
		public Dictionary<string, string> Community { get; set; }
		public Dictionary<string, Dictionary<string, string>> OptionsProperties { get; set; }
		public Dictionary<string, Dictionary<string, string>> OptionsCommunity { get; set; }

		/// <summary>
		/// Prevents a default instance of the <see cref="Amenities"/> class from being created.
		/// </summary>
		private Amenities()
		{
			// instantiate the objects
			this.Property = new Dictionary<string, string>();
			this.Community = new Dictionary<string, string>();
			this.OptionsProperties = new Dictionary<string, Dictionary<string, string>>();
			this.OptionsCommunity = new Dictionary<string, Dictionary<string, string>>();

			// populate the objects
			this.ConfigurePropertyAmenities();
			this.ConfigureCommunityAmenities();
			this.ConfigureOptionsProperties();
			this.ConfigureOptionsCommunity();
		}

		public bool IsValidAmenity(string amenityId)
		{
			if(string.IsNullOrEmpty(amenityId))
				return false;

			if(Property.ContainsKey(amenityId))
				return true;
			if(Community.ContainsKey(amenityId))
				return true;

			foreach(var key in OptionsProperties.Keys)
			{
				if(OptionsProperties[key].ContainsKey(amenityId))
					return true;
			}
			foreach(var key in OptionsCommunity.Keys)
			{
				if(OptionsCommunity[key].ContainsKey(amenityId))
					return true;
			}

			return false;
		}

		/// <summary>
		/// Configures the property amenities.
		/// </summary>
		private void ConfigurePropertyAmenities()
		{
			Property.Add("cablesat", "Cable/Satellite");
			Property.Add("dishwasher", "Dishwasher");
			Property.Add("washerdryer", "Washer/Dryer");
			Property.Add("carpet", "Carpet");
			Property.Add("fireplace", "Fireplace");
			Property.Add("highceilings", "High Ceilings");
			Property.Add("microwave", "Microwave");
			Property.Add("patiobalcony", "Patio/Balcony");
			Property.Add("view", "View");
			Property.Add("hardwood", "Hardwood Floors");
			Property.Add("remodeled", "Newly Remodeled");
			Property.Add("fenced", "Fenced Yard");
			Property.Add("washerdryerhookups", "Washer/Dryer Hookups");
			Property.Add("newpaint", "New Paint");
			Property.Add("newcarpet", "New Carpet");
			Property.Add("granite", "Granite Countertops");
			Property.Add("walkinclosets", "Walk-In Closets");
			Property.Add("tile", "Tile");
			Property.Add("storage", "Storage");
			Property.Add("pool", "Pool");
			Property.Add("hottub", "Hot Tub");
		}

		/// <summary>
		/// Configures the community amenities.
		/// </summary>
		private void ConfigureCommunityAmenities()
		{
			Community.Add("communityfitness", "Fitness Center");
			Community.Add("communitytennis", "Tennis Court");
			Community.Add("communitypark", "Near Park");
			Community.Add("communityclubhouse", "Clubhouse");
			Community.Add("communitybasketball", "Basketball Court");
			Community.Add("communitylaundry", "Onsite Laundry");
			Community.Add("communitypool", "Pool");
			Community.Add("communityplayground", "Playground");
			Community.Add("communitymanager", "Onsite Manager");
			Community.Add("communityhottub", "Hot Tub");
			Community.Add("communitybbq", "Bbq Area");
			Community.Add("communitymaintenance", "Onsite Maintenance");
		}

		/// <summary>
		/// Configures the options properties.
		/// </summary>
		private void ConfigureOptionsProperties()
		{
			/*
			 * Dictionary<string, string> coveredParking = new Dictionary<string, string>();
            coveredParking.Add("coveredparking2cars", "2 Cars");
            coveredParking.Add("coveredparking3cars", "3 Cars");
            this.OptionsCommunity.Add("Covered Parking", coveredParking);*/

			var air = new Dictionary<string, string>();
			air.Add("centralair", "Central Air");
			air.Add("windowunit", "Window Unit");
			air.Add("evaporativecooler", "Evaporative Cooler");
			air.Add("other", "Other");
			OptionsProperties.Add("Air Conditioning", air);
		}

		/// <summary>
		/// Configures the options community.
		/// </summary>
		private void ConfigureOptionsCommunity()
		{
			var covered = new Dictionary<string, string>();
			covered.Add("coveredparking1car", "1 Car");
			covered.Add("coveredparking2cars", "2 Cars");
			covered.Add("coveredparking3+cars", "3+ Cars");
			OptionsCommunity.Add("Covered Parking", covered);

			var street = new Dictionary<string, string>();
			street.Add("streetparking1car", "1 Car");
			street.Add("streetparking2cars", "2 Cars");
			street.Add("streetparking3+cars", "3+ Cars");
			OptionsCommunity.Add("Street Parking", street);

		}


		/// <summary>
		/// Gets the current instance. Uses
		/// the double locking approach as it is safe and handled in .Net
		/// without volitility.
		/// </summary>
		public static Amenities Current
		{
			get
			{
				if(instance == null)
				{
					lock(syncRoot)
					{
						if(instance == null)
							instance = new Amenities();
					}
				}
				return instance;
			}
		}
	}
}
