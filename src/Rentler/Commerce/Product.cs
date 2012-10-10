using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rentler.Adapters;

namespace Rentler.Commerce
{
    /// <summary>
    /// Definition for a product allowing it to manage items.
    /// </summary>
    public abstract class Product
    {
        private OrderItem item;

        /// <summary>
        /// Gets the name of the product in the configuration.
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        /// Placeholder for the order item.
        /// </summary>
        public virtual OrderItem Item 
        {
            get { return this.item; }
            protected set { this.item = value; }
        }

        /// <summary>
        /// Generates an order item from product options.
        /// </summary>
        /// <param name="option">The option.</param>
        /// <param name="quantity">The quantity.</param>
        /// <returns></returns>
        public abstract OrderItem ToOrderItem(string option, int quantity);

        /// <summary>
        /// Generates an instance from an order item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>A product that can be executed.</returns>
        public abstract Product FromOrderItem(OrderItem item);

        /// <summary>
        /// Executes the order details.
        /// </summary>
        /// <param name="order">The order.</param>
        /// <param name="checkoutAdapter">The checkout adapter.</param>
        public abstract void ExecuteOnComplete(Order order);

        /// <summary>
        /// Validates whether or not the the product with its
        /// currnet configuration is valid.
        /// </summary>
        /// <returns>Whether or not it is valid.</returns>
        public abstract bool Validate();
    }
}
