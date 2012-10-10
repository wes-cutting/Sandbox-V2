using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KslListingImporter
{
	public static class StringExtensions
	{
		public static string Truncate(this string str, int maxLength)
		{
			if(str == null)
				return null;
			
			return str.Substring(0, Math.Min(maxLength, str.Length));
		}

		public static string TruncateElipsed(this string str, int maxLength)
		{
			return str.Truncate(497) + "...";
		}
	}
}
