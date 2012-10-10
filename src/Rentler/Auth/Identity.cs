using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rentler.Auth
{
    /// <summary>
    /// Class representing authentication details
    /// for a user. A default instance is returned for 
    /// unauthenticated requests.
    /// </summary>
    public class Identity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Identity" /> class.
        /// Sets the username
        /// </summary>
        public Identity()
        {
            this.Username = string.Empty;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Identity" /> class.
        /// Used for authenticated requests.
        /// </summary>
        /// <param name="userId">The id of the user</param>
        /// <param name="username">The username of the user.</param>
        public Identity(int userId, string username)
        {
            this.IsAuthenticated = true;
            this.UserId = userId;
            this.Username = username;
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is authenticated.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is authenticated; otherwise, <c>false</c>.
        /// </value>
        public bool IsAuthenticated { get; set; }

        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>
        /// The username.
        /// </value>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        /// <value>
        /// The user id.
        /// </value>
        public int UserId { get; set; }
    }
}
