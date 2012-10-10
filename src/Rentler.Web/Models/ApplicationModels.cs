using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rentler.Web.Models
{
    public class ApplicationPropertyModel
    {
        public int BuildingId { get; set; }
        public List<UserInterest> Results { get; set; }
    }
}