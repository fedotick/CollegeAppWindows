using System.Collections.Generic;
using System.Linq;

namespace CollegeAppWindows.Utilities
{
    /// <summary>
    /// Provides utility methods for working with a list of SelectableItem objects.
    /// </summary>
    public class SelectableItemUtil
    {
        /// <summary>
        /// Adds text values from a HashSet to a list of SelectableItem objects.
        /// </summary>
        /// <param name="set">The HashSet containing text values to add.</param>
        /// <param name="items">The list of SelectableItem objects to which the text values will be added.</param>
        public static void AddTextToSelectableItems(HashSet<string> set, List<SelectableItem> items)
        {
            foreach (var text in set)
            {
                items.Add(new SelectableItem { Text = text });
            }
        }

        /// <summary>
        /// Returns a HashSet of strings from a list of SelectableItem objects where the IsChecked property is true.
        /// </summary>
        /// <param name="items">The list of SelectableItem objects to process.</param>
        /// <returns>A HashSet of strings containing the Text property of items where IsChecked is true.</returns>
        public static HashSet<string> GetCheckedItemsHashSet(List<SelectableItem> items)
        {
            return items
                .Where(item => item.IsChecked)
                .Select(item => item.Text)
                .ToHashSet();
        }
    }
}
