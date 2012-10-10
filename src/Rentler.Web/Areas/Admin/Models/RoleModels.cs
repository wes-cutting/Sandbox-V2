using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rentler.Web.Areas.Admin.Models
{
    public class RoleSearchModel
    {
        public string[] Roles { get; set; }
        public RoleCreateInputModel Input { get; set; }
    }

    public class RoleCreateInputModel
    {
    }
}