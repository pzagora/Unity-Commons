using System;
using System.Xml.Serialization;
using UnityEngine;

namespace Commons
{
    [Serializable]
    public abstract class Indexed : IIndexed
    {
        [SerializeField]
        private int id;

        [XmlAttribute("id")]
        public int Id
        {
            get => id;
            set => id = value;
        }
    }
}