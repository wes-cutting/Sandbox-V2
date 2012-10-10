using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Rentler
{
	public class ZipInfo
	{
		public long ZipInfoId { get; set; }

		[Required]
		public float Latitude { get; set; }

		[Required]
		public float Longitude { get; set; }

		[Required, StringLength(50)]
		public string CityAliasName { get; set; }

		[Required, StringLength(50)]
		public string City { get; set; }

		[Required, StringLength(4)]
		public string StateCode { get; set; }

		[Required]
		public int ZipCode { get; set; }
	}

	public class FriendlyZipInfo
	{
		public int ZipCode { get; set; }
		public string FriendlyName { get; set; }
		public string StateCode { get; set; }
	}
}
