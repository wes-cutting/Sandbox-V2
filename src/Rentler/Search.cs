using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Rentler
{
	/// <summary>
	/// Class encapsulating searching parameters available
	/// within the application.
	/// </summary>
	public class Search
	{
        /// <summary>
        /// Initializes a new instance of the <see cref="Search"/> class.
        /// </summary>
		public Search() 
        {
            ResultsView = "Grid";            
        }

        /// <summary>
        /// Gets or sets the location for the search to be performed.
        /// </summary>
        /// <value>
        /// The location for the search to be performed.
        /// </value>
		public string Location { get; set; }

        /// <summary>
        /// Gets or sets the min price.
        /// </summary>
        /// <value>
        /// The min price.
        /// </value>
		public int? MinPrice { get; set; }

        /// <summary>
        /// Gets or sets the max price.
        /// </summary>
        /// <value>
        /// The max price.
        /// </value>
		public int? MaxPrice { get; set; }

        /// <summary>
        /// Gets or sets the property type code that identifies
        /// the property type. This is stored.
        /// </summary>
        /// <value>
        /// The property type code.
        /// </value>
		[DisplayName("Type")]
		public int PropertyTypeCode { get; set; }

        /// <summary>
        /// Gets or sets the type of the property. Maps to the
        /// property type code.
        /// </summary>
        /// <value>
        /// The type of the property.
        /// </value>
		public PropertyType PropertyType
		{
			get { return (PropertyType)this.PropertyTypeCode; }
			set { this.PropertyTypeCode = (int)value; }
		}

        /// <summary>
        /// Gets or sets whether the search is advanced (as opposed to basic)
        /// </summary>
        public bool IsAdvanced { get; set; }

        /// <summary>
        /// Gets or sets the bedrooms.
        /// </summary>
        /// <value>
        /// The bedrooms.
        /// </value>
		public int? Bedrooms { get; set; }

        /// <summary>
        /// Gets or sets the bathrooms.
        /// </summary>
        /// <value>
        /// The bathrooms.
        /// </value>
		public int? Bathrooms { get; set; }

        /// <summary>
        /// Gets or sets the min square footage.
        /// </summary>
        /// <value>
        /// The min square footage.
        /// </value>
		public int? MinSquareFootage { get; set; }

        /// <summary>
        /// Gets or sets the max square footage.
        /// </summary>
        /// <value>
        /// The max square footage.
        /// </value>
		public int? MaxSquareFootage { get; set; }

        /// <summary>
        /// Gets or sets the year built min.
        /// </summary>
        /// <value>
        /// The year built min.
        /// </value>
		public int? YearBuiltMin { get; set; }

        /// <summary>
        /// Gets or sets the year built max.
        /// </summary>
        /// <value>
        /// The year built max.
        /// </value>
		public int? YearBuiltMax { get; set; }

        /// <summary>
        /// Gets or sets the type of the seller.
        /// </summary>
        /// <value>
        /// The type of the seller.
        /// </value>		
		public ContactInfoType SellerType 
        {
            get { return (ContactInfoType)this.SellerTypeCode; }
            set { this.SellerTypeCode = (int)value; }
        }
        
        /// <summary>
        /// Gets or sets the contact type code
        /// </summary>
        public int SellerTypeCode { get; set; }

        /// <summary>
        /// Gets or sets the keywords.
        /// </summary>
        /// <value>
        /// The keywords.
        /// </value>
		public string Keywords { get; set; }

        /// <summary>
        /// Gets or sets the amenities.
        /// </summary>
        /// <value>
        /// The amenities.
        /// </value>
		public string[] Amenities { get; set; }

        /// <summary>
        /// Searchable terms
        /// </summary>
        public string[] Terms { get; set; }

        /// <summary>
        /// Gets or sets the lease length code
        /// </summary>
        public int LeaseLengthCode { get; set; }

        /// <summary>
        /// Gets or sets the lease length. Maps to the
        /// lease length code.
        /// </summary>
        /// <value>
        /// The length of the lease
        /// </value>
        public LeaseLength LeaseLength
        {
            get { return (LeaseLength)this.LeaseLengthCode; }
            set { this.LeaseLengthCode = (int)value; }
        }

        public bool PhotosOnly { get; set; }

        /// <summary>
        /// Gets or sets the order by.
        /// </summary>
        /// <value>
        /// The order by.
        /// </value>
		public string OrderBy { get; set; }

        /// <summary>
        /// Gets or sets how results are formatted
        /// </summary>
        public string ResultsView { get; set; }

        /// <summary>
        /// Gets or sets the page.
        /// </summary>
        /// <value>
        /// The page.
        /// </value>
		public int Page { get; set; }

        /// <summary>
        /// Gets or sets the results per page.
        /// </summary>
        /// <value>
        /// The results per page.
        /// </value>
		public int ResultsPerPage { get; set; }

        /// <summary>
        /// True if the results set has another page.
        /// False if it doesn't.
        /// </summary>
        public bool HasNextPage { get; set; }

        /// <summary>
        /// True if the results set has a previous page.
        /// False if it doesn't.
        /// </summary>
        public bool HasPreviousPage { get; set; }

        /// <summary>
        /// Gets or sets the results.
        /// </summary>
        /// <value>
        /// The results.
        /// </value>
		public PaginatedList<BuildingPreview> Results { get; set; }
	}
}
