using System;
using UnityEngine;

namespace Commons.Collections
{
    /// <summary>
    /// A (key, value) tuple that can be used with Unity's serialization system. 
    /// By default, it is used inside the <c>SerializableDictionary</c>.
    /// </summary>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    [Serializable]
    public class SerializableTuple<TKey, TValue>
    {
        public TKey Key
        {
            get => key;
            set => key = value;
        }

        public TValue Value
        {
            get => value;
            set => this.value = value;
        }

        [SerializeField]
        protected TKey key;
        [SerializeField]
        protected TValue value;
    }

}
