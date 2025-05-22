using System;
using UnityEngine;

namespace Commons.Extensions
{
    public static class CanvasGroupExtensions
    {
        /// <summary>
        /// Enables given CanvasGroup
        /// </summary>
        /// <param name="input">The input to parse.</param>
        public static void Show(this CanvasGroup input) => input.Toggle(true);
        
        /// <summary>
        /// Disables given CanvasGroup
        /// </summary>
        /// <param name="input">The input to parse.</param>
        public static void Hide(this CanvasGroup input) => input.Toggle(false);
        
        /// <summary>
        /// Toggles given CanvasGroup
        /// </summary>
        /// <param name="input">The input to parse.</param>
        /// <param name="isEnabled">State to set for input CanvasGroup.</param>
        public static void Toggle(this CanvasGroup input, bool isEnabled)
        {
            input.alpha = Convert.ToInt32(isEnabled);
            input.interactable = isEnabled;
            input.blocksRaycasts = isEnabled;
        }

        /// <summary>
        /// Changes CanvasGroup alpha value to provided, clamped between min and max
        /// </summary>
        /// <param name="input">The input to parse.</param>
        /// <param name="value">Value to clamp and set as alpha.</param>
        public static void Alpha(this CanvasGroup input, float value)
        {
            input.alpha = Mathf.Clamp01(value);
        }
    }
}
