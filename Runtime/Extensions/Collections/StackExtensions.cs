using System.Collections.Generic;

namespace Common
{
    public static class StackExtensions
    {
        public static bool IsEmpty<T>(this Stack<T> self)
        {
            return self.Count == 0;
        }

        public static bool IsNullOrEmpty<T>(this Stack<T> self)
        {
            return self == null || self.IsEmpty();
        }

        public static bool Contains<T>(this Stack<T> self, IEnumerable<T> items)
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

        public static bool TryPop<T>(this Stack<T> self, out T item)
        {
            if (self.Count > 0)
            {
                item = self.Pop();
                return true;
            }
            item = default;
            return false;
        }
    }
}
