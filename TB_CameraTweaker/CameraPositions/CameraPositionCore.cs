using System.Collections.Generic;
using TB_CameraTweaker.CameraPositions.Saver;
using TB_CameraTweaker.CameraPositions.Store;
using TB_CameraTweaker.CameraSaveSystem;
using TB_CameraTweaker.KsHelperLib.Logger;
using TB_CameraTweaker.KsHelperLib.UI.Elements.CameraPositionRow;
using TB_CameraTweaker.KsHelperLib.UI.Menu;
using TB_CameraTweaker.Patchers;
using TimberApi.DependencyContainerSystem;
using TimberApi.UiBuilderSystem.ElementSystem;
using UnityEngine;

namespace TB_CameraTweaker.CameraPositionSaveSystem
{
    internal class CameraPositionCore
    {
        private ICameraPositionStore _store;
        private List<CameraPositionRowElement> _cameraPositionRowElements = new List<CameraPositionRowElement>();
        private CameraPositionInfo _activePosition;
        private readonly LogProxy _log = new("[Camera Positions: Core] ");

        public CameraPositionCore() {
            string saveFile = $@"{BepInEx.Paths.ConfigPath}\{MyPluginInfo.PLUGIN_GUID}_cameraPositions.json";
            ICameraPositionSaver saver = new CameraPositionSaverJson(saveFile);
            _store = new CameraPositionStore(saver);

            //_cameraSaveSystem.ListChanged += () => RefreshCameraPositionRows();
            //RefreshCameraPositionRows();

            UIRegister.AddUiElements -= AddUIElements;
            UIRegister.AddUiElements += AddUIElements;

            Plugin.Log.LogWarning("CameraPositionSaveSystemCore Init");
        }

        private void AddUIElements(VisualElementBuilder builder) {
            Plugin.Log.LogWarning("CameraPositionSaveSystemCore Making UI");
            //RefreshCameraPositionRows();

            foreach (var pos in _store.SavedCameraPositions) {
                new CameraPositionRowElement(pos.Name, this).Build(builder);
            }

            //foreach (var row in _cameraPositionRowElements) {
            //    row.Build(builder);
            //}
        }

        //private void RefreshCameraPositionRows() {
        //    _cameraPositionRowElements.Clear();
        //    foreach (var pos in _cameraSaveSystem.SavedCameraPositions) {
        //        _cameraPositionRowElements.Add(new CameraPositionRowElement(pos.Name));
        //    }
        //}

        internal void RemoveCameraPosition(string cameraPositionName) => _store.RemoveCameraPosition(cameraPositionName);

        internal void SetActiveCameraPosition(string cameraPositionName) {
            if (_store.GetCameraPosition(cameraPositionName, out CameraPositionInfo cam)) {
                _activePosition = cam;
                _log.LogDebug("SetActiveCameraPosition() - Success: Set active: " + cam.Name);
            };
        }

        //internal void AddCameraPosition(string cameraPositionName) => _cameraSaveSystem.AddCameraPositionInfo(cameraPositionName);

        public void ResetBackToConfigValues() {
            DependencyContainer.GetInstance<CameraFOVPatcher>().UseConfigValue();
            //DependencyContainer.GetInstance<CameraVerticalAngelLimiterPatcher>().UseConfigValue(); // soft disabled this patch
            DependencyContainer.GetInstance<CameraZoomPatcher>().UseConfigValue();
        }

        private void AddDummyData() {
            var testCamera = new CameraPositionInfo(name: "Test position info Name", new Vector3(), 55f, 40f, 30f, 60f);
            var testCamera2 = new CameraPositionInfo(name: "Test position info Name 2", new Vector3(), 55f, 40f, 30f, 60f);
            _store.AddCameraPosition(testCamera);
            _store.AddCameraPosition(testCamera2);
        }

        internal bool IsPositionActive(string positionName) {
            if (_activePosition == null) return false;
            if (_activePosition.Name == positionName) return true;
            return false;
        }
    }
}