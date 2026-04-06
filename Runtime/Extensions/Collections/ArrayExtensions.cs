using System;
using System.Collections.Generic;

namespace Common
{
    public static class ArrayExtensions
    {
        public static bool IsEmpty<T>(this T[] self)
        {
            return self.Length == 0;
        }

        public static bool IsNullOrEmpty<T>(this T[] self)
        {
            return self == null || self.IsEmpty();
        }

        public static T GetAt<T>(this T[] self, int index)
        {
            return self[index];
        }
        
        public static T First<T>(this T[] self)
        {
            return self[0];
        }

        public static T FirstOrDefault<T>(this T[] self, T value = default)
        {
            return self.IsEmpty() ? value : self.First();
        }

        public static T Last<T>(this T[] self)
        {
            return self[self.Length - 1];
        }

        public static T LastOrDefault<T>(this T[] self, T value = default)
        {
            return self.IsEmpty() ? value : self.Last();
        }

        public static bool TryGetAt<T>(this T[] self, int index, out T item)
        {
            if (-1 < index && index < self.Length)
            {
                item = self[index];
                return true;
            }
            item = default;
            return false;
        }

        public static bool TryGetFirst<T>(this T[] self, out T item)
        {
            return self.TryGetAt(0, out item);
        }

        public static bool TryGetLast<T>(this T[] self, out T item)
        {
            return self.TryGetAt(self.Length - 1, out item);
        }

        public static bool Contains<T>(this T[] self, T value)
        {
            return Array.IndexOf(self, value) != -1;
        }

        public static bool ContainsAll<T>(this T[] self, IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                if (!self.Contains(item))
                {
                    return false;
                }
            }
            return true;
        }

