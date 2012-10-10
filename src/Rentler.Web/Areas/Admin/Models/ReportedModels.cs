using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rentler.Web.Areas.Admin.Models
{
    public class ReportedSearchModel
    {
        public int Page { get; set; }
        public PaginatedList<Common.ReportedListing> Listings { get; set; }
    }
}