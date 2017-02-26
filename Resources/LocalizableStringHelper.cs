using System.Globalization;
using System.Resources;

namespace AppResources
{
    public static class LocalizableStringHelper
    {
        private static readonly ResourceManager ResourceManager = new ResourceManager("AppResources.Language", typeof(AppResources.Lang).Assembly);
        private static CultureInfo _cultureInfo = CultureInfo.CreateSpecificCulture("es");
        
        public static void SetCulture(string culture)
        {
            _cultureInfo = CultureInfo.CreateSpecificCulture(culture);
        }

        public static string GetLocalizableString(string localizableStringName)
        {
            return ResourceManager.GetString(localizableStringName, _cultureInfo);
        }
    }
}