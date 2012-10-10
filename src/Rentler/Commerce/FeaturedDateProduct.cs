using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rentler.Data;

namespace Rentler.Commerce
{
	public class FeaturedDateProduct : Product
	{
		public override string Name
		{
			get { return "featureddate"; }
		}

		public override OrderItem ToOrderItem(string option, int quantity)
		{
			DateTime dateUtc;

			if(!DateTime.TryParse(option, out dateUtc))
				throw new InvalidOperationException("The product option must be a date");

            // get local date, this will be stored in product description (not utc date)
            TimeZoneInfo mstTZ = TimeZoneInfo.FindSystemTimeZoneById("Mountain Standard Time");
            DateTime dateLocal = TimeZoneInfo.ConvertTimeFromUtc(dateUtc, mstTZ);

			OrderItem item = new OrderItem();
			item.Quantity = quantity;
			item.Price = 9.95m;
			item.ProductDescription = "Feature listing on " + dateLocal.ToShortDateString();
			item.ProductOption = dateUtc.ToString("G");
			item.ProductId = this.Name;
			return item;
		}

		public override Product FromOrderItem(OrderItem item)
		{
			FeaturedDateProduct product = new FeaturedDateProduct();
			product.Item = item;
			return product;
		}

		public override void ExecuteOnComplete(Order order)
		{
			var tzi = TimeZoneInfo.FindSystemTimeZoneById("Mountain Standard Time");
			var today = DateTime.UtcNow.AddHours(tzi.GetUtcOffset(DateTime.Now).Hours);

			var featuredDate = DateTime.Parse(
				this.Item.ProductOption, null,
				System.Globalization.DateTimeStyles.AssumeUniversal);

			//make sure the featured day is scheduled to exactly midnight.
			featuredDate = featuredDate.Date.AddHours(-tzi.GetUtcOffset(featuredDate.Date).Hours);

			using(var context = new RentlerContext())
			{
				var building = context.Buildings.FirstOrDefault(b => b.BuildingId == order.BuildingId);

				if(building == null)
					throw new ArgumentNullException("Building");

				context.FeaturedListings.Add(new FeaturedListing
				{
					BuildingId = building.BuildingId,
					ScheduledDate = featuredDate,
					Zip = building.Zip
				});

				context.SaveChanges();
			}
		}

		public override bool Validate()
		{
			if(this.Item == null)
				throw new InvalidOperationException(
					"The product must be created from an order item before it can be validated.");

			DateTime test;
			DateTime today = DateTime.Parse(DateTime.UtcNow.ToShortDateString());

			// make sure the ribbon is set right
			if(this.Item.ProductId != Name)
				return false;
			// make sure the the sub type is set
			if(string.IsNullOrEmpty(this.Item.ProductOption))
				return false;
			// verify that the sub type is a date
			if(!DateTime.TryParse(this.Item.ProductOption, out test))
				return false;
			// make sure the dates are in the future
			if(test < today)
				return false;

			return true;
		}
	}
}
