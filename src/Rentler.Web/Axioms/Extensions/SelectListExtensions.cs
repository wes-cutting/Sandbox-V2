using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Rentler.Web.Axioms.Extensions
{
    public static class SelectListExtensions
    {
        public static string ToJsonArray(this SelectList list)
        {
            var itemArray = list.Select(i => new { text = i.Text, value = i.Value }).ToArray();
            return new JavaScriptSerializer().Serialize(itemArray);
        }
    }
}