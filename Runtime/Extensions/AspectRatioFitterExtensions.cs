using UnityEngine;
using UnityEngine.UI;

namespace Commons.Extensions
{
    public static class AspectRatioFitterExtensions
    {
        /// <summary>
        /// Reset aspect ratio and set selected mode.
        /// </summary>
        /// <param name="aspectRatioFitter">AspectRatioFitter to perform operations on.</param>
        /// <param name="mode">AspectMode to set.</param>
        public static void ResetWithAspectMode(this AspectRatioFitter aspectRatioFitter, AspectRatioFitter.AspectMode mode)
        {
            aspectRatioFitter.aspectMode = mode;
            aspectRatioFitter.aspectRatio = 1f;
        }
        
        /// <summary>
        /// Sizes the rectangle such that sprite is fully contained within the parent rectangle.
        /// </summary>
        /// <param name="aspectRatioFitter">AspectRatioFitter to perform operations on.</param>
        /// <param name="sprite">Sprite for dimensions check.</param>
        public static void FitToSprite(this AspectRatioFitter aspectRatioFitter, Sprite sprite)
        {
            aspectRatioFitter.ResetWithAspectMode(AspectRatioFitter.AspectMode.FitInParent);
            aspectRatioFitter.aspectRatio = sprite.rect.width / sprite.rect.height;
        }
        
        /// <summary>
        /// Sizes the rectangle such that the parent rectangle is fully contained within sprite.
        /// </summary>
        /// <param name="aspectRatioFitter">AspectRatioFitter to perform operations on.</param>
        /// <param name="sprite">Sprite for dimensions check.</param>
        public static void EnvelopeWithSprite(this AspectRatioFitter aspectRatioFitter, Sprite sprite)
        {
            aspectRatioFitter.ResetWithAspectMode(AspectRatioFitter.AspectMode.EnvelopeParent);
            aspectRatioFitter.aspectRatio = sprite.rect.width / sprite.rect.height;
        }
    }
}
