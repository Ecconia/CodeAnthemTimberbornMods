using HarmonyLib;
using TB_CameraTweaker.KsHelperLib.Localization;
using TB_CameraTweaker.KsHelperLib.UI.Elements.Slider;
using TimberApi.UiBuilderSystem.ElementSystem;
using Timberborn.CameraSystem;
using Timberborn.Common;

namespace TB_CameraTweaker.Patchers
{
    [HarmonyPatch(typeof(CameraComponent), nameof(CameraComponent.ModifyZoomLevel))]
    internal class CameraZoomPatcher : PatcherBase<float>
    {
        private static CameraZoomPatcher _instance;
        private SliderElement _sliderZoomFactor;

        public CameraZoomPatcher() : base("CameraComponent - " + "Zoom Factor") { _instance = this; }

        public static void Postfix(CameraComponent __instance) => _instance?.PostfixPatch(__instance);

        public override void UseConfigValue() => ChangeValue(_sliderZoomFactor.Config.Value);

        protected override void AddUIElements(VisualElementBuilder builder) {
            _sliderZoomFactor.Build(builder, true);
        }

        protected override void SetupPatcher() {
            SetupConfig();
            _sliderZoomFactor.Config.Updated += () => UseConfigValue();
        }

        private void PostfixPatch(CameraComponent instance) {
            if (IsDirty) {
                FloatLimits modifiedZoomFactor = new FloatLimits(-2.5f, NewValue);
                instance._defaultZoomLimits = modifiedZoomFactor;
                instance._relaxedZoomLimits = modifiedZoomFactor;
                IsDirty = false;
            }
        }

        private void SetupConfig() {
            var cfg = new SliderConfig(
                key: "Zoom Factor",
                description: "Camera Zoom Factor (vanilla: 2.5)",
                min: 2.5f, max: 7f, def: 3f,
                labelText: _loc.T($"{LocConfig.LocTag}.menu.zoomfactor"),
                step: 0.5f,
                footerText: $"{_loc.T($"{LocConfig.LocTag}.single.original")}: 2.5"
            );
            _sliderZoomFactor = new SliderElement(cfg);
        }
    }
}