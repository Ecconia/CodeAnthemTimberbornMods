using HarmonyLib;
using System;
using TB_CameraTweaks.KsHelperLib.Logger;
using TB_CameraTweaks.KsHelperLib.UI.Elements.Labels;
using TB_CameraTweaks.KsHelperLib.UI.Elements.Slider;
using TB_CameraTweaks.KsHelperLib.UI.Menu;
using TimberApi.DependencyContainerSystem;
using TimberApi.UiBuilderSystem.ElementSystem;
using Timberborn.CameraSystem;
using Timberborn.Common;
using Timberborn.Localization;
using Timberborn.SingletonSystem;
using UnityEngine.UIElements;

namespace TB_CameraTweaks.Patchers
{
    internal class CameraFOVPatcher : ILoadableSingleton
    {
        private static SliderOption FOV;
        private static ILoc Loc;
        private static LogProxy Log = new("[Camera_FOV] ");

        public CameraFOVPatcher()
        {
            UIRegister.AddUiElements += MenuElements;
        }

        private static void SetupConfig()
        {
            var cfg = new SliderConfig(
                key: "FOV",
                description: "Camera FOV (vanilla: 30)",
                min: 30f, max: 90f, def: 55f,
                labelText: Loc.T($"{Plugin._tocTag}.menu.fov")
            );
            cfg.Step = 1.0f;
            FOV = new SliderOption(cfg);
        }

        private static void MenuElements(VisualElementBuilder obj)
        {
            FOV.Build(obj);
            StaticLabels.FooterNote(obj, $"{Loc.T($"{Plugin._tocTag}.single.default")}: 30");
        }

        public void Load()
        {
            Loc = DependencyContainer.GetInstance<ILoc>();
            SetupConfig();
            CameraFOVPatch.Setup(); // Create instance
        }

        [HarmonyPatch(typeof(CameraComponent), nameof(CameraComponent.ModifyZoomLevel))]
        private class CameraFOVPatch
        {
            private static bool RequireUpdate = true;
            public static CameraFOVPatch Instance;

            internal static void Setup()
            {
                CameraFOVPatch patch = new CameraFOVPatch();
                Instance = patch;

                FOV.Config.Updated += Instance.ValueChanged;
                RequireUpdate = true;
            }

            private void ValueChanged(object sender, EventArgs e)
            {
                RequireUpdate = true;
            }

            public static void Postfix(CameraComponent __instance)
            {
                if (RequireUpdate)
                {
                    __instance.FieldOfView = FOV.Config.Value;
                    RequireUpdate = false;
                    //Log.LogWarning($"Patch Executed. Value: {Instance.ModifiedZoomFactor.Max}");
                }
            }
        }
    }
}