using TB_CameraTweaker.KsHelperLib.Localization;
using TB_CameraTweaker.KsHelperLib.UI.Elements;
using TB_CameraTweaker.KsHelperLib.UI.Elements.Slider;
using TB_CameraTweaker.Patches;
using TimberApi.DependencyContainerSystem;

namespace TB_CameraTweaker.Features.Camera_Tweaker.UI
{
    internal class CameraTweakerUI_ZoomLevelLimiter : CameraTweakerUIBase<float>
    {
        public CameraTweakerUI_ZoomLevelLimiter(CameraZoomLevelLimitPatcher patcher) : base(patcher) { }

        private CameraZoomLevelPatcher _zoomPatcher;

        public override void Load() {
            base.Load();

            _zoomPatcher = DependencyContainer.GetInstance<CameraZoomLevelPatcher>();
            _uiElement.ValueChanged += (x) => _zoomPatcher.ChangeValue(x); // patch zoom with current setting so user can easly see the effect

            UseConfigValue();
        }

        protected override IConfigUIElement<float> GenerateUIElement() {
            SliderConfigOptions cfg = new(
                key: "Zoom Factor",
                description: "Camera Zoom Factor (vanilla: 2.5)",
                min: 2.5f, max: 7f, def: 3f,
                labelText: _loc.T($"{LocConfig.LocTag}.menu.zoomfactor"),
                step: 0.5f,
                footerText: $"{_loc.T($"{LocConfig.LocTag}.single.original")}: 2.5"
            );
            return new SliderConfigUIElement(cfg);
        }
    }
}