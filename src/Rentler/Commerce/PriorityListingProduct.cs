using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rentler.Data;

namespace Rentler.Commerce
{
	public class PriorityListingProduct : Product
	{
		public override string Name
		{
			get { return "prioritylisting"; }
		}

		public override OrderItem ToOrderItem(string option, int quantity)
		{
			OrderItem item = new OrderItem();
			item.Quantity = 1;
			item.Price = 39.95m;
			item.ProductDescription = "Priority listing";
			item.ProductOption = true.ToString();
			item.ProductId = this.Name;
			return item;
		}

		public override Product FromOrderItem(OrderItem item)
		{
			PriorityListingProduct product = new PriorityListingProduct();
			product.Item = item;
			return product;
		}

		public override void ExecuteOnComplete(Order order)
		{
			using (var context = new RentlerContext())
			{
				// get local date, to timestamp when the building was prioritized (so we can turn it off after 30 days)
				TimeZoneInfo mstTZ = TimeZoneInfo.FindSystemTimeZoneById("Mountain Standard Time");
				DateTime dateLocal = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, mstTZ);

				var building = context.Buildings.FirstOrDefault(b => b.BuildingId == order.BuildingId);

				if (building == null)
					throw new ArgumentNullException("Building");

				building.HasPriority = true;
				building.DatePrioritized = dateLocal;

				context.SaveChanges();
			}
		}

		public override bool Validate()
		{
			if (this.Item == null)
				throw new InvalidOperationException(
					"The product must be created from an order item before it can be validated.");

			// make sure the ribbon is set right
			if (this.Item.ProductId != Name)
				return false;
			// make sure the the sub type is set
			if (string.IsNullOrEmpty(this.Item.ProductOption))
				return false;

			return true;
		}
	}
}