        public static bool ContainsAny<T>(this T[] self, IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                if (self.Contains(item))
                {
                    return true;
                }
            }
            return false;
        }

        public static TOut[] ConvertAll<T, TOut>(this T[] self, Converter<T, TOut> converter)
        {
            return Array.ConvertAll(self, converter);
        }

        public static List<TOut> ConvertAll<T, TOut>(this T[] self, Converter<T, TOut> converter, List<TOut> target)
        {
            target.Clear();
            for (int i = 0; i < self.Length; ++i)
            {
                var item = converter(self[i]);
                target.Add(item);
            }
            return target;
        }

        public static Dictionary<TKey, TValue> ConvertAll<T, TKey, TValue>(this T[] self, Converter<T, TKey> keygen, Converter<T, TValue> valuegen, Dictionary<TKey, TValue> target)
        {
            target.Clear();
            for (int i = 0; i < self.Length; ++i)
            {
                var item = self[i];
                var key = keygen(item);
                var value = valuegen(item);
                target.Add(key, value);
            }
            return target;
        }

        public static void Clear<T>(this T[] self, int index, int length)
        {
            Array.Clear(self, index, length);
        }

        public static void Clear<T>(this T[] self, int index)
        {
            Array.Clear(self, index, self.Length - index);
        }

        public static void Clear<T>(this T[] self)
        {
            Array.Clear(self, 0, self.Length);
        }

        public static int IndexOf<T>(this T[] self, T value)
        {
            return Array.IndexOf(self, value);
        }

        public static int IndexOf<T>(this T[] self, T value, int startIndex)
        {
            return Array.IndexOf(self, value, startIndex);
        }

        public static int IndexOf<T>(this T[] self, T value, int startIndex, int count)
        {
            return Array.IndexOf(self, value, startIndex, count);
        }

        public static bool Exists<T>(this T[] self, Predicate<T> match)
        {
            return Array.Exists(self, match);
        }

        public static T Find<T>(this T[] self, Predicate<T> match)
        {
            return Array.Find(self, match);
        }

        public static T FindLast<T>(this T[] self, Predicate<T> match)
        {
            return Array.FindLast(self, match);
        }

        public static T[] FindAll<T>(this T[] self, Predicate<T> match)
        {
            return Array.FindAll(self, match);
        }

        public static int FindIndex<T>(this T[] self, Predicate<T> match)
        {
            return Array.FindIndex(self, match);
        }

        public static int FindIndex<T>(this T[] self, int startIndex, Predicate<T> match)
        {
            return Array.FindIndex(self, startIndex, match);
        }

        public static int FindIndex<T>(this T[] self, int startIndex, int count, Predicate<T> match)
        {
            return Array.FindIndex(self, startIndex, count, match);
        }

        public static int FindLastIndex<T>(this T[] self, Predicate<T> match)
        {
            return Array.FindLastIndex(self, match);
        }

        public static int FindLastIndex<T>(this T[] self, int startIndex, Predicate<T> match)
        {
            return Array.FindLastIndex(self, startIndex, match);
        }

        public static int FindLastIndex<T>(this T[] self, int startIndex, int count, Predicate<T> match)
        {
            return Array.FindLastIndex(self, startIndex, count, match);
        }

        public static bool TryIndexOf<T>(this T[] self, T value, out int index)
        {
            return (index = Array.IndexOf(self, value)) != -1;
        }

        public static bool TryIndexOf<T>(this T[] self, T value, int startIndex, out int index)
        {
            return (index = Array.IndexOf(self, value, startIndex)) != -1;
        }

        public static bool TryIndexOf<T>(this T[] self, T value, int startIndex, int count, out int index)
        {
            return (index = Array.IndexOf(self, value, startIndex, count)) != -1;
        }

        public static bool TryFind<T>(this T[] self, Predicate<T> match, out T value)
        {
            return !Equals(value = Array.Find(self, match), default);
        }

        public static bool TryFindLast<T>(this T[] self, Predicate<T> match, out T value)
        {
            return !Equals(value = Array.FindLast(self, match), default);
        }

        public static bool TryFindAll<T>(this T[] self, Predicate<T> match, out T[] value)
        {
            return !(value = Array.FindAll(self, match)).IsNullOrEmpty();
        }

        public static bool TryFindIndex<T>(this T[] self, Predicate<T> match, out int index)
        {
            return (index = Array.FindIndex(self, match)) != -1;
        }

        public static bool TryFindIndex<T>(this T[] self, int startIndex, Predicate<T> match, out int index)
        {
            return (index = Array.FindIndex(self, startIndex, match)) != -1;
        }

        public static bool TryFindIndex<T>(this T[] self, int startIndex, int count, Predicate<T> match, out int index)
        {
            return (index = Array.FindIndex(self, startIndex, count, match)) != -1;
        }

        public static bool TryFindLastIndex<T>(this T[] self, Predicate<T> match, out int index)
        {
            return (index = Array.FindLastIndex(self, match)) != -1;
        }

        public static bool TryFindLastIndex<T>(this T[] self, int startIndex, Predicate<T> match, out int index)
        {
            return (index = Array.FindLastIndex(self, startIndex, match)) != -1;
        }

        public static bool TryFindLastIndex<T>(this T[] self, int startIndex, int count, Predicate<T> match, out int index)
        {
            return (index = Array.FindLastIndex(self, startIndex, count, match)) != -1;
        }

        public static void Sort<T>(this T[] self)
        {
            Array.Sort(self);
        }

        public static void Sort<T>(this T[] self, Comparison<T> comparison)
        {
            Array.Sort(self, comparison);
        }

        public static void Sort<T>(this T[] self, IComparer<T> comparer)
        {
            Array.Sort(self, comparer);
        }

        public static void Sort<T>(this T[] self, int index, int length)
        {
            Array.Sort(self, index, length);
        }

        public static void Sort<T>(this T[] self, int index, int length, IComparer<T> comparer)
        {
            Array.Sort(self, index, length, comparer);
        }

        public static void Swap<T>(this T[] self, int a, int b)
        {
            var t = self[a];
            self[a] = self[b];
            self[b] = t;
        }

        public static T[] Populate<T>(this T[] self, T value, int count)
            where T : struct
        {
            for (int i = 0; i < count; ++i)
                self[i] = value;
            return self;
        }

        public static T[] Populate<T>(this T[] self, T value)
            where T : struct
        {
            return self.Populate(value, self.Length);
        }

        public static T[] Populate<T>(this T[] self, Func<T> provider, int count)
            where T : class
        {
            for (int i = 0; i < count; ++i)
                self[i] = provider();
            return self;
        }

        public static T[] Populate<T>(this T[] self, Func<T> provider)
            where T : class
        {
            return self.Populate(provider, self.Length);
        }

        public static T[] Copy<T>(this T[] self)
        {
            var result = new T[self.Length];
            self.CopyTo(result);
            return result;
        }

        public static void CopyTo<T>(this T[] self, T[] target)
        {
            self.CopyTo(target, 0);
        }
        
        public static int GetWidth<T>(this T[][] self)
        {
            return self.Length;
        }

        public static int GetHeight<T>(this T[][] self)
        {
            return self.First().Length;
        }

        public static int GetDepth<T>(this T[][][] self)
        {
            return self.First().First().Length;
        }

        public static int GetWidth<T>(this T[,] self)
        {
            return self.GetLength(0);
        }

        public static int GetHeight<T>(this T[,] self)
        {
            return self.GetLength(1);
        }

        public static int GetWidth<T>(this T[,,] self)
        {
            return self.GetLength(0);
        }

        public static int GetHeight<T>(this T[,,] self)
        {
            return self.GetLength(1);
        }

        public static int GetDepth<T>(this T[,,] self)
        {
            return self.GetLength(2);
        }

        public static IEnumerable<T> SafeEnumerator<T>(this T[] self)
        {
            if (self != null)
            {
                foreach (var item in self)
                {
                    if (item != null)
                    {
                        yield return item;
                    }
                }
            }
        }
    }
}
