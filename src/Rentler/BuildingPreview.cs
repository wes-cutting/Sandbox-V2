using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rentler
{
	public class BuildingPreview
	{
		public long BuildingId { get; set; }
		public Guid? PrimaryPhotoId { get; set; }
        public string PrimaryPhotoExtension { get; set; }
		public string RibbonId { get; set; }

		public int Bedrooms { get; set; }
		public float Bathrooms { get; set; }
		public decimal Price { get; set; }
		public string Title { get; set; }
		public string City { get; set; }
		public string State { get; set; }
		public bool IsFeatured { get; set; }
        public bool IsRemovedByAdmin { get; set; }
		public bool HasPriority { get; set; }
		public DateTime? DatePrioritized { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public bool IsActive { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public string Zip { get; set; }
        public string FullAddress 
        {
            get
            {
                return string.Format(
                    "{0} {1}, {2}, {3} {4}",
                    this.Address1, this.Address2,
                    this.City,
                    this.State, this.Zip
                );
            }
        }
	}
}
