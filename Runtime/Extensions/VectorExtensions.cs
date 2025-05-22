using System;
using UnityEngine;
using Commons.Enums;

namespace Commons.Extensions
{
    public static class VectorExtensions
    {
        /// <summary>
        /// Returns vector ignoring given component value.
        /// </summary>
        /// <param name="vector">Vector2 to ignore component in.</param>
        /// <param name="component">Vector2 component to ignore.</param>
        /// <returns>Returns Vector2 with given component set to 0.</returns>
        public static Vector2 IgnoreComponent(this Vector2 vector, Vector2Component component)
        {
            return component switch
            {
                Vector2Component.X => new Vector2(0, vector.y),
                Vector2Component.Y => new Vector2(vector.x, 0),
                _ => throw new ArgumentOutOfRangeException(nameof(component), component, null)
            };
        }
        
        /// <summary>
        /// Returns vector ignoring given component value.
        /// </summary>
        /// <param name="vector">Vector3 to ignore component in.</param>
        /// <param name="component">Vector3 component to ignore.</param>
        /// <returns>Returns Vector3 with given component set to 0.</returns>
        public static Vector3 IgnoreComponent(this Vector3 vector, Vector3Component component)
        {
            return component switch
            {
                Vector3Component.X => new Vector3(0, vector.y, vector.z),
                Vector3Component.Y => new Vector3(vector.x, 0, vector.z),
                Vector3Component.Z => new Vector3(vector.x, vector.y, 0),
                _ => throw new ArgumentOutOfRangeException(nameof(component), component, null)
            };
        }
    }
}
