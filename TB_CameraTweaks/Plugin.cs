using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using System;
using System.Collections.Generic;
using TB_CameraTweaks.KsHelperLib.Localization;
using TB_CameraTweaks.KsHelperLib.Logger;

namespace TB_CameraTweaks
{
    [BepInPlugin(_pluginId, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
    [HarmonyPatch]
    public class Plugin : BaseUnityPlugin
    {
        public const string _pluginId = "Kumare." + MyPluginInfo.PLUGIN_NAME;

        internal new static ConfigFile Config;
        internal static string _tocTag = $"{MyPluginInfo.PLUGIN_NAME.ToLower()}";
        internal static LogProxy Log;
        private static Harmony _harmony;

        public Plugin()
        {
            SetupLogger();
            Config = base.Config;
            Config.SaveOnConfigSet = true;
            SetupTOC();
            _harmony = new Harmony(_pluginId);
        }

        private void Awake()
        {
            _harmony.PatchAll();
            Log.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is loaded!");
        }

        private void SetupLogger()
        {
            Log = new("[Core] ");
            LogProxy.Level = BepInEx.Logging.LogLevel.Warning;
            LogProxy._logger = this.Logger;
#if (DEBUG)
            LogProxy.Level = BepInEx.Logging.LogLevel.All;
#endif
        }

        private void SetupTOC()
        {
            TocConfig.AddAdditionalLanguage("deDE");
            TocConfig.Header = new List<string>()
            {
                $"{MyPluginInfo.PLUGIN_NAME}, Updated: {DateTime.Now}",
                "============================================"
            };
        }

        //public void Entry(IMod mod, IConsoleWriter consoleWriter)
        //{
        //    LogProxy._logger = this.Logger;
        //    Config = base.Config;
        //    Config.SaveOnConfigSet = true;

        // SetupTOC();

        // _harmony = new Harmony(_pluginId); _harmony.PatchAll();

        //    Log.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is loaded!");
        //}
    }
}