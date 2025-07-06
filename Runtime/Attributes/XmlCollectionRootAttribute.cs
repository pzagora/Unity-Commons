using System;
using System.Xml.Serialization;

namespace Commons.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class XmlCollectionRootAttribute : XmlRootAttribute
    {
        public XmlCollectionRootAttribute(string elementName) : base(elementName) { }
    }
}
