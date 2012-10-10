using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rentler.Commerce;

namespace Rentler.Tests.Commerce
{
    [TestClass]
    public class RibbonProductTests
    {
        RibbonProduct product;
        List<string> ribbonIds;

        [TestInitialize]
        public void Setup()
        {
            product = new RibbonProduct();
            ribbonIds = new List<string>();

            foreach (var item in Rentler.Configuration.Ribbons.Current.AvailableRibbons.Keys)
                ribbonIds.Add(item);
        }

        [TestMethod]
        public void AddInvalidRibbonIdException()
        {
            try
            {
                var orderItem = product.ToOrderItem("alksdjflakjsdflkjasdlf", 10);
                Assert.Fail("Didn't throw an exception");
            }
            catch (KeyNotFoundException)
            {
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public void ToOrderItemDescriptionTest()
        {
            var orderItem = product.ToOrderItem(ribbonIds.First(), 10);
            var description = "Ribbon " + Rentler.Configuration.Ribbons.Current.AvailableRibbons[ribbonIds.First()];
            Assert.AreEqual(orderItem.ProductDescription, description);
        }

        [TestMethod]
        public void ToOrderItemPriceCorrectTest()
        {
            var orderItem = product.ToOrderItem(ribbonIds.First(), 10);
            Assert.AreEqual(orderItem.Price, .99m);
        }

        [TestMethod]
        public void ToOrderItemQuantityTest()
        {
            var orderItem = product.ToOrderItem(ribbonIds.First(), 1);
            Assert.AreEqual(orderItem.Quantity, 1);

            orderItem = product.ToOrderItem(ribbonIds.First(), 10);
            Assert.AreEqual(orderItem.Quantity, 10);
        }

        [TestMethod]
        public void ValidateNoOrderItemException()
        {
            try
            {
                this.product.Validate();
                Assert.Fail("Didn't throw an exception");
            }
            catch (InvalidOperationException)
            {
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public void ExecuteSetsRibbonIdOnBuilding()
        {
            var orderItem = product.ToOrderItem(ribbonIds.First(), 1);
            Order order = new Order();
            order.Building = new Building();
            var newProduct = product.FromOrderItem(orderItem);
            newProduct.ExecuteOnComplete(order);

            Assert.AreEqual(order.Building.RibbonId, ribbonIds.First());
        }
    }
}
