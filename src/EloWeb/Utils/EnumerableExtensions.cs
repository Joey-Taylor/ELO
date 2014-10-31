using System;
using System.Collections.Generic;

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
    }
}