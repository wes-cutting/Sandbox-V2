using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rentler
{
    /// <summary>
    /// Extends a List to provide built in paging.
    /// </summary>
    public class PaginatedList<T> : List<T>
    {
        /// <summary>
        /// Gets or sets the page number.
        /// </summary>
        /// <value>The index of the page.</value>
        public int PageNumber { get; private set; }
        /// <summary>
        /// Gets or sets the size of the page.
        /// </summary>
        /// <value>The size of the page.</value>
        public int PageSize { get; private set; }
        /// <summary>
        /// Gets or sets the total count of items.
        /// </summary>
        /// <value>The total count.</value>
        public int TotalCount { get; private set; }
        /// <summary>
        /// Gets or sets the total pages.
        /// </summary>
        /// <value>The total pages.</value>
        public int TotalPages { get; private set; }

        private PaginatedList(IEnumerable<T> items) : base(items) { }

        /// <summary>
        /// Takes advantage of lazy loading by using an IQueryable source,
        /// skipping results and taking only what is needed for a given page
        /// based on the index and size.
        /// </summary>
        /// <param name="source">The IQueryable source of data to pull paged
        /// results from.</param>
        /// <param name="pageNumber">The page number (one based).</param>
        /// <param name="pageSize">The number of results to get per page.</param>
        /// <example>
        /// To get the third page of a list of users, with 10 items in the page:
        /// var list = new PaginatedList(usersList, 2, 10);</example>
        public PaginatedList(IQueryable<T> source, int pageNumber, int pageSize)
        {
            PageNumber = pageNumber < 1 ? 1 : pageNumber;
            PageSize = pageSize;
            TotalCount = source.Count();

            // If the pagesize is < 1 then the total pages is 1
            TotalPages = PageSize < 1 ? 1 :
                (int)Math.Ceiling(TotalCount / (double)PageSize);

            if (this.PageSize < 1)
            {
                this.AddRange(source);
                this.PageSize = TotalCount;
            }
            else
                this.AddRange(source.Skip(PageNumber * PageSize - PageSize).Take(PageSize));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PaginatedList&lt;T&gt;"/> class. Does not
        /// skip/take results from the T source (it assumes you're giving it the correct set of results), 
        /// since you're setting the total count yourself.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="totalCount">The total count.</param>
        public PaginatedList(IQueryable<T> source, int pageNumber, int pageSize, int totalCount)
        {
            PageNumber = pageNumber < 1 ? 1 : pageNumber;
            PageSize = pageSize;
            TotalCount = totalCount;

            TotalPages = PageSize < 1 ? 1 :
                (int)Math.Ceiling(TotalCount / (double)PageSize);

            this.AddRange(source);
        }

        /// <summary>
        /// Gets a value indicating whether this instance has previous page.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance has previous page; otherwise, <c>false</c>.
        /// </value>
        public bool HasPreviousPage
        {
            get
            {
                return (PageNumber > 1);
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance has next page.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance has next page; otherwise, <c>false</c>.
        /// </value>
        public bool HasNextPage
        {
            get
            {
                return (PageNumber < TotalPages);
            }
        }

        public PaginatedList<T> DeepClone()
        {
            var x = this.Select(item => (T)((ICloneable)(item)).Clone()).AsEnumerable();

            return new PaginatedList<T>(x)
            {
                PageNumber = this.PageNumber,
                PageSize = this.PageSize,
                TotalCount = this.TotalCount,
                TotalPages = this.TotalPages
            };
        }
    }
}
