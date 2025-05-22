using UnityEngine;

namespace Commons.Extensions
{
    public static class ComponentExtensions
    {
        /// <summary>
        /// Gets component if present on the GameObject or adds it.
        /// </summary>
        /// <param name="component">Component to perform operation on.</param>
        /// <param name="outputComponent">Requested component.</param>
        /// <typeparam name="TOutput">Component type to add or get.</typeparam>
        /// <returns>Whether or not requested component is NULL.</returns>
        public static bool AddOrGetComponent<TOutput>(this Component component, out TOutput outputComponent)
            where TOutput : Component
        {
            return component.gameObject.TryAddComponent(out outputComponent);
        }
    }
}
