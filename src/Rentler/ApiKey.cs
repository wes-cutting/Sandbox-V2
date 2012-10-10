using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using ProtoBuf;

namespace Rentler
{
	[ProtoContract]
	public class ApiKey
	{
		[ProtoMember(1)]
		public Guid ApiKeyId { get; set; }

		[ProtoMember(2)]
		[StringLength(40)]
		public string Name { get; set; }
	}
}
