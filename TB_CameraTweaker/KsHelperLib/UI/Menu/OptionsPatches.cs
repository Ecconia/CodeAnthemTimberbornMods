using HarmonyLib;
using TB_CameraTweaker.KsHelperLib.Localization;
using TimberApi.DependencyContainerSystem;
using Timberborn.Localization;
using Timberborn.MainMenuScene;
using Timberborn.Options;
using UnityEngine.UIElements;

namespace TB_CameraTweaker.KsHelperLib.UI.Menu
{
    internal class OptionsPatches
    {
        [HarmonyPatch(typeof(OptionsBox), "GetPanel")]
        public static class InGameMenuPanelPatch
        {
            public static void Postfix(ref VisualElement __result) {
                Plugin.Log.LogDebug("Menu Patch: InGame");
                VisualElement root = __result.Query("OptionsBox");
                Button button = new() {
                    classList = { "menu-button" },
                    text = DependencyContainer.GetInstance<ILoc>().T($"{LocConfig.LocTag}.menu.title")
                };
                button.clicked += OptionsMenu.OpenOptionsDelegate;
                root.Insert(6, button);
            }
        }

        /// <summary>
        /// Patch to show Status Icon Options on Main Menu
        /// </summary>
        [HarmonyPatch(typeof(MainMenuPanel), "GetPanel")]
        public static class MainMenuPanelPatch
        {
            public static void Postfix(ref VisualElement __result) {
                Plugin.Log.LogDebug("Menu Patch: MainMenu");
                VisualElement root = __result.Query("MainMenuPanel");
                Button button = new() {
                    classList = { "menu-button" },
                    text = DependencyContainer.GetInstance<ILoc>().T($"{LocConfig.LocTag}.menu.title")
                };
                button.clicked += OptionsMenu.OpenOptionsDelegate;
                root.Insert(6, button);
            }
        }
    }
}