using System;
using System.Xml.Serialization;
using Commons.Attributes;

namespace Commons.Services
{
    [Serializable]
    [XmlRoot("translation")]
    [XmlCollectionRoot("translations")]
    public class Translation : Indexed
    {
        [XmlElement("value")]
        public TranslationValue[] values;
    }
}
