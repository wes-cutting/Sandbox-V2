using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rentler.Auth
{
    /// <summary>
    /// Class for authenticating users based on cookies.
    /// </summary>
    public class CookieAuthenticator : IAuthenticator
    {
        /// <summary>
        /// Authenticates a user based on
        /// credentials.
        /// </summary>
        /// <returns>An identity based on the cookies
        /// the browser contains.</returns>
        public Identity Authenticate()
        {
            return CustomAuthentication.GetIdentity();
        }
    }
}
