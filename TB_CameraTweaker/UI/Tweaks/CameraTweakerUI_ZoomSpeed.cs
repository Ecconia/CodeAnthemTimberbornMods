using TB_CameraTweaker.KsHelperLib.Localization;
using TB_CameraTweaker.KsHelperLib.UI.Base;
using TB_CameraTweaker.KsHelperLib.UI.ConfigBoundElements;
using TB_CameraTweaker.Patches;

namespace TB_CameraTweaker.UI.Tweaks
{
    internal class CameraTweakerUI_ZoomSpeed : UIMenuPatcherConfigElement<float>
    {
        public CameraTweakerUI_ZoomSpeed(CameraZoomSpeedPatcher patcher) : base(patcher) { }

        public override void Load() {
            _uiPriorityOrder = 4;
            base.Load();
            UseConfigValue();
        }

        protected override IConfigUIElement<float> GenerateConfigEntry() {
            SliderConfigOptions cfg = new(
                key: "ZoomSpeed",
                description: "Camera Zoom Speed (vanilla: 1.7)",
                min: 1.7f, max: 6f, def: 1.7f,
                labelText: _loc.T($"{LocConfig.LocTag}.menu.zoomspeed"),
                step: 0.1f,
                footerText: $"{_loc.T($"{LocConfig.LocTag}.single.original")}: 1.7"
            );
            return new SliderConfigUIElement(cfg);
        }
    }
}