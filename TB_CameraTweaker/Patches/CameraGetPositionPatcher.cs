using HarmonyLib;
using TB_CameraTweaker.Models;
using TimberApi.DependencyContainerSystem;
using Timberborn.CameraSystem;
using UnityEngine;

namespace TB_CameraTweaker.Patches
{
    /// <summary>
    /// Patch to  get current position of camera
    /// </summary>
    [HarmonyPatch(typeof(CameraComponent), nameof(CameraComponent.LateUpdate))]
    internal class CameraGetPositionPatcher
    {
        private static CameraGetPositionPatcher Instance => _instance ??= DependencyContainer.GetInstance<CameraGetPositionPatcher>();
        private static CameraGetPositionPatcher _instance;

        private readonly CameraPositionInfo _currentPosition = new("current", Vector3.zero, 0f, 0f, 0f, 0f);

        public static void Prefix(CameraComponent __instance) => Instance.PrefixPatch(__instance);

        public CameraPositionInfo GetCurrentPosition() => _currentPosition;

        private void PrefixPatch(CameraComponent instance) {
            if (instance == null) return;
            _currentPosition.CameraState = instance.GetCurrentState();
            _currentPosition.Fov = instance.FieldOfView;
        }
    }
}