using HarmonyLib;
using TB_CameraTweaker.KsHelperLib.Patches;
using TimberApi.DependencyContainerSystem;
using Timberborn.CameraSystem;

namespace TB_CameraTweaker.Patches
{
    [HarmonyPatch(typeof(CameraVerticalAngleLimiter), nameof(CameraComponent.LateUpdate))]
    internal class CameraVerticalAngleLimiterPatcher : PatcherGenericValue<bool>
    {
        private static CameraVerticalAngleLimiterPatcher Instance => _instance ??= DependencyContainer.GetInstance<CameraVerticalAngleLimiterPatcher>();

        private static CameraVerticalAngleLimiterPatcher _instance;

        public static void Prefix(CameraVerticalAngleLimiter __instance) => Instance.PrefixPatch(__instance);

        private void PrefixPatch(CameraVerticalAngleLimiter instance) {
            if (IsDirty) {
                instance._minVerticalAngle = NewValue ? 10f : 55f;
                IsDirty = false;
            }
        }
    }
}