// Revange of the dwarves - Test Header
using HarmonyLib;
using System;
using TB_CameraTweaker.KsHelperLib.Localization;
using TB_CameraTweaker.KsHelperLib.Logger;
using TB_CameraTweaker.KsHelperLib.UI.Elements.Slider;
using TB_CameraTweaker.KsHelperLib.UI.Menu;
using TimberApi.DependencyContainerSystem;
using TimberApi.UiBuilderSystem.ElementSystem;
using Timberborn.CameraSystem;
using Timberborn.Common;
using Timberborn.Localization;
using Timberborn.SingletonSystem;

namespace TB_CameraTweaker.Patchers
{
    internal class CameraZoomPatcher : ILoadableSingleton
    {
        private static SliderOption ZoomFactor;
        private static ILoc Loc;
        private static LogProxy Log = new("[Camera_ZoomPatcher] ");

        public CameraZoomPatcher() {
            UIRegister.AddUiElements += MenuElements;
        }

        private static void SetupConfig() {
            var cfg = new SliderConfig(
                key: "Zoom Factor",
                description: "Camera Zoom Factor (vanilla: 2.5)",
                min: 2.5f, max: 7f, def: 3f,
                labelText: Loc.T($"{LocConfig.LocTag}.menu.zoomfactor"),
                footerText: $"{Loc.T($"{LocConfig.LocTag}.single.original")}: 2.5"
            );
            cfg.Step = 0.5f;
            ZoomFactor = new SliderOption(cfg);
        }

        private static void MenuElements(VisualElementBuilder obj) {
            ZoomFactor.Build(obj, true);
        }

        public void Load() {
            Loc = DependencyContainer.GetInstance<ILoc>();
            SetupConfig();
            CameraZoomPatch.Setup(); // Create instance
        }

        [HarmonyPatch(typeof(CameraComponent), nameof(CameraComponent.ModifyZoomLevel))]
        private class CameraZoomPatch
        {
            private FloatLimits ModifiedZoomFactor;
            private static bool RequireUpdate = true;
            public static CameraZoomPatch Instance;

            internal static void Setup() {
                CameraZoomPatch patch = new CameraZoomPatch();
                Instance = patch;

                ZoomFactor.Config.Updated += Instance.ValueChanged;
                Instance.UpdateZoomFactor();
            }

            private void ValueChanged(object sender, EventArgs e) {
                UpdateZoomFactor();
            }

            private void UpdateZoomFactor() {
                ModifiedZoomFactor = new FloatLimits(-2.5f, ZoomFactor.Config.Value);
                RequireUpdate = true;
            }

            public static void Postfix(CameraComponent __instance) {
                if (RequireUpdate) {
                    __instance._defaultZoomLimits = Instance.ModifiedZoomFactor;
                    __instance._relaxedZoomLimits = Instance.ModifiedZoomFactor;
                    RequireUpdate = false;
                    //Log.LogWarning($"Patch Executed. Value: {Instance.ModifiedZoomFactor.Max}");
                }
            }
        }
    }
}