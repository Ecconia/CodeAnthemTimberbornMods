using System;
using TB_CameraTweaker.CameraPositions.Store;
using TB_CameraTweaker.Features.Camera_Freeze;
using TB_CameraTweaker.KsHelperLib.Logger;
using TB_CameraTweaker.Models;
using TB_CameraTweaker.Patches;
using TB_CameraTweaker.UI.Tweaks;

namespace TB_CameraTweaker.Camera_Position_Manager
{
    internal class CameraPositionManagerCore
    {
        public CameraPositionStore Store { get; private set; }

        public Action ActiveCameraPositionChanged;
        private CameraPositionInfo _activePosition;

        public Action OnCameraMoved;

        public CameraPositionInfo? ActivePosition { get => _activePosition; private set { _activePosition = value; ActiveCameraPositionChanged?.Invoke(); } }
        public bool CameraIsActive { get => ActivePosition != null; }

        private readonly LogProxy _log = new("[Camera Positions: Core] ");
        private readonly CameraSetPositionPatcher _cameraSetPositionPatcher;
        private readonly CameraGetPositionPatcher _cameraGetPositionPatcher;
        private readonly CameraFOVPatcher _cameraFOVPatcher;
        private readonly CameraZoomLevelLimitPatcher _cameraZoomLevelLimitPatcher;
        private readonly CameraZoomLevelPatcher _cameraZoomLevelPatcher;
        private readonly CameraPositionFreezer _cameraPositionFreezer;

        private readonly CameraTweakerUI_FOV _cameraTweakerUI_FOV;
        private readonly CameraTweakerUI_ZoomLevelLimiter _cameraTweakerUI_ZoomLevelLimiter;

        private bool _ignoreReset = false;

        public CameraPositionManagerCore(
            CameraPositionStore cameraPositionStore,
            CameraSetPositionPatcher cameraPositionPatcher,
            CameraGetPositionPatcher cameraGetPositionPatcher,
            CameraFOVPatcher cameraFOVPatcher,
            CameraZoomLevelLimitPatcher cameraZoomLevelLimit,
            CameraPositionFreezer cameraPositionFreezer,
            CameraZoomLevelPatcher cameraZoomLevel,
            CameraTweakerUI_FOV cameraTweakerUI_FOV,
            CameraTweakerUI_ZoomLevelLimiter cameraTweakerUI_ZoomLevelLimiter) {
            Store = cameraPositionStore;
            _cameraSetPositionPatcher = cameraPositionPatcher;
            _cameraGetPositionPatcher = cameraGetPositionPatcher;
            _cameraFOVPatcher = cameraFOVPatcher;
            _cameraZoomLevelLimitPatcher = cameraZoomLevelLimit;
            _cameraPositionFreezer = cameraPositionFreezer;
            _cameraZoomLevelPatcher = cameraZoomLevel;
            _cameraTweakerUI_FOV = cameraTweakerUI_FOV;
            _cameraTweakerUI_ZoomLevelLimiter = cameraTweakerUI_ZoomLevelLimiter;

            SubForCameraChanges(true);
        }

        public void AddCurrentCameraButton(string name) {
            //string cameraName = (string.IsNullOrEmpty(name)) ? "Camera: #" + new System.Random().Next(1, 99) : name;
            string cameraName = (string.IsNullOrEmpty(name)) ? "Camera: #" + (Store.SavedCameraPositions.Count + 1) : name;
            var camera = new CameraPositionInfo(cameraName, _cameraGetPositionPatcher.GetCurrentPosition());
            Store.AddCameraPosition(camera);
            ActivePosition = camera;
        }

        private void SubForCameraChanges(bool doSub) {
            _cameraGetPositionPatcher.OnNewCameraPosition -= CameraWasMoved;
            if (doSub) {
                _cameraGetPositionPatcher.OnNewCameraPosition += CameraWasMoved;
            }
        }

        private void CameraWasMoved() {
            OnCameraMoved?.Invoke();
            if (_ignoreReset) return;
            if (IsCurrentPositionActivePosition()) return;
            ResetBackToConfigValues();
        }

        public void SetNextActive() => ActivePosition = Store.GetNext(ActivePosition);

        public void SetPreviousActive() => ActivePosition = Store.GetPrevious(ActivePosition);

        public void GetFirst() {
            if (Store.SavedCameraPositions.Count > 0) {
                ActivePosition = Store.SavedCameraPositions[0];
                return;
            }
            ActivePosition = null;
        }

        public void RemoveActiveCamera() {
            if (ActivePosition != null) RemoveCamera(ActivePosition);
        }

        internal void RemoveCamera(CameraPositionInfo camera) {
            Store.RemoveCameraPosition(camera);
            GetFirst();
        }

        public bool IsCurrentPositionActivePosition() {
            if (ActivePosition != null) {
                if (ActivePosition.IsSamePosition(_cameraGetPositionPatcher.GetCurrentPosition())) return true;
            }
            return false;
        }

        public void JumpToActivePosition() {
            if (ActivePosition == null) {
                _log.LogDebug("JumpToActivePosition() - null");
                return;
            }
            _ignoreReset = true;
            _cameraFOVPatcher.ChangeValue(ActivePosition.Fov);
            _cameraZoomLevelLimitPatcher.ChangeValue(ActivePosition.ZoomLevel);
            _cameraZoomLevelPatcher.ChangeValue(ActivePosition.ZoomLevel);
            _cameraSetPositionPatcher.SetJumpPosition(ActivePosition);
            _log.LogDebug("JumpToActivePosition() - Jumped to " + ActivePosition.Name);
            _ignoreReset = false;
        }

        public void ResetBackToConfigValues() {
            _cameraTweakerUI_FOV.UseConfigValue();
            var zoomLimit = _cameraTweakerUI_ZoomLevelLimiter.CurrentValue;
            _cameraZoomLevelLimitPatcher.ChangeValue(zoomLimit);
            //_cameraZoomLevelPatcher.ChangeValue(zoomLimit);
            _log.LogDebug("ResetBackToConfigValues() - Reset");
        }

        //private void AddDummyData() {
        //    var testCamera = new CameraPositionInfo(name: "Test position info Name", new Vector3(), 55f, 40f, 30f, 60f);
        //    var testCamera2 = new CameraPositionInfo(name: "Test position info Name 2", new Vector3(), 55f, 40f, 30f, 60f);
        //    _store.AddCameraPosition(testCamera);
        //    _store.AddCameraPosition(testCamera2);
        //}

        public bool Freeze() {
            bool isFrozen = _cameraPositionFreezer.Toggle();
            SubForCameraChanges(!isFrozen);
            return isFrozen;
        }
    }
}