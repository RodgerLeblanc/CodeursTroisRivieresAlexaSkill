using System;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Resources;

namespace CellNinja.Localization
{
    public class TranslateResource : ITranslateResource
    {
        static TranslateResource()
        {
            //Load resource files
            //https://stackoverflow.com/questions/43645305/using-resx-file-in-azure-function-app/48678685#48678685
            Load("fr");
            Load("en");
        }

        private static Lazy<ResourceManager> _resmgr = null;

        public string GetStringValue(string resourceId, string locale)
        {
            return GetStringValue(resourceId, string.Empty, new CultureInfo(locale));
        }

        private string GetStringValue(string resourceId, string defaultText, CultureInfo cultureInfo)
        {
            if (_resmgr == null)
                FindResourceManager();

            if (resourceId == null)
                return defaultText;

            try
            {
                return _resmgr.Value.GetString(resourceId, cultureInfo) ?? defaultText;
            }
            catch (Exception)
            {
                return defaultText;
            }
        }

        private static void FindResourceManager()
        {
            string ResourceId = "CellNinja.Localization.Resources.Translations";
            _resmgr = new Lazy<ResourceManager>(() => new ResourceManager(ResourceId, typeof(TranslateResource).GetTypeInfo().Assembly));
        }

        private static ResourceSet Load(string lang)
        {
            var asm = Assembly.LoadFrom(Path.Combine(Environment.CurrentDirectory, "bin", lang, "CellNinja.Localization.resources.dll"));
            var resourceName = $"CellNinja.Localization.Resources.Translations.{lang}.resources";
            var tt = asm.GetManifestResourceNames();
            return new ResourceSet(asm.GetManifestResourceStream(resourceName));
        }
    }
}
