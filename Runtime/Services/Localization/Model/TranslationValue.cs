using System;
using System.Xml.Serialization;

namespace Commons.Services
{
    [Serializable]
    public class TranslationValue
    {
        [XmlAttribute("lang")]
        public string lang;

        [XmlText]
        public string text;
    }
}
