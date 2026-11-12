using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Commons.Editor
{
    public static class TMPInspectorToolRegistry
    {
        private static readonly List<Func<TextMeshProUGUI, bool>> Drawers = new();

        public static void Register(Func<TextMeshProUGUI, bool> drawer)
        {
            if (drawer == null)
                throw new ArgumentNullException(nameof(drawer));

            Drawers.Remove(drawer);
            Drawers.Add(drawer);
        }

        public static void Unregister(Func<TextMeshProUGUI, bool> drawer)
            => Drawers.Remove(drawer);

        internal static bool Draw(TextMeshProUGUI target)
        {
            foreach (var drawer in Drawers)
            {
                try
                {
                    if (drawer(target))
                        return true;
                }
                catch (Exception exception)
                {
                    Debug.LogException(exception);
                }
            }

            return false;
        }
    }
}
