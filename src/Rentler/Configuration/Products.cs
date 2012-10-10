using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rentler.Commerce;

namespace Rentler.Configuration
{
    /// <summary>
    /// Singleton implementation of available products within the system.
    /// </summary>
    public sealed class Products
    {
        private static volatile Products instance;
        private static object syncRoot = new Object();

        /// <summary>
        /// Prevents a default instance of the <see cref="Ribbons"/> class from being created.
        /// </summary>
        private Products()
        {
            this.AvailableProducts = new Dictionary<string, Product>();
            this.ConfigureProducts();
        }

        /// <summary>
        /// Configures the ribbons.
        /// </summary>
        private void ConfigureProducts()
        {
            this.AvailableProducts.Add("ribbon", new RibbonProduct());
            this.AvailableProducts.Add("featureddate", new FeaturedDateProduct());
			this.AvailableProducts.Add("prioritylisting", new PriorityListingProduct());
        }

        /// <summary>
        /// Gets or sets the available ribbons.
        /// </summary>
        /// <value>
        /// The available ribbons.
        /// </value>
        public Dictionary<string, Product> AvailableProducts { get; set; }

        /// <summary>
        /// Shortcut to access list of products.
        /// </summary>
        /// <param name="productName">Name of the product.</param>
        /// <returns>A product with the name specified.</returns>
        public static Product GetProduct(string productName)
        {
            return Rentler.Configuration.Products.Current.AvailableProducts[productName];
        }

        /// <summary>
        /// Gets the current instance of the ribbons configuration. Uses
        /// the double locking approach as it is safe and handled in .Net
        /// without volitility.
        /// </summary>
        public static Products Current
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new Products();
                    }
                }
                return instance;
            }
        }
    }
}
