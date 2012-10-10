using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rentler.Commerce;

namespace Rentler.Tests.Commerce
{
    /// <summary>
    /// Tests for the base product for the system.
    /// </summary>
    [TestClass]
    public class ProductBaseTests
    {
        List<Product> products;

        /// <summary>
        /// Setups this instance.
        /// </summary>
        [TestInitialize]
        public void Setup()
        {
            products = new List<Product>();
            foreach (var keyvalue in Rentler.Configuration.Products.Current.AvailableProducts)
                products.Add(keyvalue.Value);
        }

        /// <summary>
        /// Alls the products contain key name test.
        /// </summary>
        [TestMethod]
        public void AllProductsContainKeyNameTest()
        {
            foreach (var item in products)
                Assert.IsTrue(Rentler.Configuration.Products.Current.AvailableProducts.ContainsKey(item.Name));
        }
    }
}
