using System.Collections.Generic;
using System.Linq;

namespace Commons.PlayerPrefs
{
    public abstract class ATargetedPlayerPrefs
    {
        protected static void SetString(string key, string value)
            => UnityEngine.PlayerPrefs.SetString(key, value);
        
        protected static void SetFloat(string key, float value)
            => UnityEngine.PlayerPrefs.SetFloat(key, value);
        
        protected static void SetInt(string key, int value)
            => UnityEngine.PlayerPrefs.SetInt(key, value);
        
        protected static string GetString(string key)
            => UnityEngine.PlayerPrefs.GetString(key);
        
        protected static float GetFloat(string key)
            => UnityEngine.PlayerPrefs.GetFloat(key);
        
        protected static int GetInt(string key)
            => UnityEngine.PlayerPrefs.GetInt(key);

        protected static void DeleteKeys(List<string> keys)
            => keys.ForEach(DeleteKey);
        
        protected static void DeleteKey(string key)
            => UnityEngine.PlayerPrefs.DeleteKey(key);
        
        protected static bool HasKeys(List<string> keys)
            => keys.All(HasKey);
        
        protected static bool HasKey(string key)
            => UnityEngine.PlayerPrefs.HasKey(key);

        protected static void Save()
            => UnityEngine.PlayerPrefs.Save();
    }
}