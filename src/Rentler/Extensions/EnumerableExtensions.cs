using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rentler.Extensions
{
	public static class ListExtensions
	{
		static Random rand = new Random();

		/// <summary>
		/// Randomly shuffles the order of an enumerable source, using
		/// a more efficient O(n) version of the Fisher-Yates shuffle algorithm.
		/// </summary>
		/// <typeparam name="T">The type of object in the enumerable source</typeparam>
		/// <param name="source">The enumerable source to randomly shuffle.</param>
		/// <returns>The enumerable source, randomly shuffled.</returns>
		public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
		{
			T[] elements = source.ToArray();
			for(int i = elements.Length - 1; i >= 0; i--)
			{
				// Swap element "i" with a random earlier element it (or itself)
				// ... except we don't really need to swap it fully, as we can
				// return it immediately, and afterwards it's irrelevant.
				int swapIndex = rand.Next(i + 1);
				yield return elements[swapIndex];
				elements[swapIndex] = elements[i];
			}
		}
	}

	public static class EnumerableExtensions
	{
		public static IEnumerable<TSource> DistinctBy<TSource, TKey>
			(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
		{
			HashSet<TKey> seenKeys = new HashSet<TKey>();
			foreach (TSource element in source)
			{
				if (seenKeys.Add(keySelector(element)))
				{
					yield return element;
				}
			}
		}
	}
}
