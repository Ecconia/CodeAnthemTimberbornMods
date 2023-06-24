using HarmonyLib;
using TB_CameraTweaker.KsHelperLib.BaseHelpers;
using TB_CameraTweaker.Models;
using TimberApi.DependencyContainerSystem;
using Timberborn.CameraSystem;

namespace TB_CameraTweaker.Patches
{
    [HarmonyPatch(typeof(CameraComponent), nameof(CameraComponent.LateUpdate))]
    internal class CameraSetPositionPatcher : GenericValue<CameraPositionInfo>
    {
        private static CameraSetPositionPatcher Instance => _instance ??= DependencyContainer.GetInstance<CameraSetPositionPatcher>();
        private static CameraSetPositionPatcher _instance;

        private CameraPositionInfo? _freezePosition;

        public void SetJumpPosition(CameraPositionInfo camPos) {
            if (camPos == null) return;
            ChangeValue(camPos);
            IsDirty = true;
        }

        public void Freeze(CameraPositionInfo camPos) {
            if (camPos == null) return;
            _freezePosition = camPos;
        }

        public void Unfreeze() => _freezePosition = null;

        public static void Prefix(CameraComponent __instance) => Instance.PostfixPatch(__instance);

        private void PostfixPatch(CameraComponent instance) {
            if (_freezePosition != null) {
                instance.RestoreState(_freezePosition.CameraState);
                // do we need to reset fov too?
                return;
            }

            if (IsDirty) {
                instance.RestoreState(NewValue.CameraState);
                IsDirty = false;
            }
        }
    }
}