using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rentler.Auth
{
    /// <summary>
    /// Contract for authenticating a user
    /// based on a set of credentials.
    /// </summary>
    public interface IAuthenticator
    {
        /// <summary>
        /// Authenticates a user based on a set of credentials
        /// </summary>
        /// <returns>Identity based on authentication</returns>
        Identity Authenticate();
    }
}
