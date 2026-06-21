using System;
using UnityEngine;

namespace Commons
{
    [AttributeUsage(AttributeTargets.Field)]
    public class StrongHeaderAttribute : PropertyAttribute
    {
        public string Text { get; }
        public bool LineSeparator { get; }

        public StrongHeaderAttribute(string text, bool lineSeparator = true)
        {
            Text = text;
            LineSeparator = lineSeparator;
        }
    }
}