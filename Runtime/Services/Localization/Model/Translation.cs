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
        [XmlAttribute("key")]
        public string key;

        [XmlElement("value")]
        public TranslationValue[] values;
    }
}
