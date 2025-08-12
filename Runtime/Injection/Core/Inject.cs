using System;

namespace Commons.Injection
{
    /// <summary>
    /// Marks a field to have a dependency injected into once, by <see cref="Binder"/>
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class Inject : Attribute
    {
        /// <summary>
        /// Allows defining a type by which dependency should be injected. Defaults to field type
        /// </summary>
        public readonly Type Type;

        /// <summary>
        /// Allows defining a callback method to call upon dependency injection. Defaults to On{type}Inject
        /// </summary>
        public readonly string Callback;

        public Inject(Type type, string callback = null)
        {
            Type = type;
            Callback = callback;
        }

        public Inject(string callback) : this(null, callback) { }
        
        public Inject() : this(null, null) { }
    }
}
