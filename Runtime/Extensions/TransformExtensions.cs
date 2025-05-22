using UnityEngine;

namespace Commons.Extensions
{
    public static class TransformExtensions
    {
        /// <summary>
        /// Sets the layer of the transform's children.
        /// </summary>
        /// <param name="transform">Transform to modify.</param>
        /// <param name="layerName">Name of layer.</param>
        /// <param name="recurrently">Also set all ancestor layers?</param>
        public static void SetChildLayers(this Transform transform, string layerName, bool recurrently = true)
        {
            var layer = LayerMask.NameToLayer(layerName);
            transform.gameObject.layer = layer;
            SetChildLayersHelper(transform, layer, recurrently);
        }
        
        /// <summary>
        /// Sets the x component of the transform's position.
        /// </summary>
        /// <param name="transform">Transform to modify.</param>
        /// <param name="x">Value of x.</param>
        public static void SetX(this Transform transform, float x)
        {
            var position = transform.position;
            position = new Vector3(x, position.y, position.z);
            transform.position = position;
        }

        /// <summary>
        /// Sets the y component of the transform's position.
        /// </summary>
        /// <param name="transform">Transform to modify.</param>
        /// <param name="y">Value of y.</param>
        public static void SetY(this Transform transform, float y)
        {
            var position = transform.position;
            position = new Vector3(position.x, y, position.z);
            transform.position = position;
        }

        /// <summary>
        /// Sets the z component of the transform's position.
        /// </summary>
        /// <param name="transform">Transform to modify.</param>
        /// <param name="z">Value of z.</param>
        public static void SetZ(this Transform transform, float z)
        {
            var position = transform.position;
            position = new Vector3(position.x, position.y, z);
            transform.position = position;
        }

        #region Helper Methods
        
        private static void SetChildLayersHelper(Transform transform, int layer, bool recurrently)
        {
            foreach (Transform child in transform)
            {
                child.gameObject.layer = layer;

                if (recurrently)
                {
                    SetChildLayersHelper(child, layer, true);
                }
            }
        }

        #endregion
    }
}
