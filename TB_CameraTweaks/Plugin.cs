using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using System.Collections.Generic;
using TB_CameraTweaks.Lang;
using TB_CameraTweaks.MyLogger;
using TimberApi.ConsoleSystem;
using TimberApi.ModSystem;

namespace TB_CameraTweaks
{
    [BepInPlugin(_pluginId, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
    [HarmonyPatch]
    public class Plugin : BaseUnityPlugin
    {
        public const string _pluginId = "Kumare." + MyPluginInfo.PLUGIN_NAME;

        internal new static ConfigFile Config;
        internal static string _tocTag = $"{MyPluginInfo.PLUGIN_NAME.ToLower()}";
        internal static LogProxy Log = new("[Core] ");
        private static Harmony _harmony;

        //public void Entry(IMod mod, IConsoleWriter consoleWriter)
        //{
        //    LogProxy._logger = this.Logger;
        //    Config = base.Config;
        //    Config.SaveOnConfigSet = true;

        // SetupTOC();

        // _harmony = new Harmony(_pluginId); _harmony.PatchAll();

        //    Log.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is loaded!");
        //}

        private void Awake()
        {
            LogProxy._logger = this.Logger;
            Config = base.Config;
            Config.SaveOnConfigSet = true;

            SetupTOC();

            _harmony = new Harmony(_pluginId);
            _harmony.PatchAll();
            Log.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is loaded!");
        }

        private void SetupTOC()
        {
            List<string> languages = new List<string>() { "deDE", "enUS" };
            List<string> entries = new List<string>()
            {
                "menu.title", // default, used as main menu title on UIPatches.cs
                "menu.options",  // default, used as option header title on menu
                "menu.zoomfactor"
            };
            TocManager tocFac = new TocManager(languages, entries);
            tocFac.CheckFiles();
        }

        private void InitConfig()
        {
            // Plugin.Config.Bind(MyPluginInfo.PLUGIN_NAME, "Zoom Factor", 1.3f, new
            // ConfigDescription("Camera Zoom Factor (default: 1.3)", new
            // AcceptableValueRange<float>(1.3f, 2.0f)));

            //            Plugin.Config.Bind(MyPluginInfo.PLUGIN_NAME, "Disable Snap Camera", false,
            //new ConfigDescription("Disable Camera Snap (default: false)"));
        }
    }
}