using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Rentler
{
    public class UserCreditCard
    {
        public int UserCreditCardId { get; set; }

        [Required]
        public int UserId { get; set; }

        public virtual User User { get; set; }

        public long? Alias { get; set; }

        public Guid? AccountReference { get; set; }

        [Required]
        [StringLength(16)]
        public string CardNumber { get; set; }

        [Required]
        [StringLength(50)]
        public string CardName { get; set; }

        [Required]
        public int ExpirationMonth { get; set; }

        [Required]        
        public int ExpirationYear { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [StringLength(50)]
        public string Address1 { get; set; }

        [StringLength(50)]
        public string Address2 { get; set; }

        [Required]
        [StringLength(50)]
        public string City { get; set; }

        [Required]
        [StringLength(2)]
        public string State { get; set; }

        [Required]
        [StringLength(10)]
        public string Zip { get; set; }

        [StringLength(20)]
        public string Phone { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

        public DateTime? UpdateDate { get; set; }

        [StringLength(100)]
        public string UpdatedBy { get; set; }

        [Required]
        public DateTime CreateDate { get; set; }

        [Required]
        [StringLength(100)]
        public string CreatedBy { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
