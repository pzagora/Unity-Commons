using System;

namespace Commons.Attributes
{
    /// <summary>
    /// Attribute to annotate enum values with a localization key.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class LocalizedKeyAttribute : Attribute
    {
        public string Key { get; }

        public LocalizedKeyAttribute(string key)
        {
            Key = key;
        }
    }
}