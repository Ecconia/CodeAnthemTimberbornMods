using HarmonyLib;
using TB_CameraTweaker.CameraSaveSystem;
using TimberApi.UiBuilderSystem.ElementSystem;
using Timberborn.CameraSystem;
using UnityEngine;

namespace TB_CameraTweaker.Patchers
{
    [HarmonyPatch(typeof(CameraComponent), nameof(CameraComponent.LateUpdate))]
    internal class CameraPositionDataPatcher : PatcherBase<CameraPositionInfo>
    {
        public static CameraPositionDataPatcher Instance { get => _instance; }
        public CameraPositionInfo CurrentPosition { get => _currentPosition; }
        private static CameraPositionDataPatcher _instance;
        private CameraPositionInfo _currentPosition = new CameraPositionInfo("current", Vector3.zero, 0f, 0f, 0f, 0f);

        public CameraPositionDataPatcher() : base("CameraComponent - " + "FOV") { _instance = this; }

        public static void Postfix(CameraComponent __instance) => _instance?.PostfixPatch(__instance);

        public static void Prefix(CameraComponent __instance) => _instance?.PrefixPatch(__instance);

        public void SetTargetPosition(CameraPositionInfo targetPosition) {
            if (targetPosition == null) return;
            if (targetPosition == _currentPosition) return;
            NewValue = targetPosition;
            IsDirty = true;
        }

        public override void UseConfigValue() {
            return;
        }

        protected override void AddUIElements(VisualElementBuilder builder) {
            return;
        }

        protected override void SetupPatcher() {
            return;
        }

        private void PostfixPatch(CameraComponent instance) {
            if (IsDirty) {
                instance.Target = NewValue.Target;
                instance.ZoomLevel = NewValue.ZoomLevel;
                instance.HorizontalAngle = NewValue.HorizontalAngle;
                instance.VerticalAngle = NewValue.VerticalAngle;
                instance.FieldOfView = NewValue.Fov;
                IsDirty = false;
            }
        }

        private void PrefixPatch(CameraComponent instance) {
            if (instance == null) return;

            _currentPosition.Target = instance.Target;
            _currentPosition.ZoomLevel = instance.ZoomLevel;
            _currentPosition.HorizontalAngle = instance.HorizontalAngle;
            _currentPosition.VerticalAngle = instance.VerticalAngle;
            _currentPosition.Fov = instance.FieldOfView;
        }
    }
}