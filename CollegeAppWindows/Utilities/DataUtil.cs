using System;
using System.Collections.Generic;
using System.Linq;
/* 
using System.Reflection.PortableExecutable;
using System.Reflection; 
*/

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

        /*
        public static List<T> Search<T>(List<T> list, string searchValue)
        {
            //List<T> result = new List<T>();

            //foreach (T item in list)
            //{
            //    FieldInfo[] fields = typeof(T).GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            //    foreach (FieldInfo field in fields)
            //    {
            //        string fieldName = field.Name;
            //        string fieldValue = field.GetValue(item).ToString();

            //        if (fieldValue.Contains(searchValue))
            //        {
            //            result.Add(item);
            //            break;
            //        }
            //    }
            //}

            //return result;

            List<T> result = new List<T>();
            HashSet<T> uniqueResults = new HashSet<T>();

            foreach (T item in list)
            {
                try
                {
                    FieldInfo[] fields = typeof(T).GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

                    foreach (FieldInfo field in fields)
                    {
                        object value = field.GetValue(item);

                        if (value != null && value.ToString().IndexOf(searchValue, comparisonType) >= 0)
                        {
                            if (uniqueResults.Add(item))
                            {
                                result.Add(item);
                            }

                            break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing item: {ex.Message}");
                }
            }

            return result;
        }
        */
    }
}
