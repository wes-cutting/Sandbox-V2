using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rentler.Web.Areas.Dashboard.Models
{
    public class ListingSavedModel
    {
        public PaginatedList<BuildingPreview> Results { get; set; }
    }

}