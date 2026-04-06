using System;
using System.Collections;
using System.Collections.Generic;

namespace Common
{
    public static class ListExtensions
    {
        public static bool IsNullOrEmpty<T>(this List<T> input)
        {
            return input.GetCountSafely() == 0;
        }

        public static bool NotNullNorEmpty<T>(this List<T> input)
        {
            return !input.IsNullOrEmpty();
        }
        
        public static int GetCountSafely<T>(this List<T> self)
        {
            return self == null ? 0 : self.Count;
        }

        public static T GetAt<T>(this List<T> self, int index)
        {
            return self[index];
        }

        public static T First<T>(this List<T> self)
        {
            return self[0];
        }

        public static T FirstOrDefault<T>(this List<T> self, T value = default)
        {
            return self.IsEmpty() ? value : self.First();
        }

        public static T Last<T>(this List<T> self)
        {
            return self[self.Count - 1];
        }

        public static T LastOrDefault<T>(this List<T> self, T value = default)
        {
            return self.IsEmpty() ? value : self.Last();
        }

        public static List<TOut> ConvertAll<TIn, TOut>(this List<TIn> self, Converter<TIn, TOut> converter)
        {
            var result = new List<TOut>(self.Count);
            self.ConvertAll(converter, result);
            return result;
        }

        public static List<TOut> ConvertAll<TIn, TOut>(this List<TIn> self, Converter<TIn, TOut> converter, List<TOut> target)
        {
            target.Clear();
            for (int i = 0; i < self.Count; ++i)
            {
                var item = converter(self[i]);
                target.Add(item);
            }
            return target;
        }

