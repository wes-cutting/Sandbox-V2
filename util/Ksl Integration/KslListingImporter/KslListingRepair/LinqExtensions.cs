using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KslListingRepair
{
	public static class LinqExtensions
	{
		public static IEnumerable<TSource> DistinctBy<TSource, TKey>
			(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
		{
			HashSet<TKey> seenKeys = new HashSet<TKey>();
			foreach(TSource element in source)
			{
				if(seenKeys.Add(keySelector(element)))
				{
					yield return element;
				}
			}
		}
	}

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
