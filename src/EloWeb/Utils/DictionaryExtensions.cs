using System.Collections.Generic;

namespace EloWeb.Utils
{
    public static class DictionaryExtensions
    {
        /// <summary>
        /// Returns the value associated with the key 
        /// or default(U) if the dictionary doesn't have the key
        /// </summary>
        public static U GetOrDefault<T, U>(this IDictionary<T, U> dictionary, T key)
        {
            U value;
            return dictionary.TryGetValue(key, out value) ? value : default(U);
        }
    }
}