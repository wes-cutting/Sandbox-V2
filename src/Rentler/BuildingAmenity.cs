using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rentler
{
    public class BuildingAmenity
    {
        public long BuildingId { get; set; }

        public string AmenityId { get; set; }

        public virtual Building Building { get; set; }
    }
}
