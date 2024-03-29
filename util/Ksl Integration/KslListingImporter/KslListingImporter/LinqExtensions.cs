﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KslListingImporter
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
}
