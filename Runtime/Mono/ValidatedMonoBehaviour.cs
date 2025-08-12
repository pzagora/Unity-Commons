using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using Commons.Attributes;
using Commons.Constants;

namespace Commons.Mono
{
    public abstract class ValidatedMonoBehaviour : MonoBehaviour
    {
#if UNITY_EDITOR
        private const BindingFlags FLAGS 
            = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static;
        
        private void Start()
        {
            ValidateRequiredReferences();
        }

        public bool HasAllRequiredReferences(out List<string> missingReferenceExceptions)
        {
            missingReferenceExceptions = null;
            
            var type = GetType();
            var fieldInfos = type.GetFields(FLAGS).ToList();
            if (!fieldInfos.Any()) 
                return true;
            
            missingReferenceExceptions = (from fieldInfo in fieldInfos
                let requiredAttribute = fieldInfo.GetCustomAttributes(typeof(Required), false).FirstOrDefault()
                let serializeFieldAttribute = fieldInfo.GetCustomAttributes(typeof(SerializeField), false).FirstOrDefault()
                where requiredAttribute != null
                where fieldInfo.IsPublic || serializeFieldAttribute != null
                where fieldInfo.GetValue(this) == null || (fieldInfo.GetType().IsClass && fieldInfo.GetValue(this) is Object o && o == null)
                select string.Format(Msg.VMB_FIELD_NULL, this, fieldInfo.Name)).ToList();

            if (!missingReferenceExceptions.Any()) 
                return true;

            return false;
        }
        
        private void ValidateRequiredReferences()
        {
            if (HasAllRequiredReferences(out var missingReferenceExceptions))
                return;
            
            missingReferenceExceptions.ForEach(mre => Debug.LogError(mre, this));
        }
#endif
    }
}
