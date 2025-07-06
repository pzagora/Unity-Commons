using System;
using UnityEngine;

namespace Commons.Services
{
    public class LanguagePersistence<TLanguage> where TLanguage : Enum
    {
        private readonly ILanguageMapper<TLanguage> _mapper;
        private readonly string _prefsKey;

        public LanguagePersistence(ILanguageMapper<TLanguage> mapper)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _prefsKey = $"{nameof(LanguagePersistence<TLanguage>)}_{typeof(TLanguage).Name}";
        }

        public TLanguage LoadOrDetect()
        {
            if (PlayerPrefs.HasKey(_prefsKey))
            {
                var savedCode = PlayerPrefs.GetString(_prefsKey);
                if (_mapper.TryGetLanguage(savedCode, out var savedLang))
                {
                    return savedLang;
                }
            }

            return Save(_mapper.MapSystemLanguage(Application.systemLanguage));
        }

        public TLanguage Save(TLanguage language)
        {
            PlayerPrefs.SetString(_prefsKey, _mapper.GetCodeForLanguage(language));
            PlayerPrefs.Save();
            return language;
        }
    }
}