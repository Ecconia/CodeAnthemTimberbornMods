using TB_CameraTweaker.KsHelperLib.Localization;
using TB_CameraTweaker.KsHelperLib.UI.Base;
using TB_CameraTweaker.KsHelperLib.UI.ConfigBoundElements;
using TB_CameraTweaker.Models;
using TB_CameraTweaker.Patches;

namespace TB_CameraTweaker.UI.Position_Manager
{
    internal class CameraPositionUI_Freeze : UIMenuConfigOnly<bool>
    {
        private readonly CameraGetPositionPatcher _cameraGetPositionPatcher;
        private readonly CameraSetPositionPatcher _cameraSetPositionPatcher;

        public CameraPositionUI_Freeze(CameraGetPositionPatcher cameraGetPositionPatcher, CameraSetPositionPatcher cameraSetPositionPatcher) {
            _cameraGetPositionPatcher = cameraGetPositionPatcher;
            _cameraSetPositionPatcher = cameraSetPositionPatcher;
        }

        protected override void OnValueChanged(bool newValue) {
            if (newValue) {
                //Plugin.Log.LogDebug("Freeze Camera");
                var currentPosition = new CameraPositionInfo("FreezePosition", _cameraGetPositionPatcher.GetCurrentPosition());
                _cameraSetPositionPatcher.Freeze(currentPosition);
            } else {
                //Plugin.Log.LogDebug("Unfreeze Camera");
                _cameraSetPositionPatcher.Unfreeze();
            }
        }

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