using TB_CameraTweaker.KsHelperLib.Localization;
using TB_CameraTweaker.KsHelperLib.UI.Elements;
using TB_CameraTweaker.KsHelperLib.UI.Elements.CheckBox;
using TB_CameraTweaker.Patches;

namespace TB_CameraTweaker.Features.Camera_Tweaker.UI
{
    internal class CameraTweakerUI_VerticalAngleLimiter : CameraTweakerUIBase<bool>
    {
        public CameraTweakerUI_VerticalAngleLimiter(CameraVerticalAngleLimiterPatcher patcher) : base(patcher) { }

        public override void Load() {
            base.Load();
            UseConfigValue();
        }

        protected override IConfigUIElement<bool> GenerateUIElement() {
            var cfg = new CheckBoxConfig(
                key: "Vertical Angel Limiter Factor",
                description: "Disable Vertical Angel Limiter",
                def: false,
                labelText: _loc.T(key: $"{LocConfig.LocTag}.menu.VerticalLimiter"),
                footerText: $"{_loc.T($"{LocConfig.LocTag}.single.original")}: {_loc.T($"{LocConfig.LocTag}.single.off")}"
            );
            return new CheckBoxUIElement(cfg);
        }
    }
}