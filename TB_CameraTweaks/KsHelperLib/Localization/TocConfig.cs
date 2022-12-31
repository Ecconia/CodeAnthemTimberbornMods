using System;
using System.Collections.Generic;

namespace TB_CameraTweaks.KsHelperLib.Localization
{
    internal static class TocConfig
    {
        public static List<string> Header { get; set; } = new List<string>() { };
        public static string TocTag { get; set; } = $"{MyPluginInfo.PLUGIN_NAME.ToLower()}";
        public static string DefaultLanguage { get; set; } = "enUS";
        private static List<string> AdditionalLanguages = new List<string>();

        public static void AddAdditionalLanguage(string langTag)
        {
            if (string.IsNullOrEmpty(langTag))
            {
                throw new ArgumentException($"TocConfig, couldn't add additional language: {langTag}", nameof(langTag));
            }
            AdditionalLanguages.Add(langTag);
        }

        public static IEnumerable<string> GetLanguages() => AdditionalLanguages;
    }
}