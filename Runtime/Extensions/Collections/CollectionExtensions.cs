using System.Collections.Generic;

namespace Common
{
    public static class CollectionExtensions
    {
        public static bool IsEmpty<T>(this ICollection<T> self)
        {
            return self.Count == 0;
        }

        public static bool IsNullOrEmpty<T>(this ICollection<T> self)
        {
            return self == null || self.IsEmpty();
        }

        public static bool ContainsAll<T>(this ICollection<T> self, params T[] items)
        {
            for (int i = 0; i < items.Length; ++i)
            {
                if (!self.Contains(items[i]))
                {
                    return false;
                }
            }
            return true;
        }

        public static bool ContainsAny<T>(this ICollection<T> self, params T[] items)
        {
            for (int i = 0; i < items.Length; ++i)
            {
                if (self.Contains(items[i]))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool ContainsAll<T>(this ICollection<T> self, IEnumerable<T> items)
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

        public static bool ContainsAny<T>(this ICollection<T> self, IEnumerable<T> items)
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

        public static bool RemoveAll<T>(this ICollection<T> self, params T[] items)
        {
            var result = true;
            for (int i = 0; i < items.Length; ++i)
            {
                result &= self.Remove(items[i]);
            }
            return result;
        }

        public static bool RemoveAll<T>(this ICollection<T> self, IEnumerable<T> items)
        {
            var result = true;
            foreach (var item in items)
            {
                result &= self.Remove(item);
            }
            return result;
        }
    }
}