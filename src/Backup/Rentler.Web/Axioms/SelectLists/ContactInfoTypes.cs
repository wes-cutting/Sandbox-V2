using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Rentler.Web.Axioms.SelectLists
{
    public static class ContactInfoTypes
    {
        private static IEnumerable<SelectListItem> types;

        /// <summary>
        /// Updates the items.
        /// </summary>
        private static void UpdateItems()
        {
            List<SelectListItem> newTypes = new List<SelectListItem>();
            foreach (ContactInfoType type in Enum.GetValues(typeof(ContactInfoType)))
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

        /// <summary>
        /// Gets the items in the contact info types that are valid
        /// for creating or editing a contact info.
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
    }
}