using TB_CameraTweaker.KsHelperLib.Localization;
using TB_CameraTweaker.KsHelperLib.UI.Base;
using TB_CameraTweaker.KsHelperLib.UI.ConfigBoundElements;
using TB_CameraTweaker.Patches;

namespace TB_CameraTweaker.UI.Tweaks
{
    internal class CameraTweakerUI_FOV : UIMenuPatcherConfigElement<float>
    {
        public CameraTweakerUI_FOV(CameraFOVPatcher patcher) : base(patcher) { }

        public override void Load() {
            _uiPriorityOrder = 1;
            base.Load();
            UseConfigValue();
        }

        protected override IConfigUIElement<float> GenerateConfigEntry() {
            SliderConfigOptions cfg = new(
                key: "FOV",
                description: "Camera FOV (vanilla: 30)",
                min: 30f, max: 90f, def: 55f,
                labelText: _loc.T($"{LocConfig.LocTag}.menu.fov"),
                step: 1.0f,
                footerText: $"{_loc.T($"{LocConfig.LocTag}.single.original")}: 30"
            );
            return new SliderConfigUIElement(cfg);
        }
    }
}