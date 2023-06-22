using HarmonyLib;
using TB_CameraTweaker.KsHelperLib.Patches;
using TimberApi.DependencyContainerSystem;
using Timberborn.CameraSystem;
using Timberborn.Common;

namespace TB_CameraTweaker.Patches
{
    [HarmonyPatch(typeof(CameraComponent), nameof(CameraComponent.ModifyZoomLevel))]
    internal class CameraZoomLevelLimitPatcher : PatcherGenericValue<float>
    {
        private static CameraZoomLevelLimitPatcher Instance => _instance ??= DependencyContainer.GetInstance<CameraZoomLevelLimitPatcher>();

        private static CameraZoomLevelLimitPatcher _instance;

        public static void Postfix(CameraComponent __instance) => Instance.PostfixPatch(__instance);

        private void PostfixPatch(CameraComponent instance) {
            if (IsDirty) {
                FloatLimits modifiedZoomFactor = new(-2.5f, NewValue);
                instance._defaultZoomLimits = modifiedZoomFactor;
                instance._relaxedZoomLimits = modifiedZoomFactor;
                IsDirty = false;
            }
        }
    }
}