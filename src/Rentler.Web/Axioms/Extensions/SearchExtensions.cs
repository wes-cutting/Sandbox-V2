using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rentler.Web.Axioms.Extensions
{
    public static class SearchExtensions
    {
        /// <summary>
        /// Generates a link to the next page, if there is one.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>A link to the next page, or an empty string if there isn't one.</returns>
        public static string NextLink(this Search model)
        {
            //if (!model.HasNextPage)
            //    return "";

            return GenerateLink(model, model.Page + 1);            
        }

        /// <summary>
        /// Generates a link to the search page.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>A link to the search page.</returns>
        public static string Link(this Search model)
        {
            return GenerateLink(model, null);            
        }

        public static string GenerateLink(Search model, int? linkPage)
        {
            string link = "?";

            int page = (linkPage.HasValue) ? linkPage.Value : model.Page;

            if (page != 0)
                link += string.Format("Page={0}&", page);

            if (!string.IsNullOrWhiteSpace(model.Location))
                link += string.Format("Location={0}&", HttpUtility.UrlEncode(model.Location));
            if (model.ResultsPerPage != 0)
                link += string.Format("ResultsPerPage={0}&", model.ResultsPerPage);
            if (model.MinPrice.HasValue)
                link += string.Format("MinPrice={0}&", model.MinPrice);
            if (model.MaxPrice.HasValue)
                link += string.Format("MaxPrice={0}&", model.MaxPrice);
            if (model.PropertyType != PropertyType.Undefined)
                link += string.Format("PropertyTypeCode={0}&", model.PropertyTypeCode);            
            if (model.OrderBy != null)
                link += string.Format("OrderBy={0}&", model.OrderBy);

            if (model.IsAdvanced)
            {
                link += string.Format("IsAdvanced={0}&", model.IsAdvanced);

                if (model.Bedrooms.HasValue)
                    link += string.Format("Bedrooms={0}&", model.Bedrooms);
                if (model.Bathrooms.HasValue)
                    link += string.Format("Bathrooms={0}&", model.Bathrooms);
                if (model.MinSquareFootage.HasValue)
                    link += string.Format("MinSquareFootage={0}&", model.MinSquareFootage);
                if (model.MaxSquareFootage.HasValue)
                    link += string.Format("MaxSquareFootage={0}&", model.MaxSquareFootage);
                if(model.YearBuiltMin.HasValue)
                    link += string.Format("YearBuiltMin={0}&", model.YearBuiltMin);
                if(model.YearBuiltMax.HasValue)
                    link += string.Format("YearBuiltMax={0}&", model.YearBuiltMax);
                
                // property amenities
                if (model.Amenities != null)
                {
                    for (int i = 0; i < model.Amenities.Length; ++i)
                    {
                        link += string.Format("amenities{0}={1}&", HttpUtility.UrlEncode("[]"), model.Amenities[i]);
                    }
                }

                if (!string.IsNullOrWhiteSpace(model.Keywords))
                    link += string.Format("Keywords={0}&", HttpUtility.UrlEncode(model.Keywords));
            }

            link = link.TrimEnd('&');

            return link;
        }

        //public static string GenerateLink(int page, string location,
        //    int pageSize, int priceLower, int priceUpper, string propertyType,
        //    string zip, string orderBy)
        //{
        //    string link = "?";

        //    if (page != 0)
        //        link += string.Format("Page={0}&", page);
        //    if (location != null)
        //        link += string.Format("Location={0}&", HttpUtility.UrlEncode(location));            
        //    if (pageSize != 0)
        //        link += string.Format("ResultsPerPage={0}&", pageSize);
        //    if (priceLower != 0)
        //        link += string.Format("MinPrice={0}&", priceLower);
        //    if (priceUpper != 0)
        //        link += string.Format("MaxPrice={0}&", priceUpper);
        //    if (propertyType != null)
        //        link += string.Format("PropertyType={0}&", HttpUtility.UrlEncode(propertyType));
        //    if (zip != null)
        //        link += string.Format("Zip={0}&", zip.Replace(" ", ""));
        //    if (orderBy != null)
        //        link += string.Format("OrderBy={0}&", orderBy);
            
        //    link = link.TrimEnd('&');

        //    return link;
        //}

        //public static string GenerateLink(int page, string location, int miles,
        //    int pageSize, int priceLower, int priceUpper, string propertyType, string zip,
        //    bool isAdvanced, int? beds, int? baths, string keywords, string orderBy,
        //    int sqftLower, int sqftUpper, int yearBuiltLower, int yearBuiltUpper, int[] amenities)
        //{
        //    string link = "?";

        //    if (page != 0)
        //        link += string.Format("Page={0}&", page);
        //    if (location != null)
        //        link += string.Format("Location={0}&", HttpUtility.UrlEncode(location));            
        //    if (pageSize != 0)
        //        link += string.Format("ResultsPerPage={0}&", pageSize);
        //    if (priceLower != 0)
        //        link += string.Format("MinPrice={0}&", priceLower);
        //    if (priceUpper != 0)
        //        link += string.Format("MaxPrice={0}&", priceUpper);
        //    if (propertyType != null)
        //        link += string.Format("PropertyType={0}&", HttpUtility.UrlEncode(propertyType));
        //    if (zip != null)
        //        link += string.Format("Zip={0}&", zip.Replace(" ", ""));
        //    if (orderBy != null)
        //        link += string.Format("OrderBy={0}&", orderBy);

        //    if (sqftLower != 0)
        //        link += string.Format("MinSquareFootage={0}&", sqftLower);
        //    if (sqftUpper != 10000)
        //        link += string.Format("MaxSquareFootage={0}&", sqftUpper);

        //    link += string.Format("YearBuiltMin={0}&", yearBuiltLower);
        //    link += string.Format("YearBuiltMax={0}&", yearBuiltUpper);

        //    link += string.Format("IsAdvanced={0}&", isAdvanced);

        //    if (beds.HasValue)
        //        link += string.Format("Bedrooms={0}&", beds.Value);
        //    if (baths.HasValue)
        //        link += string.Format("Bathrooms={0}&", baths.Value);

        //    // property amenities
        //    if (amenities != null)
        //    {
        //        for (int i = 0; i < amenities.Length; ++i)
        //        {
        //            link += string.Format("amenities{0}={1}&", HttpUtility.UrlEncode("[]"), amenities[i]);
        //        }
        //    }            

        //    if (!string.IsNullOrEmpty(keywords))
        //        link += string.Format("Keywords={0}&", HttpUtility.UrlEncode(keywords));

        //    link = link.TrimEnd('&');

        //    return link;
        //}

        public static string RawPagerLink(Search model, int page, string text)
        {
            string generatedLink = GenerateLink(model, page);

            return string.Format(" <a href=\"{0}\" target=\"_top\">{1}</a> ", generatedLink, text);
        }

        public static string RawPagerLinks(this Search model)
        {
            string linkSet = "";

            int currentPage = model.Page;

            // generate first link
            if (currentPage > 3)
            {
                linkSet += RawPagerLink(model, 1, "First");
                linkSet += " ... ";
            }
            // generate the two before
            if (currentPage > 2)
            {
                linkSet += RawPagerLink(model, currentPage - 2, (currentPage - 2).ToString());
            }
            if (currentPage > 1)
            {
                linkSet += RawPagerLink(model, currentPage - 1, (currentPage - 1).ToString());
            }
            // add the current
            linkSet += string.Format(" {0} ", currentPage);
            // generate the two after
            if (currentPage < model.Results.TotalPages)
            {
                linkSet += RawPagerLink(model, currentPage + 1, (currentPage + 1).ToString());
            }
            if ((currentPage + 1) < model.Results.TotalPages)
            {
                linkSet += RawPagerLink(model, currentPage + 2, (currentPage + 2).ToString());
            }
            // generate the last link
            if ((currentPage + 2) < model.Results.TotalPages)
            {
                linkSet += " ... ";
                linkSet += RawPagerLink(model, model.Results.TotalPages, "Last");
            }
            return linkSet;
        }

        public static string PreviousLink(this Search model)
        {
            if (model.Page == 1)
                return "";

            return GenerateLink(model, model.Page - 1);            
        }

        public static bool ConfirmAdvanced(this Search model)
        {
            return
                model.Bedrooms.HasValue ||
                model.Bathrooms.HasValue ||
                model.MinSquareFootage.HasValue ||
                model.MaxSquareFootage.HasValue ||
                model.YearBuiltMin.HasValue ||
                model.YearBuiltMax.HasValue ||
                model.Amenities != null ||
                model.Terms != null ||
                model.LeaseLength != LeaseLength.Undefined ||
                model.PhotosOnly ||
                !string.IsNullOrWhiteSpace(model.Keywords);
        }
    }
}