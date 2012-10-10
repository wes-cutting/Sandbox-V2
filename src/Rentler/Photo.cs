using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Rentler
{
    public class Photo
    {
        public Guid PhotoId { get; set; }

        [Required]
        public long BuildingId { get; set; }

        [Required]
        public bool IsPrimary { get; set; }

        [Required]
        public int SortOrder { get; set; }

        [Required]
        [StringLength(5)]
        public string Extension { get; set; }

        [Required]
        public DateTime CreateDateUtc { get; set; }

        [Required]
        public string CreatedBy { get; set; }

        [Required]
        public DateTime UpdateDateUtc { get; set; }

        [Required]
        public string UpdatedBy { get; set; }

        public virtual Building Building { get; set; }
    }
}
