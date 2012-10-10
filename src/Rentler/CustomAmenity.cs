using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Rentler
{
    public class CustomAmenity
    {
        public long BuildingId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public virtual Building Building { get; set; }
    }
}
