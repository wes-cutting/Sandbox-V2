using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Rentler
{
    public class UserInterest
    {
        public int UserInterestId { get; set; }
        
        public int UserId { get; set; }
        
        public virtual User User { get; set; }
        
        public long BuildingId { get; set; }

        public virtual Building Building { get; set; }

        [StringLength(1000)]
        public string Message { get; set; }

        [StringLength(1000)]
        public string ResponseMessage { get; set; }

        public bool ApplicationSubmitted { get; set; }
    }
}
