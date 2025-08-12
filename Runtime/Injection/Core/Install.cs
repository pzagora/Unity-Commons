using System;

namespace Commons.Injection
{
    /// <summary>
    /// Marks a class to have itself installed as a dependency by <see cref="Binder"/>
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class Install : Attribute
    {
        /// <summary>
        /// Allows defining a type by which dependency should be installed. Defaults to class type
        /// </summary>
        public readonly Type Type;

        public Install(Type type)
            => Type = type;

        public Install() : this(null) { }
    }
}
