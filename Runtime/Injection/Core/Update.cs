using System;
using Commons.Injection;

namespace Commons.Injection
{
    /// <summary>
    /// Marks a field to have a dependency kept updated into, by <see cref="Binder"/>
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class Update : Inject
    {
        public Update(Type type, string callback) : base(type, callback) { }
        public Update(Type type) : base(type) { }
        public Update(string callback) : base(null, callback) { }
        public Update() : base(null, null) { }
    }
}
