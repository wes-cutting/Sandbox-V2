using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rentler
{
	public class AffiliateUser
	{
		[Key, ForeignKey("User")]
		public int UserId { get; set; }

		[StringLength(50)]
		public string AffiliateUserKey { get; set; }

		public Guid ApiKey { get; set; }
		public string PasswordHash { get; set; }
		public bool IsDeleted { get; set; }

		public virtual User User { get; set; }
	}
}
