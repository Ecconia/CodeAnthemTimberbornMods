using HarmonyLib;
using TB_CameraTweaker.KsHelperLib.BaseHelpers;
using TimberApi.DependencyContainerSystem;
using Timberborn.CameraSystem;

namespace TB_CameraTweaker.Patches
{
    [HarmonyPatch(typeof(CameraComponent), nameof(CameraComponent.LateUpdate))]
    internal class CameraFreeModePatcher : GenericValue<bool>
    {
        private static CameraFreeModePatcher Instance => _instance ??= DependencyContainer.GetInstance<CameraFreeModePatcher>();

        private static CameraFreeModePatcher _instance;

        public static void Postfix(CameraComponent __instance) => Instance.PostfixPatch(__instance);

        private void PostfixPatch(CameraComponent instance) {
            if (IsDirty) {
                instance.FreeMode = NewValue;
                Plugin.Log.LogDebug("CameraFreeModePatcher() - " + NewValue);
                IsDirty = false;
            }
        }
    }
}