using HarmonyLib;
using TB_CameraTweaker.KsHelperLib.Localization;
using TB_CameraTweaker.KsHelperLib.UI.Elements.Slider;
using TimberApi.UiBuilderSystem.ElementSystem;
using Timberborn.CameraSystem;

namespace TB_CameraTweaker.Patchers
{
    [HarmonyPatch(typeof(CameraComponent), nameof(CameraComponent.ModifyZoomLevel))]
    internal class CameraFOVPatcher : PatcherBase<float>
    {
        private static CameraFOVPatcher _instance;
        private SliderOption _sliderFOV;

        public CameraFOVPatcher() : base("CameraComponent - " + "FOV") { _instance = this; }

        public static void Postfix(CameraComponent __instance) => _instance?.PostfixPatch(__instance);

        public override void UseConfigValue() => ChangeValue(_sliderFOV.Config.Value);

        protected override void AddUIElements(VisualElementBuilder builder) {
            _sliderFOV.Build(builder, true);
        }

        protected override void SetupPatcher() {
            SetupConfig();
            _sliderFOV.Config.Updated += () => UseConfigValue();
        }

        private void PostfixPatch(CameraComponent instance) {
            if (IsDirty) {
                instance.FieldOfView = _instance.NewValue;
                IsDirty = false;
            }
        }

        private void SetupConfig() {
            SliderConfig cfg = new(
                key: "FOV",
                description: "Camera FOV (vanilla: 30)",
                min: 30f, max: 90f, def: 55f,
                labelText: _loc.T($"{LocConfig.LocTag}.menu.fov"),
                step: 1.0f,
                footerText: $"{_loc.T($"{LocConfig.LocTag}.single.original")}: 30"
            );
            _sliderFOV = new SliderOption(cfg);
        }
    }
}