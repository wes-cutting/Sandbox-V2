using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rentler.Adapters;

namespace Rentler.Commerce
{
    /// <summary>
    /// Product logic for ribbons.
    /// </summary>
    public class RibbonProduct : Product
    {
        public override OrderItem ToOrderItem(string option, int quantity)
        {
            OrderItem item = new OrderItem();
            item.Quantity = quantity;
            item.Price = 19.95m;
            item.ProductDescription = Rentler.Configuration.Ribbons.Current.AvailableRibbons[option] + " ribbon";
            item.ProductOption = option;
            item.ProductId = this.Name;
            return item;
        }

        public override Product FromOrderItem(OrderItem item)
        {
            RibbonProduct product = new RibbonProduct();
            product.Item = item;
            return product;
        }

        public override string Name
        {
            get { return "ribbon"; }
        }

        public override void ExecuteOnComplete(Order order)
        {
            order.Building.RibbonId = this.Item.ProductOption;
			order.Building.DateRibbonActivated = DateTime.UtcNow;
        }

        public override bool Validate()
        {
            if (this.Item == null)
                throw new InvalidOperationException(
                    "The product must be created from an order item before it can be validated.");

            // make sure the ribbon is set right
            if(this.Item.ProductId != Name)
                return false;
            // make sure the the sub type is set
            if(string.IsNullOrEmpty(this.Item.ProductOption))
                return false;
            // make sure the ribbon is correct
            if (!Rentler.Configuration.Ribbons.Current.AvailableRibbons.ContainsKey(this.Item.ProductOption))
                return false;

            return true;
        }
    }
}
