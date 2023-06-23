using TB_CameraTweaker.KsHelperLib.Localization;
using TB_CameraTweaker.KsHelperLib.UI.Base;
using TB_CameraTweaker.KsHelperLib.UI.ConfigBoundElements;
using TB_CameraTweaker.Patches;

namespace TB_CameraTweaker.UI.Tweaks
{
    internal class CameraTweakerUI_VerticalAngleLimiter : UIMenuPatcherConfigElement<bool>
    {
        public CameraTweakerUI_VerticalAngleLimiter(CameraVerticalAngleLimiterPatcher patcher) : base(patcher) { }

        public override void Load() {
            _uiPriorityOrder = 10;
            base.Load();
            UseConfigValue();
        }

        protected override IConfigUIElement<bool> GenerateConfigEntry() {
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