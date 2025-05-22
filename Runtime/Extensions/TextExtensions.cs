using TMPro;
using UnityEngine.UI;

namespace Commons.Extensions
{
    public static class TextExtensions
    {
        /// <summary>
        /// Clears TMP_Text.text value of a given input.
        /// </summary>
        /// <param name="input">The input to clear.</param>
        public static void Clear(this TMP_Text input)
        {
            input.text = string.Empty;
        }
        
        /// <summary>
        /// Clears Text.text value of a given input.
        /// </summary>
        /// <param name="input">The input to clear.</param>
        public static void Clear(this Text input)
        {
            input.text = string.Empty;
        }
    }
}
