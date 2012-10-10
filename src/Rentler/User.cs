using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Rentler
{
    public class User
    {
        public int UserId { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [StringLength(250)]
        public string PasswordHash { get; set; }

        [Required]
        [StringLength(250)]
        public string Email { get; set; }

        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        public DateTime UpdateDateUtc { get; set; }

        [Required]
        public DateTime CreateDateUtc { get; set; }

        [Required]
        [StringLength(50)]
        public string UpdatedBy { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

		[Required]
		public Guid ReferenceId { get; set; }

        public virtual ICollection<Building> Buildings { get; set; }

        public virtual ICollection<ContactInfo> ContactInfos { get; set; }

        public virtual ICollection<SavedBuilding> SavedBuildings { get; set; }

        public virtual ICollection<UserCreditCard> UserCreditCards { get; set; }

        public ICollection<Order> Orders { get; set; }

		public virtual AffiliateUser AffiliateUser { get; set; }

		public virtual UserApplication UserApplication { get; set; }

        public virtual ICollection<UserBank> UserBanks { get; set; }

        public virtual ICollection<UserInterest> UserInterests { get; set; }

		public virtual ICollection<RoleUser> RoleUsers { get; set; }
    }
}
