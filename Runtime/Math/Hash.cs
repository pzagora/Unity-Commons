using System.Linq;

namespace Commons
{
    public static class Hash
    {
        public static int Compute(params object[] objects)
        {
            unchecked
            {
                return objects.Aggregate(0, (current, t) => (current * 397) ^ t.GetHashCode());
            }
        }
    }
}
