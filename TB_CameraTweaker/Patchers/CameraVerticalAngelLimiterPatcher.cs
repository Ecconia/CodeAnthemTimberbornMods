using HarmonyLib;
using System;
using TB_CameraTweaker.KsHelperLib.Localization;
using TB_CameraTweaker.KsHelperLib.Logger;
using TB_CameraTweaker.KsHelperLib.UI.Elements.CheckBox;
using TB_CameraTweaker.KsHelperLib.UI.Menu;
using TimberApi.DependencyContainerSystem;
using TimberApi.UiBuilderSystem.ElementSystem;
using Timberborn.CameraSystem;
using Timberborn.Common;
using Timberborn.Localization;
using Timberborn.SingletonSystem;

namespace TB_CameraTweaker.Patchers
{
    internal class CameraVerticalAngelLimiterPatcher : ILoadableSingleton
    {
        private static CheckBoxOption VerticalAngelLimiter;
        private static ILoc Loc;
        private static LogProxy Log = new("[Camera_VerticalAngelLimiter] ");

        public CameraVerticalAngelLimiterPatcher()
        {
            UIRegister.AddUiElements += MenuElements;
        }

        public void Load()
        {
            Loc = DependencyContainer.GetInstance<ILoc>();
            SetupConfig();
            CameraVerticalAngelLimiterPatch.Setup(); // Create instance
        }

        private static void SetupConfig()
        {
            var cfg = new CheckBoxConfig(
                key: "Vertical Angel Limiter Factor",
                description: "Disable Vertical Angel Limiter",
                def: false,
                labelText: Loc.T(key: $"{LocConfig.LocTag}.menu.VerticalLimiter"),
                footerText: $"{Loc.T($"{LocConfig.LocTag}.single.original")}: {Loc.T($"{LocConfig.LocTag}.single.off")}"
            );
            VerticalAngelLimiter = new CheckBoxOption(cfg);
        }

        private static void MenuElements(VisualElementBuilder obj)
        {
            VerticalAngelLimiter.Build(obj);
        }

        [HarmonyPatch(typeof(CameraVerticalAngleLimiter), nameof(CameraComponent.LateUpdate))]
        private class CameraVerticalAngelLimiterPatch
        {
            public static CameraVerticalAngelLimiterPatch Instance;
            private static bool RequireUpdate = true;
            private FloatLimits ModifiedZoomFactor;

            public static void Prefix(CameraVerticalAngleLimiter __instance)
            {
                if (RequireUpdate)
                {
                    __instance._minVerticalAngle = VerticalAngelLimiter.Config.Value ? 10f : 55f;
                    RequireUpdate = false;
                    //Log.LogWarning($"Patch Executed. Value: {Instance.ModifiedZoomFactor.Max}");
                }
            }

            internal static void Setup()
            {
                CameraVerticalAngelLimiterPatch patch = new CameraVerticalAngelLimiterPatch();
                Instance = patch;

                VerticalAngelLimiter.Config.Updated += Instance.ValueChanged;
                RequireUpdate = true;
            }

            private void ValueChanged(object sender, EventArgs e)
            {
                RequireUpdate = true;
            }
        }
    }
}