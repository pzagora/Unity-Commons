using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Commons
{
    public static class Wait
    {
        /// <summary>
        /// Waits until the getter's value is approximately equal to the target using Mathf.Approximately.
        /// </summary>
        public static async Task UntilApproximately(Func<float> getter, float target)
        {
            while (!Mathf.Approximately(getter(), target))
                await Task.Yield();
        }
    }
}