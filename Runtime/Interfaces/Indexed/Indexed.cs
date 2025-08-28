using System;
using System.Xml.Serialization;
using UnityEngine;

namespace Commons
{
    [Serializable]
    public abstract class Indexed : IIndexed
    {
        [SerializeField]
        private string id;

        [XmlAttribute("id")]
        public string Id
        {
            get => id;
            set => id = value;
        }
    }
}