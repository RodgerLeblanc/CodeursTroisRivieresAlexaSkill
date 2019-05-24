using CellNinja.Localization;
using System;

namespace CodeursTroisRivieresAlexaSkill
{
    public static class Translate
    {
        public static ITranslateResource resource;

        public static string DefaultLocale { get; set; } = "fr-CA";

        public static string Get(string id)
        {
            return Get(id, DefaultLocale);
        }

        public static string Get(string id, string locale)
        {
            if (resource == null)
            {
                resource = DependencyInjection.Get<ITranslateResource>() ??
                    throw new InvalidOperationException($"Could not find a registered instance for {nameof(ITranslateResource)}.");
            }

            return resource.GetStringValue(id, locale);
        }
    }
}
