using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rentler.Auth;

namespace Rentler.Tests.Auth
{
    [TestClass]
    public class IdentityTests
    {
        [TestMethod]
        public void DefaultConstructorNotAuthenticated()
        {
            Identity ident = new Identity();
            Assert.IsFalse(ident.IsAuthenticated);
            Assert.AreEqual(ident.Username, string.Empty);
            Assert.AreEqual(ident.UserId, 0);
        }

        [TestMethod]
        public void UserConstructorIsAuthenticated()
        {
            Identity ident = new Identity(1, "cyberkruz");
            Assert.IsTrue(ident.IsAuthenticated);
        }

        [TestMethod]
        public void UserConstructorPropertiesSet()
        {
            Identity ident = new Identity(1, "cyberkruz");
            Assert.AreEqual(ident.Username, "cyberkruz");
            Assert.AreEqual(ident.UserId, 1);
        }
    }
}
