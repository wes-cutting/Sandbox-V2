using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Rentler.Web.Axioms.SelectLists
{
    /// <summary>
    /// Class retrieving select lists for mvc based on
    /// the available property types within the system.
    /// </summary>
    public static class PropertyTypes
    {
        private static IEnumerable<SelectListItem> types;

        private static void UpdateItems()
        {
            List<SelectListItem> newTypes = new List<SelectListItem>();
            foreach (PropertyType type in Enum.GetValues(typeof(PropertyType)))
            {
                newTypes.Add(new SelectListItem()
                {
                    Text = type.Description(),
                    Value = ((int)type).ToString()
                });
            }
            types = newTypes;
        }

        /// <summary>
        /// Gets the items in the property types that are valid
        /// for creating or editing a property.
        /// </summary>
        public static IEnumerable<SelectListItem> OnlyValid
        {
            get
            {
                if (types == null)
                    UpdateItems();
                return types.Where(m => m.Value != "0").ToArray();
            }
        }

        /// <summary>
        /// Gets all of the property types for searching.
        /// </summary>
        public static IEnumerable<SelectListItem> All
        {
            get
            {
                if (types == null)
                    UpdateItems();
                return types;
            }
        }
    }
}