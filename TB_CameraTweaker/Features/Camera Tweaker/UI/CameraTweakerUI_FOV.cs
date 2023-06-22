using TB_CameraTweaker.KsHelperLib.Localization;
using TB_CameraTweaker.KsHelperLib.UI.Elements;
using TB_CameraTweaker.KsHelperLib.UI.Elements.Slider;
using TB_CameraTweaker.Patches;

namespace TB_CameraTweaker.Features.Camera_Tweaker.UI
{
    internal class CameraTweakerUI_FOV : CameraTweakerUIBase<float>
    {
        public CameraTweakerUI_FOV(CameraFOVPatcher patcher) : base(patcher) { }

        public override void Load() {
            base.Load();
            UseConfigValue();
        }

        protected override IConfigUIElement<float> GenerateUIElement() {
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