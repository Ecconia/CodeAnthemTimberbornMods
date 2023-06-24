using HarmonyLib;
using TB_CameraTweaker.KsHelperLib.BaseHelpers;
using TimberApi.DependencyContainerSystem;
using Timberborn.CameraSystem;

namespace TB_CameraTweaker.Patches
{
    [HarmonyPatch(typeof(CameraComponent), nameof(CameraComponent.LateUpdate))]
    internal class CameraZoomLevelPatcher : GenericValue<float>
    {
        private static CameraZoomLevelPatcher Instance => _instance ??= DependencyContainer.GetInstance<CameraZoomLevelPatcher>();

        private static CameraZoomLevelPatcher _instance;

        public static void Postfix(CameraComponent __instance) => Instance.PostfixPatch(__instance);

        private void PostfixPatch(CameraComponent instance) {
            if (IsDirty) {
                instance.ZoomLevel = NewValue;
                Plugin.Log.LogDebug("CameraZoomLevelPatcher() - " + NewValue);
                IsDirty = false;
            }
        }
    }
}