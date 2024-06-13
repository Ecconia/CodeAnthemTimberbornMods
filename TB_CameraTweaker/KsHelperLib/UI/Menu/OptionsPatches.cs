using HarmonyLib;
using TB_CameraTweaker.KsHelperLib.Localization;
using TimberApi.DependencyContainerSystem;
using TimberApi.UiBuilderSystem.CustomElements;
using Timberborn.Localization;
using Timberborn.MainMenuScene;
using Timberborn.MapEditorUI;
using Timberborn.OptionsGame;
using UnityEngine.UIElements;

namespace TB_CameraTweaker.KsHelperLib.UI.Menu
{
    internal class OptionsPatches
    {
        [HarmonyPatch(typeof(GameOptionsBox), "GetPanel")]
        public static class InGameMenuPanelPatch
        {
            public static void Postfix(ref VisualElement __result) {
                Plugin.Log.LogDebug("Menu Patch: InGame");
                VisualElement root = __result.Query("OptionsBox");
                MenuButtonInjector.InjectMenuButtonAt(root, 6);
            }
        }

        [HarmonyPatch(typeof(MapEditorOptionsBox), "GetPanel")]
        public static class InMapEditorMenuPanelPatch
        {
            public static void Postfix(ref VisualElement __result) {
                Plugin.Log.LogDebug("Menu Patch: MapEditor");
                VisualElement root = __result.Query("OptionsBox");
                MenuButtonInjector.InjectMenuButtonAt(root, 6);
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
                MenuButtonInjector.InjectMenuButtonAt(root, 6);
            }
        }

        private static class MenuButtonInjector
        {
            public static void InjectMenuButtonAt(VisualElement root, int index)
            {
                LocalizableButton button = new() {
                    name = "CameraTweaker",
                    classList = { "menu-button", "menu-button--stretched" },
                    //Automatic localization by key does not work. Manually set the text:
                    text = DependencyContainer.GetInstance<ILoc>().T($"{LocConfig.LocTag}.menu.title"),
                };
                button.clicked += OptionsMenu.OpenOptionsDelegate;
                root.Insert(index, button);
            }
        }
    }
}