        public static bool HasAnyOfType<T>(this IList self)
        {
            if (self != null)
            {
                foreach (var item in self)
                {
                    if (item is T)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static T FindAnyOfType<T>(this IList self)
        {
            if (self != null)
            {
                foreach (var item in self)
                {
                    if (item is T casted)
                    {
                        return casted;
                    }
                }
            }
            return default;
        }

        public static IEnumerable<T> FindAllOfType<T>(this IList self)
        {
            if (self != null)
            {
                foreach (var item in self)
                {
                    if (item is T casted)
                    {
                        yield return casted;
                    }
                }
            }
        }

        public static bool TryGetAt<T>(this List<T> self, int index, out T item)
        {
            if (-1 < index && index < self.Count)
            {
                item = self[index];
                return true;
            }
            item = default;
            return false;
        }

        public static bool TryGetFirst<T>(this List<T> self, out T item)
        {
            return self.TryGetAt(0, out item);
        }

        public static bool TryGetLast<T>(this List<T> self, out T item)
        {
            return self.TryGetAt(self.Count - 1, out item);
        }

        public static bool TryIndexOf<T>(this List<T> self, T item, out int index)
        {
            return (index = self.IndexOf(item)) != -1;
        }

        public static bool TryIndexOf<T>(this List<T> self, T item, int startIndex, out int index)
        {
            return (index = self.IndexOf(item, startIndex)) != -1;
        }

        public static bool TryIndexOf<T>(this List<T> self, T item, int startIndex, int count, out int index)
        {
            return (index = self.IndexOf(item, startIndex, count)) != -1;
        }

        public static bool TryFind<T>(this List<T> self, Predicate<T> match, out T value)
        {
            return !Equals(value = self.Find(match), default);
        }

        public static bool TryFindLast<T>(this List<T> self, Predicate<T> match, out T value)
        {
            return !Equals(value = self.FindLast(match), default);
        }

        public static bool TryFindAll<T>(this List<T> self, Predicate<T> match, out List<T> value)
        {
            return !(value = self.FindAll(match)).IsNullOrEmpty();
        }

        public static bool TryFindIndex<T>(this List<T> self, Predicate<T> match, out int index)
        {
            return (index = self.FindIndex(match)) != -1;
        }

        public static bool TryFindIndex<T>(this List<T> self, int startIndex, Predicate<T> match, out int index)
        {
            return (index = self.FindIndex(startIndex, match)) != -1;
        }

        public static bool TryFindIndex<T>(this List<T> self, int startIndex, int count, Predicate<T> match, out int index)
        {
            return (index = self.FindIndex(startIndex, count, match)) != -1;
        }

        public static bool TryFindLastIndex<T>(this List<T> self, Predicate<T> match, out int index)
        {
            return (index = self.FindLastIndex(match)) != -1;
        }

        public static bool TryFindLastIndex<T>(this List<T> self, int startIndex, Predicate<T> match, out int index)
        {
            return (index = self.FindLastIndex(startIndex, match)) != -1;
        }

        public static bool TryFindLastIndex<T>(this List<T> self, int startIndex, int count, Predicate<T> match, out int index)
        {
            return (index = self.FindLastIndex(startIndex, count, match)) != -1;
        }

        public static void Swap<T>(this IList<T> self, int a, int b)
        {
            (self[a], self[b]) = (self[b], self[a]);
        }

        public static void SwapLast<T>(this List<T> self, int index)
        {
            self.Swap(index, self.Count - 1);
        }

        public static List<T> Populate<T>(this List<T> self, T value, int count)
            where T : struct
        {
            for (int i = 0; i < count; ++i)
            {
                self.Add(value);
            }
            return self;
        }

        public static List<T> Populate<T>(this List<T> self, T value)
            where T : struct
        {
            return self.Populate(value, self.Capacity);
        }

        public static List<T> Populate<T>(this List<T> self, Func<T> provider, int count)
            where T : class
        {
            for (int i = 0; i < count; ++i)
            {
                self.Add(provider());
            }
            return self;
        }

        public static List<T> Populate<T>(this List<T> self, Func<T> provider)
            where T : class
        {
            return self.Populate(provider, self.Capacity);
        }

        public static List<T> Populate<T>(this List<T> self, int count)
        {
            for (int i = 0; i < count; ++i)
            {
                self.Add(default);
            }
            return self;
        }

        public static List<T> Populate<T>(this List<T> self)
        {
            return self.Populate(self.Capacity);
        }

        public static void AddRange<T>(this List<T> self, params T[] items)
        {
            self.AddRange(items);
        }

        public static bool AddUnique<T>(this List<T> self, T item)
        {
            if (!self.Contains(item))
            {
                self.Add(item);
                return true;
            }
            return false;
        }

        public static void InsertFirst<T>(this List<T> self, T item)
        {
            self.Insert(0, item);
        }

        public static int InsertSorted<T>(this List<T> self, T item, IComparer<T> comparer = null)
        {
            int index = self.BinarySearch(item, comparer ?? Comparer<T>.Default);
            if (index < 0)
                index = ~index;
            self.Insert(index, item);
            return index;
        }

        public static int InsertSorted<T>(this List<T> self, T item, Comparison<T> comparison)
        {
            return self.InsertSorted(item, Comparer<T>.Create(comparison));
        }

        public static bool InsertUnique<T>(this List<T> self, int index, T item)
        {
            if (!self.Contains(item))
            {
                self.Insert(index, item);
                return true;
            }
            return false;
        }

        public static bool RemoveRange<T>(this List<T> self, IEnumerable<T> items)
        {
            var result = true;
            foreach (var item in items)
            {
                result &= self.Remove(item);
            }
            return result;
        }

        public static void RemoveFirst<T>(this List<T> self)
        {
            self.RemoveAt(0);
        }

        public static void RemoveFirst<T>(this List<T> self, int count)
        {
            self.RemoveRange(0, count);
        }

        public static bool RemoveFirst<T>(this List<T> self, Predicate<T> match)
        {
            for (int i = 0; i < self.Count; ++i)
            {
                if (match(self[i]))
                {
                    self.RemoveAt(i);
                    return true;
                }
            }
            return false;
        }

        public static void RemoveLast<T>(this List<T> self)
        {
            self.RemoveAt(self.Count - 1);
        }

        public static void RemoveLast<T>(this List<T> self, int count)
        {
            self.RemoveRange(self.Count - 1 - count, count);
        }

        public static bool RemoveLast<T>(this List<T> self, Predicate<T> match)
        {
            for (int i = self.Count - 1; i > -1; --i)
            {
                var item = self[i];
                if (match(item))
                {
                    self.RemoveAt(i);
                    return true;
                }
            }
            return false;
        }

        public static bool RemoveAll<T>(this List<T> self, Predicate<T> match)
        {
            var result = false;
            for (int i = self.Count - 1; i > -1; --i)
            {
                var item = self[i];
                if (match(item))
                {
                    self.RemoveAt(i);
                    result = true;
                }
            }
            return result;
        }

        public static T RevokeAt<T>(this List<T> self, int index)
        {
            var result = self[index];
            self.RemoveAt(index);
            return result;
        }

        public static T RevokeFirst<T>(this List<T> self)
        {
            return self.RevokeAt(0);
        }

        public static T RevokeFirst<T>(this List<T> self, Predicate<T> match)
        {
            for (int i = 0; i < self.Count; ++i)
            {
                var item = self[i];
                if (match(item))
                {
                    self.RemoveAt(i);
                    return item;
                }
            }
            return default;
        }

        public static T RevokeLast<T>(this List<T> self)
        {
            return self.RevokeAt(self.Count - 1);
        }

        public static T RevokeLast<T>(this List<T> self, Predicate<T> match)
        {
            for (int i = self.Count - 1; i > -1; --i)
            {
                var item = self[i];
                if (match(item))
                {
                    self.RemoveAt(i);
                    return item;
                }
            }
            return default;
        }

        public static IEnumerable<T> RevokeAll<T>(this List<T> self, Predicate<T> match)
        {
            for (int i = self.Count - 1; i > -1; --i)
            {
                var item = self[i];
                if (match(item))
                {
                    self.RemoveAt(i);
                    yield return item;
                }
            }
        }

        public static IEnumerable EnumerateSafely(this IList self)
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

        public static IEnumerable<T> EnumerateSafely<T>(this List<T> self)
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
