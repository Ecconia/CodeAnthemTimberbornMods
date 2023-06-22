using HarmonyLib;
using TB_CameraTweaker.KsHelperLib.Patches;
using TimberApi.DependencyContainerSystem;
using Timberborn.CameraSystem;

namespace TB_CameraTweaker.Patches
{
    [HarmonyPatch(typeof(CameraComponent), nameof(CameraComponent.LateUpdate))]
    internal class CameraZoomLevelPatcher : PatcherGenericValue<float>
    {
        private static CameraZoomLevelPatcher Instance => _instance ??= DependencyContainer.GetInstance<CameraZoomLevelPatcher>();

        private static CameraZoomLevelPatcher _instance;

        public static void Postfix(CameraComponent __instance) => Instance.PostfixPatch(__instance);

        private void PostfixPatch(CameraComponent instance) {
            if (IsDirty) {
                instance.ZoomLevel = NewValue;
                IsDirty = false;
            }
        }
    }
}