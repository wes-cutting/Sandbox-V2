using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Rentler
{
    public class SavedBuilding
    {
        public int UserId { get; set; }

        public long BuildingId { get; set; }

        [Required]
        public DateTime CreateDateUtc { get; set; }

        [Required]
        public string CreatedBy { get; set; }

        public virtual User User { get; set; }

        public virtual Building Building { get; set; }
    }
}
