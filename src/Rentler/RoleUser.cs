using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Rentler
{
	public class RoleUser
	{
		[Required]
		public string RoleName { get; set; }

		[Required]
		public int UserId { get; set; }

		public virtual User User { get; set; }

		public virtual Role Role { get; set; }
	}
}
