using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rentler.Common
{
    public class PropertyPreview
    {
        public int UserId { get; set; }
        public long BuildingId { get; set; }
        public Guid? PrimaryPhotoId { get; set; }
        public string PrimaryPhotoExtension { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public bool IsActive { get; set; }
        public bool IsRemovedByAdmin { get; set; }
    }
}
