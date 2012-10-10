using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rentler.Web.Models
{
    public class HomeIndexModel
    {
        public long TotalListingViews { get; set; }
        public Search Search { get; set; }
    }
}