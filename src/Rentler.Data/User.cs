using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rentler.Data
{
    public partial class User
    {
        public Rentler.Common.User ToBusinessUser()
        {
            return new Common.User()
            {
                Email = this.Email,
                UserId = this.UserId,
                Username = this.Username
            };
        }
    }
}
