using TB_CameraTweaker.KsHelperLib.Localization;
using TB_CameraTweaker.KsHelperLib.UI.Elements;
using TB_CameraTweaker.KsHelperLib.UI.Elements.CheckBox;
using TB_CameraTweaker.Models;
using TB_CameraTweaker.Patches;
using TimberApi.DependencyContainerSystem;

namespace TB_CameraTweaker.Features.Camera_Tweaker.UI
{
    internal class CameraTweakerUI_Freeze : CameraTweakerUIBase<bool>
    {
        public CameraTweakerUI_Freeze() : base(null) { }

        protected override void OnValueChanged(bool newValue) {
            if (newValue) { FreezedPosition(); } else { UnFreezePosition(); }
        }

        private void FreezedPosition() {
            Plugin.Log.LogDebug("Freeze Camera");
            CameraPositionInfo currentPosition = new("FreezePosition", DependencyContainer.GetInstance<CameraGetPositionPatcher>().GetCurrentPosition());
            DependencyContainer.GetInstance<CameraSetPositionPatcher>().Freeze(currentPosition);
        }

        private void UnFreezePosition() {
            Plugin.Log.LogDebug("Unfreeze Camera");
            DependencyContainer.GetInstance<CameraSetPositionPatcher>().Unfreeze();
        }

        protected override IConfigUIElement<bool> GenerateUIElement() {
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