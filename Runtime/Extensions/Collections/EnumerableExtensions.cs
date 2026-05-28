using System.Collections.Generic;

namespace Commons
{
    public static class EnumerableExtensions
    {
        public static bool HasNext<T>(this IEnumerable<T> self)
        {
            using var e = self.GetEnumerator();
            return e.MoveNext();
        }
    }
}