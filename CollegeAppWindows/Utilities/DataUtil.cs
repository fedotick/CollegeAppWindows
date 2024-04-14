using System;
using System.Collections.Generic;
using System.Linq;

namespace CollegeAppWindows.Utilities
{
    /// <summary>
    /// Utility class for data manipulation and transformation.
    /// </summary>
    public class DataUtil
    {
        /// <summary>
        /// Returns a set of unique values from a list based on a selector function.
        /// </summary>
        /// <typeparam name="T">Type of the objects in the list.</typeparam>
        /// <param name="list">The list of objects to process.</param>
        /// <param name="selector">A function to extract a string property from each object in the list.</param>
        /// <returns>A HashSet containing unique, non-null, and non-empty values from the list.</returns>
        public static HashSet<string> GetUniqueValues<T>(List<T> list, Func<T, string> selector)
        {
            return list
                .Select(selector)
                .Where(value => !string.IsNullOrEmpty(value))
                .ToHashSet();
        }

        /// <summary>
        /// Filters a list based on a set of specific items and a selector function.
        /// </summary>
        /// <typeparam name="T">The type of objects in the list.</typeparam>
        /// <param name="list">The list of objects to filter.</param>
        /// <param name="specificItems">A HashSet of items used for filtering.</param>
        /// <param name="selector">A selector function to extract a property from an object.</param>
        /// <returns>A filtered list of objects.</returns>
        public static List<T> FilterBySpecificItems<T>(List<T> list, HashSet<string> specificItems, Func<T, string> selector)
        {
            return list
                .Where(item => specificItems.Contains(selector(item)))
                .ToList();
        }
    }
}
