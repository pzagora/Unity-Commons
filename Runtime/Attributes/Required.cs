using System;

namespace Commons.Attributes
{
    /// <summary>
    /// Flag that automatically makes sure selected variables are assigned and/or initialized.<para />
    /// Check happens at Start() call, so it is possible to fill variable in Awake() method.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class Required : Attribute
    {
    }
}