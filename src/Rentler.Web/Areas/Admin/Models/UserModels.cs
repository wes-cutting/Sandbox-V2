using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rentler.Web.Areas.Admin.Models
{
    public class UserSearchModel
    {
        public PaginatedList<Common.User> Results { get; set; }
        public int Page { get; set; }
        public string Query { get; set; }
    }
}