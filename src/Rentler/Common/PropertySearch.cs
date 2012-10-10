using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rentler.Common
{
    /// <summary>
    /// Class containing seach information for someone
    /// searching through their properties.
    /// </summary>
    public class PropertySearch
    {
        /// <summary>
        /// Gets or sets the keywords to search for 
        /// properties by.
        /// </summary>
        /// <value>
        /// The keywords to search on.
        /// </value>
        public string Keywords { get; set; }

        /// <summary>
        /// Gets or sets the type of ordering to do
        /// on the results.
        /// </summary>
        /// <value>
        /// The type of ordering to do on the results.
        /// </value>
        public string OrderBy { get; set; }

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
        /// Gets or sets the results.
        /// </summary>
        /// <value>
        /// The results.
        /// </value>
        public PaginatedList<PropertyPreview> Results { get; set; }
    }

	public class KslPropertySearch
	{
		/// <summary>
		/// Gets or sets the keywords to search for 
		/// properties by.
		/// </summary>
		/// <value>
		/// The keywords to search on.
		/// </value>
		public string Keywords { get; set; }

		/// <summary>
		/// Gets or sets the type of ordering to do
		/// on the results.
		/// </summary>
		/// <value>
		/// The type of ordering to do on the results.
		/// </value>
		public string OrderBy { get; set; }

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
		/// Gets or sets the results.
		/// </summary>
		/// <value>
		/// The results.
		/// </value>
		public List<BuildingPreview> Results { get; set; }
	}
}
