using System;
using System.Collections.Generic;

namespace Common
{
    public static class DictionaryExtensions
    {
        public static bool IsEmpty<TKey, TValue>(this IDictionary<TKey, TValue> self)
        {
            return self.Count == 0;
        }

        public static bool IsNullOrEmpty<TKey, TValue>(this IDictionary<TKey, TValue> self)
        {
            return self == null || self.IsEmpty();
        }

        public static bool ContainsAllKeys<TKey, TValue>(this IDictionary<TKey, TValue> self, IEnumerable<TKey> keys)
        {
            foreach (var key in keys)
            {
                if (!self.ContainsKey(key))
                {
                    return false;
                }
            }
            return true;
        }

        public static bool ContainsAllKeys<TKey, TValue>(this Dictionary<TKey, TValue> self, params TKey[] keys)
        {
            for (int i = 0; i < keys.Length; ++i)
            {
                if (!self.ContainsKey(keys[i]))
                {
                    return false;
                }
            }
            return true;
        }

        public static bool ContainsAllValues<TKey, TValue>(this Dictionary<TKey, TValue> self, IEnumerable<TValue> values)
        {
            foreach (var value in values)
            {
                if (!self.ContainsValue(value))
                {
                    return false;
                }
            }
            return true;
        }

        public static bool ContainsAllValues<TKey, TValue>(this Dictionary<TKey, TValue> self, params TValue[] values)
        {
            for (int i = 0; i < values.Length; ++i)
            {
                if (!self.ContainsValue(values[i]))
                {
                    return false;
                }
            }
            return true;
        }

        public static Dictionary<TNewKey, TValue> ConvertKeys<TOldKey, TNewKey, TValue>(this Dictionary<TOldKey, TValue> self, Converter<TOldKey, TNewKey> converter)
        {
            var result = new Dictionary<TNewKey, TValue>(self.Count);
            foreach (var entry in self)
            {
                var newKey = converter(entry.Key);
                result[newKey] = entry.Value;
            }
            return result;
        }

        public static Dictionary<TKey, TNewValue> ConvertValues<TKey, TOldValue, TNewValue>(this Dictionary<TKey, TOldValue> self, Converter<TOldValue, TNewValue> converter)
        {
            var result = new Dictionary<TKey, TNewValue>(self.Count);
            foreach (var entry in self)
            {
                var newValue = converter(entry.Value);
                result[entry.Key] = newValue;
            }
            return result;
        }

        public static Dictionary<TNewKey, TNewValue> ConvertAll<TOldKey, TNewKey, TOldValue, TNewValue>(this Dictionary<TOldKey, TOldValue> self, Converter<TOldKey, TNewKey> keyConverter, Converter<TOldValue, TNewValue> valueConverter)
        {
            var result = new Dictionary<TNewKey, TNewValue>(self.Count);
            foreach (var entry in self)
            {
                var newKey = keyConverter(entry.Key);
                var newValue = valueConverter(entry.Value);
                result[newKey] = newValue;
            }
            return result;
        }

        public static bool Remove<TKey, TValue>(this IDictionary<TKey, TValue> self, IEnumerable<TKey> keys)
        {
            var result = true;
            foreach (var key in keys)
            {
                result &= self.Remove(key);
            }
            return result;
        }

        public static TValue Revoke<TKey, TValue>(this IDictionary<TKey, TValue> self, TKey key)
        {
            self.TryGetValue(key, out var result);
            self.Remove(key);
            return result;
        }

        public static bool TryRevoke<TKey, TValue>(this IDictionary<TKey, TValue> self, TKey key, out TValue value)
        {
            var result = self.TryGetValue(key, out value);
            self.Remove(key);
            return result;
        }

        public static TKey FirstOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> self, TKey value = default)
        {
            foreach (var entry in self)
            {
                return entry.Key;
            }
            return value;
        }

        public static TValue GetOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> self, TKey key, TValue value = default)
        {
            return self.TryGetValue(key, out var result) ? result : value;
        }

        public static TValue GetOrCompute<TKey, TValue>(this IDictionary<TKey, TValue> self, TKey key, Func<TValue> computor)
        {
            if (!self.TryGetValue(key, out var result))
            {
                self[key] = result = computor();
            }
            return result;
        }
    }
}
