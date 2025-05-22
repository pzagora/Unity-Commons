using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine.UI;

namespace Commons.Extensions
{
    public static class ListExtensions
    {
        /// <summary>
        /// Clears all TMP_Text.text values in a given input list.
        /// </summary>
        /// <param name="input">The input list to parse.</param>
        public static void ClearTexts(this List<TMP_Text> input)
        {
            if (input.IsNullOrEmpty())
                return;
            
            input.ForEach(text => text.Clear());
        }
        
        /// <summary>
        /// Clears all Text.text values in a given input list.
        /// </summary>
        /// <param name="input">The input list to parse.</param>
        public static void ClearTexts(this List<Text> input)
        {
            if (input.IsNullOrEmpty())
                return;
            
            input.ForEach(text => text.Clear());
        }
        
        /// <summary>
        /// Checks if actions can be performed on given input.
        /// </summary>
        /// <param name="input">The input list to parse.</param>
        public static bool IsNullOrEmpty<T>(this List<T> input) => input == null || !input.Any();

        /// <summary>
        /// Checks if actions can be performed on given input.
        /// </summary>
        /// <param name="input">The input list to parse.</param>
        public static bool NotNullNorEmpty<T>(this List<T> input) => input != null && input.Any();
    }
}
