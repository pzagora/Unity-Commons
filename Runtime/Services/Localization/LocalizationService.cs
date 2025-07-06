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
                        this.ReportWarning($"[{nameof(LocalizationService<TLanguage>)}] Unknown language code: {val.lang}");
                    }
                }
                
                _translations[translation.key] = map;
                
                await Task.Yield();
            }
            
            this.ReportEvent($"Registered {_translations.Keys.Count} translations");
            await BatchUpdate();
        }

        public async Task ChangeLanguage(TLanguage language)
        {
            if (Equals(CurrentLanguage, language))
                return;

            this.ReportEvent($"Language changed: {CurrentLanguage} -> {language}");
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
            
            if (_translations.TryGetValue(key, out var langMap) && 
                langMap.TryGetValue(language, out var text))
            {
                return text;
            }

            this.ReportWarning($"Missing translation ({key}) in {language}");
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
