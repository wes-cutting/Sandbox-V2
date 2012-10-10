using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Rentler
{
    public class OrderItem
    {
        public int OrderItemId { get; set; }

        public int OrderId { get; set; }

        public int Quantity { get; set; }

        [Required]
        [StringLength(100)]
        public string ProductId { get; set; }

        [StringLength(100)]
        public string ProductOption { get; set; }

        [Required]
        [StringLength(255)]
        public string ProductDescription { get; set; }

        public decimal Price { get; set; }

        public virtual Order Order { get; set; }
    }
}
