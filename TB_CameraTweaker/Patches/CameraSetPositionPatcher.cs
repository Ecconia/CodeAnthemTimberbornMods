using HarmonyLib;
using TB_CameraTweaker.KsHelperLib.Patches;
using TB_CameraTweaker.Models;
using TimberApi.DependencyContainerSystem;
using Timberborn.CameraSystem;

namespace TB_CameraTweaker.Patches
{
    [HarmonyPatch(typeof(CameraComponent), nameof(CameraComponent.LateUpdate))]
    internal class CameraSetPositionPatcher : PatcherGenericValue<CameraPositionInfo>
    {
        private static CameraSetPositionPatcher Instance => _instance ??= DependencyContainer.GetInstance<CameraSetPositionPatcher>();
        private static CameraSetPositionPatcher _instance;

        private CameraPositionInfo _freezePosition;

        public void SetJumpPosition(CameraPositionInfo camPos) {
            if (camPos == null) return;
            ChangeValue(camPos);
            IsDirty = true;
        }

        public void Freeze(CameraPositionInfo camPos) {
            if (camPos == null) return;
            _freezePosition = camPos;
        }

        public void Unfreeze() {
            _freezePosition = null;
        }

        //public void SetTargetPosition(CameraPositionInfo targetPosition) {
        //    if (targetPosition == null) return;
        //    if (targetPosition == _currentPosition) return;
        //    ChangeValue(targetPosition);
        //    IsDirty = true;
        //}

        public static void Prefix(CameraComponent __instance) => Instance.PostfixPatch(__instance);

        private void PostfixPatch(CameraComponent instance) {
            if (_freezePosition != null) {
                SetPosition(instance, _freezePosition);
                return;
            }

            if (IsDirty) {
                SetPosition(instance, NewValue);
                IsDirty = false;
            }
        }

        private void SetPosition(CameraComponent instance, CameraPositionInfo camPos) {
            instance.RestoreState(camPos.CameraState);
            instance.FieldOfView = camPos.Fov;
        }
    }
}