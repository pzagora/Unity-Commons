using UnityEngine;

namespace Commons
{
    public static class RectTransformExtensions
    {
        /// <summary>
        /// Sets the anchored position of the RectTransform.
        /// </summary>
        /// <param name="target">The RectTransform to modify.</param>
        /// <param name="value">The anchored position to set.</param>
        public static void SetAnchored(this RectTransform target, Vector2 value)
            => target.anchoredPosition = value;

        /// <summary>
        /// Sets the X of the anchored position, leaving Y (and Z) unchanged.
        /// </summary>
        /// <param name="target">The RectTransform to modify.</param>
        /// <param name="x">The X value to set.</param>
        public static void SetAnchoredX(this RectTransform target, float x)
        {
            var position = target.anchoredPosition;
            position.x = x;
            target.anchoredPosition = position;
        }

        /// <summary>
        /// Sets the Y of the anchored position, leaving X (and Z) unchanged.
        /// </summary>
        /// <param name="target">The RectTransform to modify.</param>
        /// <param name="y">The Y value to set.</param>
        public static void SetAnchoredY(this RectTransform target, float y)
        {
            var position = target.anchoredPosition;
            position.y = y;
            target.anchoredPosition = position;
        }
    }
}
