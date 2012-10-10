using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Rentler
{
	public class Role
	{
		[Key, Required, StringLength(50)]
		public string RoleName { get; set; }

		[StringLength(160)]
		public string Description { get; set; }
	}
}
