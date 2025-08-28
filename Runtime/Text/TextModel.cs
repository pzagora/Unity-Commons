using System;
using System.Collections.Generic;
using System.Linq;

namespace Commons
{
    public class TextModel : BaseObservable
    {
        private string _key;
        private object[] _args = Array.Empty<object>();

        public TextModel() : this(string.Empty) { }
        public TextModel(string key)
        {
            Key = key;
        }
        
        public string Key
        {
            get => _key; 
            set => UpdateValue(ref _key, value);
        }

        public object[] Args
        {
            get => _args; 
            set => UpdateValue(ref _args, GetNormalizedArgs(value));
        }
        
        public TextModel Full(string key, params object[] args)
        {
            using (Batch())
            {
                WithKey(key);
                WithArgs(args);
            }
            return this;
        }
        
        public TextModel WithKey(string key)
        {
            Key = key;
            return this;
        }
        
        public TextModel WithArgs(params object[] args)
        {
            Args = args ?? Array.Empty<object>();
            return this;
        }

        private static object[] GetNormalizedArgs(IReadOnlyCollection<object> src)
        {
            if (src == null || src.Count == 0) 
                return Array.Empty<object>();
            
            return src
                .Select(v => v?.ToString() ?? string.Empty)
                .Cast<object>()
                .ToArray();
        }
    }
}