using System;
using UnityEngine;

namespace Commons.Extensions
{
    public static class NumericExtensions
    {
        public const int Zero = 0;
        public const int One = 1;
        
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
        
        /// <summary>
        /// Clamp value to be not less than zero.
        /// </summary>
        public static int ClampToPositive(this int value)
            => value < Zero ? Zero : value;

        /// <summary>
        /// Clamp value to be not less than zero.
        /// </summary>
        public static float ClampToPositive(this float value)
            => value < Zero ? Zero : value;
        
        /// <summary>
        /// Floors the provided float value to zero.
        /// </summary>
        public static int FloorToInt(this float value)
            => Mathf.FloorToInt(value);

        public static float ToAdditionalPercentageMultiplier(this int value)
            => 1 + value.ToPercentageMultiplier();
        
        public static float ToPercentageMultiplier(this int value)
            => value / 100f;
    }
}
