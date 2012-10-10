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

        private static void UpdateItems()
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
            leaseOptions = newLeaseOptions;
        }        

        /// <summary>
        /// Gets all of the property types for searching.
        /// </summary>
        public static IEnumerable<SelectListItem> All
        {
            get
            {
                if (leaseOptions == null)
                    UpdateItems();
                return leaseOptions;
            }
        }
    }
}