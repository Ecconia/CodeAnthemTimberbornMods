using System;
using System.Collections.Generic;

namespace TB_CameraTweaker.KsHelperLib.Localization
{
    internal static class LocConfig
    {
        public static List<string> Header { get; set; } = new List<string>() { };
        public static string LocTag { get; set; } = $"{MyPluginInfo.PLUGIN_NAME.ToLower()}";
        public static string DefaultLanguage { get; set; } = "enUS";
        private static readonly List<string> AdditionalLanguages = new();

        public static void AddAdditionalLanguage(string langTag)
        {
            if (string.IsNullOrEmpty(langTag))
            {
                throw new ArgumentException($"LocConfig, couldn't add additional language: {langTag}", nameof(langTag));
            }
            AdditionalLanguages.Add(langTag);
        }

        public static IEnumerable<string> GetLanguages() => AdditionalLanguages;
    }
}