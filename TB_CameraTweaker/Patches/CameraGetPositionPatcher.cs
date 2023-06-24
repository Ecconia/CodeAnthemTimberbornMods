using HarmonyLib;
using System;
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

        public Action OnNewCameraPosition;

        private readonly CameraPositionInfo _currentPosition = new("current", Vector3.zero, 0f, 0f, 0f, 0f);

        public static void Prefix(CameraComponent __instance) => Instance.PrefixPatch(__instance);

        public CameraPositionInfo GetCurrentPosition() => _currentPosition;

        private void PrefixPatch(CameraComponent instance) {
            if (instance == null) return;
            if (DoesPositionMatch(instance)) return;

            _currentPosition.CameraState = instance.GetCurrentState();
            _currentPosition.Fov = instance.FieldOfView;
            OnNewCameraPosition?.Invoke();
        }

        private bool DoesPositionMatch(CameraComponent instance) {
            bool doesMatch = true;
            if (_currentPosition.Target != instance.Target) doesMatch = false;
            if (_currentPosition.ZoomLevel != instance.ZoomLevel) doesMatch = false;
            if (_currentPosition.HorizontalAngle != instance.HorizontalAngle) doesMatch = false;
            if (_currentPosition.VerticalAngle != instance.VerticalAngle) doesMatch = false;
            if (_currentPosition.Fov != instance.FieldOfView) doesMatch = false;
            return doesMatch;
        }
    }
}