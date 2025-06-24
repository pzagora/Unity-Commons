using System;

namespace Unity.Commons
{
    public static class Safe
    {
        public static void Call(Func<bool> condition, Action action)
        {
            if (condition()) action();
        }
        
        public static T Get<T>(Func<bool> condition, Func<T> getter, T defaultValue = default)
            => condition() ? getter() : defaultValue;

        public static void Set<T>(Func<bool> condition, Action<T> setter, T value)
        {
            if (condition()) setter(value);
        }

        public static void Set<T1, T2>(Func<bool> condition, Action<T1, T2> setter, T1 arg1, T2 arg2)
        {
            if (condition()) setter(arg1, arg2);
        }
    }
}