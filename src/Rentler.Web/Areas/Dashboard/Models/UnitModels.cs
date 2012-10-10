using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rentler.Web.Areas.Dashboard.Models
{
    public class UnitViewModel
    {
        public Building Building { get; set; }
        public long ViewCount { get; set; }
        public long SearchViewCount { get; set; }
    }
}