using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewNewRentler.Importers
{
	public class AmenitiesImporter : IRentlerImporter
	{
		public void Import()
		{
			//This one does all of the amenities stuff

			var current = New.Amenities.Current;

			/*
			amenityid	name	category
			3	Cable/Satellite	Property
			4	Dishwasher	Property
			6	Washer/Dryer	Property
			7	Pool	Community
			8	Hot Tub	Community
			10	Carpet	Property
			11	Fireplace	Property
			12	High Ceilings	Property
			13	Microwave	Property
			14	Patio/Balcony	Property
			16	View	Property
			17	Hardwood Floors	Property
			24	Newly Remodeled	Property
			25	Fenced Yard	Property
			26	Washer/Dryer hookups	Property
			27	New Paint	Property
			28	New Carpet	Property
			29	Granite Countertops	Property
			30	Walk-In Closets	Property
			31	Tile	Property
			32	Storage	Property
			33	Pet-Friendly	Property
			 */

			//property amenities map (for old amenities with type Property)
			var propMapBase = new Dictionary<string, int>();
			propMapBase["washerdryer"] = 6;
			propMapBase["washerdryerhookups"] = 26;
			propMapBase["dishwasher"] = 4;
			//internetready
			//handicapaccessible

			propMapBase["hardwood"] = 17;
			propMapBase["tile"] = 31;
			propMapBase["walkinclosets"] = 30;
			propMapBase["fireplace"] = 11;
			//alarm

			//upgraded countertops
			//propMapBase["highceilings"] = 12;
			propMapBase["upgradedcountertops"] = 29;
			propMapBase["remodeled"] = 24;
			propMapBase["newpaint"] = 27;
			propMapBase["newcarpet"] = 28;
			propMapBase["storage"] = 32;

			//pool
			//hot tub
			propMapBase["microwave"] = 13;
			//propMapBase["patiobalcony"] = 14;
			//deck
			propMapBase["view"] = 16;
			propMapBase["fencedyard"] = 25;

			//invert it cause I feel like it, and I don't feel like re-writing the above.
			//its kinda useless the other way.
			var propMap = propMapBase.ToDictionary(x => x.Value, x => x.Key);

			//community amenities map (for old amenities with type Community)
			var comMap = new Dictionary<int, string>();
			comMap[7] = "communitypool";
			comMap[8] = "communityhottub";

			//community options
			var coMap = new Dictionary<int, Dictionary<string, string>>();
			coMap[1] = new Dictionary<string, string> 
			{ 
				{"1 Car", "coveredparking"},
				{"2 Cars", "coveredparking2cars"},
				{"3+ Cars", "coveredparking3+cars"}
			};
			coMap[2] = new Dictionary<string, string> 
			{ 
				{"1 Car", "streetparking1car"},
				{"2 Cars", "streetparking2cars"},
				{"3+ Cars", "streetparking3+cars"}
			};

			//property options
			var poMap = new Dictionary<int, Dictionary<string, string>>();
			poMap[3] = new Dictionary<string, string> 
			{ 
				{"Central Air", "centralair"},
				{"Evaporative Cooler","evaporativecooler"},
				{"Window Unit", "windowunit"},
				{"Other", "other"}
			};

			//I'MA CHARGIN MA CONVERSION


			//do basic amenities
			List<Old.BuildingAmenity> basicProperty = new List<Old.BuildingAmenity>();
			using(var context = new Old.RentlerNewEntities())
				basicProperty = context.BuildingAmenities
					.Where(p => p.Amenity.Category == "Property").ToList();

			//put them in that db!
			using(var context = new New.RentlerEntities())
			{
				var ba = new List<New.BuildingAmenity>();
				foreach(var item in basicProperty)
				{
					if(propMap.ContainsKey(item.AmenityId))
						ba.Add(new New.BuildingAmenity
						{
							BuildingId = item.BuildingId,
							AmenityId = propMap[item.AmenityId]
						});
				}

				Console.WriteLine("Prepped {0} amenities, inserting!", ba.Count);

				context.BulkInsert(ba);
				
				basicProperty.Clear();
			}


			//time for community basics!
			List<Old.BuildingAmenity> basicCommunity = new List<Old.BuildingAmenity>();
			using(var context = new Old.RentlerNewEntities())
				basicCommunity = context.BuildingAmenities
					.Where(b => b.Amenity.Category == "Community").ToList();

			//get them in there!
			using(var context = new New.RentlerEntities())
			{
				var ca = new List<New.BuildingAmenity>();
				foreach(var item in basicCommunity)
				{
					ca.Add(new New.BuildingAmenity
					{
						BuildingId = item.BuildingId,
						AmenityId = comMap[item.AmenityId]
					});

				}

				Console.WriteLine("Prepped {0} community amenities, inserting!", ca.Count);

				context.BulkInsert(ca);

				basicCommunity.Clear();
			}


			//and on to property options
			var propOptions = new List<Old.BuildingAmenitiesWithOption>();
			using(var context = new Old.RentlerNewEntities())
				propOptions = context.BuildingAmenitiesWithOptions
					.Where(b => b.AmenityId != 4 &&
								b.AmenitiesWithOption.Category == "Property" &&
								b.Option != "Forced Air").ToList();

			//convert those beasts
			var pwo = new List<New.BuildingAmenity>();
			foreach(var item in propOptions)
				pwo.Add(new New.BuildingAmenity
				{
					BuildingId = item.BuildingId,
					AmenityId = poMap[item.AmenityId][item.Option]
				});

			using(var context = new New.RentlerEntities())
			{
				context.BulkInsert(pwo);

				propOptions.Clear();
				pwo.Clear();
			}


			//finally, community options
			var comOptions = new List<Old.BuildingAmenitiesWithOption>();
			using(var context = new Old.RentlerNewEntities())
				comOptions = context.BuildingAmenitiesWithOptions
					.Where(b => b.AmenitiesWithOption.Category == "Community").ToList();

			//convert 'em!
			var cwo = new List<New.BuildingAmenity>();
			foreach(var item in comOptions)
				cwo.Add(new New.BuildingAmenity
				{
					BuildingId = item.BuildingId,
					AmenityId = coMap[item.AmenityId][item.Option]
				});

			using(var context = new New.RentlerEntities())
			{
				context.BulkInsert(cwo);

				comOptions.Clear();
				cwo.Clear();
			}

			Console.WriteLine("Done!");
		}
	}
}
