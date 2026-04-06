using System.Collections.Generic;

namespace Common
{
    public static class QueueExtensions
    {
        public static bool IsEmpty<T>(this Queue<T> self)
        {
            return self.Count == 0;
        }

        public static bool IsNullOrEmpty<T>(this Queue<T> self)
        {
            return self == null || self.IsEmpty();
        }

        public static bool Contains<T>(this Queue<T> self, IEnumerable<T> items)
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

        public static bool TryDequeue<T>(this Queue<T> self, out T item)
        {
            if (self.Count > 0)
            {
                item = self.Dequeue();
                return true;
            }
            item = default;
            return false;
        }
    }
}
