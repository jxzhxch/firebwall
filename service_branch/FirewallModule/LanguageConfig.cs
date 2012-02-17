using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace FM
{
    public static class LanguageConfig
    {
        /// <summary>
        /// Language enums
        /// </summary>
        public enum Language
        {
            ENGLISH,
            SPANISH,
            GERMAN,
            CHINESE,
            RUSSIAN,
            NONE
        }

        static Language cLanguage = Language.NONE;

        /// <summary>
        /// Change the current language
        /// </summary>
        /// <param name="l"></param>
        public static void SetLanguage(Language l)
        {
            cLanguage = l;
        }

        /// <summary>
        /// Returns the current language, or sets it if it hasn't been.
        /// </summary>
        /// <returns></returns>
        public static Language GetCurrentLanguage()
        {
            if (cLanguage == Language.NONE)
            {
                switch (CultureInfo.CurrentCulture.TwoLetterISOLanguageName)
                {
                    case "en":
                        cLanguage = Language.ENGLISH;
                        break;
                    case "es":
                        cLanguage = Language.SPANISH;
                        break;
                    case "de":
                        cLanguage = Language.GERMAN;
                        break;
                    case "zh":
                        cLanguage = Language.CHINESE;
                        break;
                    case "ru":
                        cLanguage = Language.RUSSIAN;
                        break;
                }
            }
            return cLanguage;
        }

    }
}
