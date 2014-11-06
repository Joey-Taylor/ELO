using System;
using System.Collections.Generic;
using System.Linq;

namespace EloWeb.Utils
{
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Returns the length of the longest sequence which passes the predicate.
        /// </summary>
        public static int LengthOfLongestSequence<T>(this IEnumerable<T> enumerable, Predicate<T> predicate)
        {
            var longestSequence = 0;
            var currentSequence = 0;

            foreach (var elem in enumerable)
            {
                if (predicate(elem)) currentSequence++;
                else
                {
                    if (currentSequence > longestSequence)
                    {
                        longestSequence = currentSequence;
                    }
                    currentSequence = 0;
                }
            }

            if (currentSequence > longestSequence)
            {
                longestSequence = currentSequence;
            }

            return longestSequence;
        }

        /// <summary>
        /// Returns the elements of the array which maximise the selector
        /// </summary>
        public static IEnumerable<T> MaxByAll<T, P>(this IEnumerable<T> enumerable, Func<T, P> selector)
        {
            var ordered = enumerable.OrderByDescending(selector);

            if (!ordered.Any()) return new T[0];

            var max = selector(ordered.First());
            return ordered.TakeWhile(t => Equals(selector(t), max));
        }
    }
}