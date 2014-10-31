using System.Collections.Generic;

namespace EloWeb.Utils
{
    public static class StringExtensions
    {
        /// <summary>
        /// Returns the last n characters of the string or the whole 
        /// string if it is less than n characters long 
        /// </summary>
        public static string Last(this string str, int n)
        {
            return str.Length <= n ? str : str.Substring(str.Length - n, n);
        }

        /// <summary>
        /// Same as string.Join
        /// </summary>
        public static string Join(this IEnumerable<string> str, string separator)
        {
            return string.Join(separator, str);
        }
    }
}