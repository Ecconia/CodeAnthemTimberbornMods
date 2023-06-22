using HarmonyLib;
using Timberborn.SingletonSystem;

namespace TB_CameraTweaker.KsHelperLib.Patches
{
    internal class PatchLoader : ILoadableSingleton
    {
        private static bool isPatched = false;
        private static readonly Harmony _harmony = new Harmony(MyPluginInfo.PLUGIN_GUID);
        public Harmony Harmony { get => _harmony; }

        public void Load() {
            DoPatchAll();
        }

        private void DoPatchAll() {
            if (isPatched) return;
            _harmony.PatchAll();
            Plugin.Log.LogDebug($"Patches: Loaded");
            isPatched = true;
        }
    }
}