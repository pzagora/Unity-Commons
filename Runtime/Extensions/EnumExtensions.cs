using System;
using System.Collections.Concurrent;
using System.Linq;
using Commons.Attributes;

namespace Commons.Extensions
{
    public static class EnumExtensions
    {
        private static readonly ConcurrentDictionary<Enum, string> LocalizationCache 
            = new ConcurrentDictionary<Enum, string>();
        
        public static string GetLocalizationKey(this Enum error)
        {
            return LocalizationCache.GetOrAdd(error, e =>
            {
                var type = e.GetType();

                var memberInfo = type
                    .GetMember(e.ToString())
                    .FirstOrDefault();

                var attr = memberInfo?
                    .GetCustomAttributes(typeof(LocalizedKeyAttribute), false)
                    .FirstOrDefault() as LocalizedKeyAttribute;

                return attr != null
                    ? attr.Key
                    : (type.FullName ?? type.Name).Replace('+', '.');
            });
        }
    }
}