using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Commons.Services
{
    public class LocalizationService<TLanguage> : ILocalizationService<TLanguage>
        where TLanguage : Enum
    {
        public TLanguage CurrentLanguage { get; private set; }
        
        private readonly ILanguageMapper<TLanguage> _mapper;
        private readonly LanguagePersistence<TLanguage> _languagePersistence;
        private readonly HashSet<ILocalizedText> _registeredTexts = new();
        private readonly Dictionary<string, Dictionary<TLanguage, string>> _translations = new();

        private const int TEXT_UPDATE_BATCH_SIZE = 20;
        
        private const string LOG_UNKNOWN_LANGUAGE = "Unknown language code: {0}";
        private const string LOG_REGISTERED_TRANSLATIONS = "Registered {0} translations";
        private const string LOG_LANGUAGE_CHANGED = "Language changed: {0} -> {1}";
        private const string LOG_MISSING_TRANSLATION = "Missing translation <{0}> in {1}";
        
        public LocalizationService(ILanguageMapper<TLanguage> mapper)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _languagePersistence = new LanguagePersistence<TLanguage>(_mapper);

            CurrentLanguage = _languagePersistence.LoadOrDetect();
        }

        public void Register(ILocalizedText component) 
            => _registeredTexts.Add(component);
        public void Unregister(ILocalizedText component) 
            => _registeredTexts.Remove(component);

        public async Task Register(IEnumerable<Translation> translations)
        {
            _translations.Clear();
            
            foreach (var translation in translations)
            {
                var map = new Dictionary<TLanguage, string>();
                
                foreach (var val in translation.values)
                {
                    if (_mapper.TryGetLanguage(val.lang, out var lang))
                    {
                        map[lang] = val.text;
                    }
                    else
                    {
                        Report.Warning<LocalizationService<TLanguage>>(string.Format(LOG_UNKNOWN_LANGUAGE, val.lang));
                    }
                }
                
                _translations[translation.Id] = map;
                
                await Task.Yield();
            }
            
            Report.Event<LocalizationService<TLanguage>>(string.Format(LOG_REGISTERED_TRANSLATIONS, _translations.Keys.Count));
            await BatchUpdate();
        }

        public async Task ChangeLanguage(TLanguage language)
        {
            if (Equals(CurrentLanguage, language))
                return;

            Report.Event<LocalizationService<TLanguage>>(string.Format(LOG_LANGUAGE_CHANGED, CurrentLanguage, language));
            CurrentLanguage = language;

            _languagePersistence.Save(CurrentLanguage);

            await BatchUpdate();
        }

        public bool HasLocalized(string key) 
            => !string.IsNullOrWhiteSpace(key) && _translations.ContainsKey(key.ToLowerInvariant());

        public string GetLocalized(string key)
            => GetLocalized(key, CurrentLanguage);

        public string GetLocalized(string key, TLanguage language)
        {
            if (string.IsNullOrWhiteSpace(key))
                return string.Empty;
            
            key = key.ToLowerInvariant();

            var hasMap = _translations.TryGetValue(key, out var langMap);
            if (hasMap && langMap.TryGetValue(language, out var text))
            {
                return text;
            }
            
            Report.Warning<LocalizationService<TLanguage>>(string.Format(LOG_MISSING_TRANSLATION, key, language));
            return key;
        }

        private async Task BatchUpdate()
        {
            var count = 0;
            foreach (var text in _registeredTexts)
            {
                text.UpdateText();
                count++;

                if (count % TEXT_UPDATE_BATCH_SIZE == 0)
                {
                    await Task.Yield();
                }
            }
        }
    }
}
