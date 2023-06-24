using HarmonyLib;
using TB_CameraTweaker.KsHelperLib.BaseHelpers;
using TimberApi.DependencyContainerSystem;
using Timberborn.CameraSystem;

namespace TB_CameraTweaker.Patches
{
    [HarmonyPatch(typeof(CameraComponent), nameof(CameraComponent.LateUpdate))]
    internal class CameraZoomSpeedPatcher : GenericValue<float>
    {
        private static CameraZoomSpeedPatcher Instance => _instance ??= DependencyContainer.GetInstance<CameraZoomSpeedPatcher>();

        private static CameraZoomSpeedPatcher _instance;

        public static void Postfix(CameraComponent __instance) => Instance.PostfixPatch(__instance);

        private void PostfixPatch(CameraComponent instance) {
            if (IsDirty) {
                instance._zoomSpeed = NewValue;
                Plugin.Log.LogDebug("CameraZoomSpeedPatcher() - " + NewValue);
                IsDirty = false;
            }
        }
    }
}