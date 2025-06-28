using System;
using System.Text.RegularExpressions;
using Commons.Constants;

namespace Commons.Extensions
{
    public static class StringExtensions
    {
        #region FIELDS
        
        private const string CamelCasePattern = @"(?<!^)(?=[A-Z])";

        #endregion
        
        /// <param name="input">The input to parse.</param>
        /// <returns>Returns an input string with first letter transformed to uppercase.</returns>
        public static string FirstToUpper(this string input) =>
            input switch
            {
                null => string.Empty,
                "" => string.Empty,
                _ => input[0].ToString().ToUpper() + input[1..]
            };
        
        /// <summary>
        /// Splits text before every capital letter.
        /// </summary>
        /// <param name="input">The input to parse.</param>
        /// <returns>Returns an input string with space character before each uppercase character.</returns>
        public static string SplitCamelCase(this string input) {
            var splitText = Regex.Split(input, CamelCasePattern);
            return string.Join(Msg.SPACE, splitText);
        }
        
        /// <summary>
        /// Checks if input string is http url.
        /// </summary>
        /// <param name="input">String to check.</param>
        /// <returns>True if http url, false otherwise.</returns>
        public static bool IsHttpUrl(this string input)
        {
            return !string.IsNullOrWhiteSpace(input) &&
                   (input.StartsWith("http://", StringComparison.OrdinalIgnoreCase) ||
                    input.StartsWith("https://", StringComparison.OrdinalIgnoreCase)
                   );
        }
    }
}
