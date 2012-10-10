using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Rentler
{
    public class Order
    {
        public int OrderId { get; set; }

        public long? BuildingId { get; set; }
        
        public int UserId { get; set; }
        
        public int? UserCreditCardId { get; set; }        
                
        public decimal OrderTotal { get; set; }

        [StringLength(100)]
        public string PromoCode { get; set; }

        [Required]
        public DateTime CreateDate { get; set; }

        [Required]
        public int OrderStatusCode { get; set; }

        [Required]
        public OrderStatus OrderStatus
        {
            get { return (OrderStatus)this.OrderStatusCode; }
            set { this.OrderStatusCode = (int)value; }
        }

        [Required]
        [StringLength(100)]
        public string CreatedBy { get; set; }

        public virtual Building Building { get; set; }
        
        public virtual User User { get; set; }

        public virtual UserCreditCard UserCreditCard { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
