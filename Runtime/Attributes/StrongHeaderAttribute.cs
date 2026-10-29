using System;
using UnityEngine;
#if UNITY_EDITOR
using Commons.Editor;
#endif

namespace Commons
{
    [AttributeUsage(AttributeTargets.Field)]
    public class StrongHeaderAttribute : PropertyAttribute
    {
        public string Text { get; }
        public bool LineSeparator { get; }

        public StrongHeaderAttribute(string text, bool lineSeparator = true, bool nicify = true)
        {
#if UNITY_EDITOR
            Text = nicify ? text.GetInspectorDisplayName() : text;
#else
            Text = text;
#endif
            LineSeparator = lineSeparator;
        }
    }
}