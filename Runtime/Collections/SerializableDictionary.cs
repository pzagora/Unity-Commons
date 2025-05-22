using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
// ReSharper disable UnusedMember.Global

namespace Commons.Collections
{
    [Serializable]
    public class SerializableDictionary {}

    /// <summary>
    /// Implementation of dictionary that you can use with unity serialization system. Both key and value type have to
    /// be serializable
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    [Serializable]
    public class SerializableDictionary<TKey, TValue> : 
        SerializableDictionary, 
        IEnumerable<SerializableDictionary<TKey, TValue>.Tuple>
    {
        public int Count => data.Count;

        public TValue this[TKey key]
        {
            get => GetValueForKey(key);
            set
            {
                var tuple = data.FirstOrDefault(d => Comparer.Default.Compare(d.Key, key) == 0);
                if (tuple != null)
                {
                    tuple.Value = value;
                }
                else
                {
                    data.Add(new Tuple {Key = key, Value = value});
                }
            }
        }

        [Serializable] public class Tuple : SerializableTuple<TKey, TValue>{}

        [SerializeField]
        protected List<Tuple> data = new();

        public TValue GetValueForKey(TKey key, TValue defaultValue = default)
        {
            foreach (var d in data.Where(d => CompareKeys(key, d.Key))) 
                return d.Value;
            
            return defaultValue;
        }

        public bool ContainsKey(TKey key)
        {
            return data.Any(d => CompareKeys(key, d.Key));
        }

        public void RemoveKey(TKey key) => data.RemoveAll(KeyPredicate(key));
        public int GetIndex(TKey key) => data.FindIndex(KeyPredicate(key));

        private static bool CompareKeys(TKey key1, TKey key2) => Comparer<TKey>.Default.Compare(key1, key2) == 0;
        private static Predicate<Tuple> KeyPredicate(TKey key) => t => CompareKeys(key, t.Key); 
        
        public IEnumerator<Tuple> GetEnumerator() => data.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerable<TKey> Keys => data.Select(d => d.Key);
    }
}