using TB_CameraTweaker.Features.Camera_Freeze;
using TB_CameraTweaker.KsHelperLib.Localization;
using TB_CameraTweaker.KsHelperLib.UI.Base;
using TB_CameraTweaker.KsHelperLib.UI.ConfigBoundElements;

namespace TB_CameraTweaker.UI.Position_Manager
{
    internal class CameraPositionUI_Freeze : UIMenuConfigOnly<bool>
    {
        private readonly CameraPositionFreezer _freezer;

        public CameraPositionUI_Freeze(CameraPositionFreezer freezer) {
            _freezer = freezer;
        }

        protected override void OnValueChanged(bool newValue) => _freezer.ChangeValue(newValue);

        public override void Load() {
            _uiPriorityOrder = 101;
            base.Load();
        }

        protected override IConfigUIElement<bool> GenerateConfigEntry() {
            var cfg = new CheckBoxConfig(
                key: "Camera Freeze",
                description: "Freeze Camera (auto. resets to false)",
                def: false,
                labelText: _loc.T($"{LocConfig.LocTag}.menu.freeze"),
                footerText: $"{_loc.T($"{LocConfig.LocTag}.single.original")}: {_loc.T($"{LocConfig.LocTag}.single.off")}"
            );
            cfg.Value = false;
            return new CheckBoxUIElement(cfg);
        }
    }
}