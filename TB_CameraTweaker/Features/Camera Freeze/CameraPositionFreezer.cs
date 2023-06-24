using System;
using TB_CameraTweaker.KsHelperLib.BaseHelpers;
using TB_CameraTweaker.Models;
using TB_CameraTweaker.Patches;

namespace TB_CameraTweaker.Features.Camera_Freeze
{
    internal class CameraPositionFreezer : BoolToggle
    {
        private readonly CameraGetPositionPatcher _cameraGetPositionPatcher;
        private readonly CameraSetPositionPatcher _cameraSetPositionPatcher;

        public CameraPositionFreezer(CameraGetPositionPatcher cameraGetPositionPatcher, CameraSetPositionPatcher cameraSetPositionPatcher) {
            _cameraGetPositionPatcher = cameraGetPositionPatcher ?? throw new ArgumentNullException(nameof(CameraGetPositionPatcher));
            _cameraSetPositionPatcher = cameraSetPositionPatcher ?? throw new ArgumentNullException(nameof(CameraSetPositionPatcher));
        }

        protected override void ValueChanged() {
            if (CurrentState) {
                var currentPosition = new CameraPositionInfo("FreezePosition", _cameraGetPositionPatcher.GetCurrentPosition());
                _cameraSetPositionPatcher.Freeze(currentPosition);
                Plugin.Log.LogDebug("[CameraPositionFreezer] - Freeze Camera");
                return;
            }
            Plugin.Log.LogDebug("[CameraPositionFreezer] - Unfreeze Camera");
            _cameraSetPositionPatcher.Unfreeze();
        }
    }
}