using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RoboBackup {
    /// <summary>
    /// Localization class providing translations and language-related methods for the GUI part.
    /// </summary>
    public static class Lang {
        /// <summary>
        /// Collection of available languages, mainly used in <see cref="SettingsForm"/>. Key is the language string (e.g. "English"), Value is the ISO 639-1 code of the language (e.g. "en").
        /// </summary>
        public static Dictionary<string, string> AvailableLocales {
            get {
                if (_availableLocales == null) {
                    _availableLocales = GetAvailableLocales();
                }
                return _availableLocales;
            }
        }

        /// <summary>
        /// Private store for <see cref="AvailableLocales"/> property.
        /// </summary>
        private static Dictionary<string, string> _availableLocales;

        /// <summary>
        /// Private store for translation strings for the currently set <see cref="Config.Language"/> language.
        /// </summary>
        private static Dictionary<string, string> _translations = new Dictionary<string, string>();

        /// <summary>
        /// Translates given placeholder string. Optionally appends culturally neutral string, e.g. ":" (colon).
        /// </summary>
        /// <param name="key">Placeholder <see cref="string"/> to look up in translations.</param>
        /// <param name="append">Optional <see cref="string"/> to append to the translated string.</param>
        /// <returns>A string translated into the currently set language.</returns>
        public static string Get(string key, string append = null) {
            if (_translations.ContainsKey(key)) {
                string translation = _translations[key];
                if (append != null) {
                    return string.Format("{0}{1}", translation, append);
                }
                return translation;
            }
            return key;
        }

        /// <summary>
        /// Loads corresponding translation string from <see cref="Config.Language"/> language under <see cref="Config.LangRoot"/> directory.
        /// </summary>
        public static void SetLang() {
            string langFile = Path.Combine(Config.LangRoot, string.Format("{0}.txt", Config.Language));
            if (!File.Exists(langFile)) {
                return;
            }
            // Read the lang file, trim whitespaces from lines, skip empty lines, and split the rest to key=value dictionary
            _translations = File.ReadAllLines(langFile).Select(line => line.Trim()).Where(line => !string.IsNullOrEmpty(line)).Select(line => line.Split(new char[] { '=' }, 2)).ToDictionary(line => line[0], line => line[1]);
        }

        /// <summary>
        /// Scans "lang" directory and compiles a collection of languages and translation files present in it.
        /// </summary>
        /// <returns>Collection of available languages, mainly used in <see cref="SettingsForm"/>. Key is the language string (e.g. "English"), Value is the ISO 639-1 code of the language (e.g. "en").</returns>
        private static Dictionary<string, string> GetAvailableLocales() {
            Dictionary<string, string> locales = new Dictionary<string, string>();
            foreach (string file in Directory.EnumerateFiles(Config.LangRoot)) {
                try {
                    // Get the value of line from lang file which starts with '='
                    string langName = File.ReadAllLines(file).First(line => line.StartsWith("=")).Trim().Substring(1);
                    string langCode = Path.GetFileNameWithoutExtension(file);
                    locales.Add(langName, langCode);
                } catch { /* Ignore if the file doesn't contain what we expect */ }
            }
            return locales;
        }
    }
}
