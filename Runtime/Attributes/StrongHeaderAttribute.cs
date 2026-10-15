using System;
using Commons.Editor;
using UnityEngine;

namespace Commons
{
    [AttributeUsage(AttributeTargets.Field)]
    public class StrongHeaderAttribute : PropertyAttribute
    {
        public string Text { get; }
        public bool LineSeparator { get; }

        public StrongHeaderAttribute(string text, bool lineSeparator = true, bool nicify = true)
        {
            Text = nicify ? text.GetInspectorDisplayName() : text;
            LineSeparator = lineSeparator;
        }
    }
}