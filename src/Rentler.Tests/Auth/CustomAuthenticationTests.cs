using System;
using System.IO;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rentler.Auth;

namespace Rentler.Tests.Auth
{
    [TestClass]
    public class CustomAuthenticationTests
    {
        [TestInitialize]
        public void Setup()
        {
            HttpContext.Current = new HttpContext(
                new HttpRequest("", "http://tempuri.org", ""),
                new HttpResponse(new StringWriter()));

            // unauthenticated user
            HttpContext.Current.User = new GenericPrincipal(
                new GenericIdentity(string.Empty),
                new string[0]);

        }

        [TestMethod]
        public void GetIdentityUnauthenticatedUser()
        {
            var identity = CustomAuthentication.GetIdentity();
            Assert.IsNotNull(identity);
            Assert.AreEqual(identity.IsAuthenticated, false);
            Assert.AreEqual(identity.Username, string.Empty);
            Assert.AreEqual(identity.UserId, 0);
        }

        [TestMethod]
        public void GetIdentityIsAuthenticated()
        {
            // unauthenticated user
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                    1, "cyberkruz", DateTime.Now, DateTime.Now.AddDays(30),
                    true, "4", FormsAuthentication.FormsCookiePath);
            FormsIdentity ident = new FormsIdentity(ticket);
            HttpContext.Current.User = new GenericPrincipal(ident, new string[0]);

            var identity = CustomAuthentication.GetIdentity();
            Assert.IsTrue(identity.IsAuthenticated);
            Assert.AreEqual(identity.Username, "cyberkruz");
            Assert.AreEqual(identity.UserId, 4);
        }

        [TestMethod]
        public void SetAuthCookieNoException()
        {
            CustomAuthentication.SetAuthCookie("cyberkruz", 4, true);
        }

        [TestMethod]
        public void SignOutNoException()
        {
            CustomAuthentication.SignOut();
        }

        [TestMethod]
        public void GetIdentityInvalidIdentity()
        {
            HttpContext.Current.User = new GenericPrincipal(
                new FakeIdentity("cyberkruz"), new string[0]);
            try
            {
                CustomAuthentication.GetIdentity();
            }
            catch (InvalidCastException)
            {
                Assert.IsTrue(true);
            }
        }

        public class FakeIdentity : GenericIdentity
        {
            public FakeIdentity(string username) : base(username) { }
        }
    }
}
