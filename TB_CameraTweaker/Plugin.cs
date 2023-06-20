using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using System;
using System.Collections.Generic;
using TB_CameraTweaker.CameraPositionSaveSystem;
using TB_CameraTweaker.KsHelperLib.Localization;
using TB_CameraTweaker.KsHelperLib.Logger;

namespace TB_CameraTweaker
{
    [BepInPlugin("Kumare." + MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
    [HarmonyPatch]
    public class Plugin : BaseUnityPlugin
    {
        internal new static ConfigFile Config;
        internal static LogProxy Log;
        internal static CameraPositionCore _cameraPositionSaveSystem;
        private static Harmony _harmony;

        public Plugin() {
            SetupLogger();
            Config = base.Config;
            Config.SaveOnConfigSet = true;
            SetupLOC();
            _cameraPositionSaveSystem = new CameraPositionCore();
            _harmony = new Harmony(MyPluginInfo.PLUGIN_GUID);
        }

        private void Awake() {
            _harmony.PatchAll();
            Log.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is loaded!");
        }

        private void SetupLogger() {
            Log = new("[Core] ");
            LogProxy.Level = BepInEx.Logging.LogLevel.Warning;
            LogProxy._logger = Logger;
#if (DEBUG)
            LogProxy.Level = BepInEx.Logging.LogLevel.All;
#endif
        }

        private void SetupLOC() {
            LocConfig.AddAdditionalLanguage("deDE");
            LocConfig.Header = new List<string>()
            {
                $"{MyPluginInfo.PLUGIN_NAME}, Updated: {DateTime.Now}",
                ""
            };
        }

        //public void Entry(IMod mod, IConsoleWriter consoleWriter)
        //{
        //    LogProxy._logger = this.Logger;
        //    Config = base.Config;
        //    Config.SaveOnConfigSet = true;

        // SetupLOC();

        // _harmony = new Harmony(_pluginId); _harmony.PatchAll();

        //    Log.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is loaded!");
        //}
    }
}