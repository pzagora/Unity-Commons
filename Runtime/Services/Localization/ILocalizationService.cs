using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Commons.Services
{
    public interface ILocalizationService<TLanguage>
        where TLanguage : Enum
    {
        /// <summary>
        /// Get currently selected language.
        /// </summary>
        TLanguage CurrentLanguage { get; }
        
        /// <summary>
        /// Register a Translation into the service.
        /// </summary>
        Task Register(IEnumerable<Translation> translation);
        
        /// <summary>
        /// Register a ILocalizedText into the service.
        /// </summary>
        void Register(ILocalizedText component);
        
        /// <summary>
        /// Unregister a ILocalizedText from the service.
        /// </summary>
        void Unregister(ILocalizedText component);

        /// <summary>
        /// Change current language.
        /// </summary>
        Task ChangeLanguage(TLanguage language);

        /// <summary>
        /// Check if translation for given key exists in any language
        /// </summary>
        bool HasLocalized(string key);
        
        /// <summary>
        /// Get translated text for a key in current language.
        /// </summary>
        string GetLocalized(string key);

        /// <summary>
        /// Get translated text for a key in specific language.
        /// </summary>
        string GetLocalized(string key, TLanguage language);
    }
}