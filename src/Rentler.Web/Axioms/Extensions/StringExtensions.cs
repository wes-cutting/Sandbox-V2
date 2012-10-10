using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;

namespace Rentler.Web.Axioms.Extensions
{
    public static class StringExtensions
    {
        public static string StripNonAscii(this string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return string.Empty;

            text = text
                .Replace("\r\n", " ")
                .Replace("\n", " ")
                .Replace("\t", " ");
            
            return Regex.Replace(text, @"[^\u0000-\u007F]", string.Empty);
        }
    }
}