using HarmonyLib;
using System;
using TB_CameraTweaks.KsHelperLib.Logger;
using TB_CameraTweaks.KsHelperLib.UI.Elements.Slider;
using TB_CameraTweaks.KsHelperLib.UI.Menu;
using TimberApi.DependencyContainerSystem;
using TimberApi.UiBuilderSystem.ElementSystem;
using Timberborn.CameraSystem;
using Timberborn.Common;
using Timberborn.Localization;
using Timberborn.SingletonSystem;

namespace TB_CameraTweaks.Patchers
{
    internal class CameraZoomPatcher : ILoadableSingleton
    {
        private static SliderOption ZoomFactor;
        private static ILoc Loc;
        private static LogProxy Log = new("[Camera_ZoomPatcher] ");

        public CameraZoomPatcher()
        {
            UIRegister.AddUiElements += MenuElements;
        }

        private static void SetupConfig()
        {
            var cfg = new SliderConfig(
                key: "Zoom Factor",
                description: "Camera Zoom Factor (vanilla: 2.5)",
                min: 2.5f, max: 7f, def: 5f,
                labelText: Loc.T($"{Plugin._tocTag}.menu.zoomfactor")
            );
            cfg.Step = 0.5f;
            ZoomFactor = new SliderOption(cfg);

            CameraZoomPatch.Setup(); // Create instance

            //_zoomFactorCfg = Plugin.Config.Bind(MyPluginInfo.PLUGIN_NAME, "Zoom Factor", zoomFactor_def,
            //new ConfigDescription("Camera Zoom Factor (default: 1.3)", new AcceptableValueRange<float>(zoomFactor_min, zoomFactor_max)));
        }

        private static void MenuElements(VisualElementBuilder obj)
        {
            ZoomFactor.Build(obj);
        }

        public void Load()
        {
            Loc = DependencyContainer.GetInstance<ILoc>();
            SetupConfig();
        }

        [HarmonyPatch(typeof(CameraComponent), nameof(CameraComponent.ModifyZoomLevel))]
        private class CameraZoomPatch
        {
            private FloatLimits ModifiedZoomFactor;
            private static bool RequireUpdate = true;
            public static CameraZoomPatch Instance;

            internal static void Setup()
            {
                CameraZoomPatch patch = new CameraZoomPatch();
                Instance = patch;

                ZoomFactor.Config.Updated += Instance.ValueChanged;
                Instance.UpdateZoomFactor();
            }

            private void ValueChanged(object sender, EventArgs e)
            {
                UpdateZoomFactor();
            }

            private void UpdateZoomFactor()
            {
                ModifiedZoomFactor = new Timberborn.Common.FloatLimits(-2.5f, ZoomFactor.Config.Value);
                RequireUpdate = true;
            }

            public static void Postfix(CameraComponent __instance)
            {
                if (RequireUpdate)
                {
                    __instance._defaultZoomLimits = Instance.ModifiedZoomFactor;
                    RequireUpdate = false;
                    //Log.LogWarning($"Patch Executed. Value: {Instance.ModifiedZoomFactor.Max}");
                }
            }
        }

        //[HarmonyPatch(typeof(CameraComponent), nameof(CameraComponent.RestoreState))]
        //private class CameraZoomPatch2
        //{
        //    public static void Postfix(CameraComponent __instance)
        //    {
        //        Plugin.Log.LogWarning("Patch executed: RestoreState");
        //    }
        //}
    }
}