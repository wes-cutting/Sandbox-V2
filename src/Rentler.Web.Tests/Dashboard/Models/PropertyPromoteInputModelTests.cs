using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rentler.Web.Areas.Dashboard.Models;

namespace Rentler.Web.Tests.Dashboard.Models
{
	[TestClass]
	public class PropertyPromoteInputModelTests
	{
		Building building;
		List<FeaturedListing> featured;

		[TestInitialize]
		public void Initialize()
		{
			this.building = new Building();
			this.building.BuildingId = 100;
			this.building.Title = "My Title";
			this.building.Zip = "84123";
			this.building.Description = "My Description";

			var tzi = TimeZoneInfo.FindSystemTimeZoneById("Mountain Standard Time");
			var today = DateTime.UtcNow.AddHours(tzi.GetUtcOffset(DateTime.Now).Hours);
			today = today.Date.ToUniversalTime().AddHours(-tzi.GetUtcOffset(today).Hours);

			featured = new List<FeaturedListing> {
				new FeaturedListing{
					 ScheduledDate = today,
					 BuildingId = 100,
					 Zip = "84123",
					 FeaturedListingId = 1
				},
				new FeaturedListing{
					 ScheduledDate = today,
					 BuildingId = 100,
					 Zip = "84123",
					 FeaturedListingId = 1
				},
				new FeaturedListing{
					 ScheduledDate = today,
					 BuildingId = 100,
					 Zip = "84123",
					 FeaturedListingId = 1
				},
				new FeaturedListing{
					 ScheduledDate = today,
					 BuildingId = 100,
					 Zip = "84104",
					 FeaturedListingId = 1
				},
				new FeaturedListing{
					 ScheduledDate = today,
					 BuildingId = 100,
					 Zip = "84104",
					 FeaturedListingId = 1
				},
				new FeaturedListing{
					 ScheduledDate = today,
					 BuildingId = 100,
					 Zip = "84104",
					 FeaturedListingId = 1
				}
			};
		}

		[TestMethod]
		public void BuildingWithNoOrderNullSelectedRibbon()
		{
			PropertyPromoteInputModel model = new PropertyPromoteInputModel(building, featured);
			Assert.IsNull(model.SelectedRibbonId);
			Assert.IsNull(model.SelectedRibbonName);
		}

		[TestMethod]
		public void BuildingWithOrderSelectedRibbonId()
		{
			// add a temporary order
			Order order = new Order();
			string ribbonType = Rentler.Configuration.Ribbons.Current.AvailableRibbons.First().Key;
			OrderItem item = Rentler.Configuration.Products.GetProduct("ribbon").ToOrderItem(ribbonType, 1);
			order.OrderItems = new List<OrderItem>();
			order.OrderItems.Add(item);
			building.TemporaryOrder = order;

			// test
			PropertyPromoteInputModel model = new PropertyPromoteInputModel(building, featured);
			Assert.AreEqual(item.ProductOption, model.SelectedRibbonId);
		}

		[TestMethod]
		public void BuildingWithOrderSelectedRibbonName()
		{
			// add a temporary order
			Order order = new Order();
			string ribbonName = Rentler.Configuration.Ribbons.Current.AvailableRibbons.First().Value;
			string ribbonType = Rentler.Configuration.Ribbons.Current.AvailableRibbons.First().Key;
			OrderItem item = Rentler.Configuration.Products.GetProduct("ribbon").ToOrderItem(ribbonType, 1);
			order.OrderItems = new List<OrderItem>();
			order.OrderItems.Add(item);
			building.TemporaryOrder = order;

			// test
			PropertyPromoteInputModel model = new PropertyPromoteInputModel(building, featured);
			Assert.AreEqual(model.SelectedRibbonName, ribbonName);
		}

		[TestMethod]
		public void ToBuildingBasicInformation()
		{
			PropertyPromoteInputModel model = new PropertyPromoteInputModel(building, featured);
			var b = model.ToBuilding();

			Assert.AreEqual(b.Title, this.building.Title);
			Assert.AreEqual(b.BuildingId, this.building.BuildingId);
			Assert.AreEqual(b.Description, this.building.Description);
		}
	}
}
