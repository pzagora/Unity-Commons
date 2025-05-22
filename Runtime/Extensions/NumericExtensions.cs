using System;

namespace Commons.Extensions
{
    public static class NumericExtensions
    {
        public const int Zero = 0;
        
        /// <summary>
        /// Checks if value is 0 using default or double.Epsilon comparison.
        /// </summary>
        /// <param name="value">Value to check.</param>
        /// <typeparam name="T">Suitable value type.</typeparam>
        /// <returns>True if value is zero, False otherwise.</returns>
        public static bool IsZero<T>(this T value) where T : struct, IEquatable<T>
        {
            if (typeof(T) == typeof(float) || typeof(T) == typeof(double))
            {
                return Math.Abs(Convert.ToDouble(value)) < double.Epsilon;
            }
            
            return value.Equals(default);
        }

        /// <summary>
        /// Checks if value is NOT 0 using default or double.Epsilon comparison.
        /// </summary>
        /// <param name="value">Value to check.</param>
        /// <typeparam name="T">Suitable value type.</typeparam>
        /// <returns>True if value is NOT zero, False otherwise.</returns>
        public static bool NotZero<T>(this T value) where T : struct, IEquatable<T> => !IsZero(value);
    }
}
