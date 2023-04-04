using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace SpaceProgram.Language
{
    internal class LanguageHelper
    {
        private static ResourceManager _rm;
        static LanguageHelper()
        {
            _rm = new ResourceManager("SpaceProgram.Language.output", Assembly.GetExecutingAssembly());
        }
        public static string? GetString(string name)
        {
            return _rm.GetString(name);
        }
        public static void ChangeLanguage(string language)
        {
            var cultureInfo = new CultureInfo(language);
            CultureInfo.CurrentCulture = cultureInfo;
            CultureInfo.CurrentUICulture = cultureInfo;
        }
    }
}
