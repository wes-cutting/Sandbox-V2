using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Rentler.Web.Axioms.SelectLists
{
    public static class LeaseOptions
    {
        private static IEnumerable<SelectListItem> leaseOptions;
        private static IEnumerable<SelectListItem> searchLeaseOptions;

        private static List<SelectListItem> GetLeaseOptions()
        {
            List<SelectListItem> newLeaseOptions = new List<SelectListItem>();
            foreach (LeaseLength ll in Enum.GetValues(typeof(LeaseLength)))
            {
                newLeaseOptions.Add(new SelectListItem()
                {
                    Text = ll.Description(),
                    Value = ((int)ll).ToString()
                });
            }
            return newLeaseOptions;
        }

        private static void UpdateAll()
        {
            leaseOptions = GetLeaseOptions();            
        }

        private static void UpdateForSearch()
        {
            var options = GetLeaseOptions();
            
            // override Undefined as Lease Length for UI
            var undefined = options.SingleOrDefault(o => o.Value == "0");
            if (undefined != null)
                undefined.Text = "Lease Length";

            searchLeaseOptions = options;                       
        }

        /// <summary>
        /// Gets all of the lease options.
        /// </summary>
        public static IEnumerable<SelectListItem> All
        {
            get
            {
                if (leaseOptions == null)
                    UpdateAll();
                return leaseOptions;
            }
        }

        public static IEnumerable<SelectListItem> Searchable
        {
            get
            {
                if (searchLeaseOptions == null)
                    UpdateForSearch();
                return searchLeaseOptions;
            }
        }
    }
}