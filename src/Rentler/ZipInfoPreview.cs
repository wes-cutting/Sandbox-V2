using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProtoBuf;

namespace Rentler
{
    [ProtoContract]
    public class ZipInfoPreview
    {
        [ProtoMember(1)]
        public int ZipCode { get; set; }
        [ProtoMember(2)]
        public string FriendlyName { get; set; }
        [ProtoMember(3)]
        public string StateCode { get; set; }
		[ProtoMember(4)]
		public float Latitude { get; set; }
		[ProtoMember(5)]
		public float Longitude { get; set; }
    }
}
