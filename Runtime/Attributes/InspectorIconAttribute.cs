using System;

namespace Commons
{
    /// <summary>
    /// Names a built-in editor icon (as understood by <c>EditorGUIUtility.IconContent</c>) that a custom inspector can use
    /// to represent this field in compact, width-constrained layouts.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class InspectorIconAttribute : Attribute
    {
        public string Name { get; }

        public InspectorIconAttribute(string name)
            => Name = name;
    }
}
