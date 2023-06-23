using TB_CameraTweaker.CameraPositions.Store;
using TB_CameraTweaker.KsHelperLib.DataSaver;
using TB_CameraTweaker.KsHelperLib.Logger;
using TB_CameraTweaker.Models;
using TB_CameraTweaker.Patches;
using TB_CameraTweaker.UI.Tweaks;
using TimberApi.DependencyContainerSystem;
using TimberApi.UiBuilderSystem.ElementSystem;
using UnityEngine;

namespace TB_CameraTweaker.Camera_Position_Manager
{
    internal class CameraPositionManagerCore
    {
        public ICameraPositionStore Store { get; private set; }
        private readonly ICameraPositionStore _store;
        //private readonly List<CameraPositionRowElement> _cameraPositionRowElements = new();
        private CameraPositionInfo _activePosition;
        private readonly LogProxy _log = new("[Camera Positions: Core] ");
        private readonly CameraSetPositionPatcher _cameraPositionPatcher;
        private readonly CameraGetPositionPatcher _cameraGetPositionPatcher;

        public CameraPositionManagerCore(CameraSetPositionPatcher cameraPositionPatcher, CameraGetPositionPatcher cameraGetPositionPatcher) {
            _cameraPositionPatcher = cameraPositionPatcher;
            _cameraGetPositionPatcher = cameraGetPositionPatcher;

            string saveFile = $@"{BepInEx.Paths.ConfigPath}\{MyPluginInfo.PLUGIN_GUID}_cameraPositions.json";
            IDataSaver<CameraPositionInfo> saver = new JsonFileDataSaver<CameraPositionInfo>(saveFile);
            Store = new CameraPositionStore(saver);

            //AddDummyData();

            //_cameraSaveSystem.ListChanged += () => RefreshCameraPositionRows();
            //RefreshCameraPositionRows();

            Plugin.Log.LogWarning("CameraPositionSaveSystemCore Init");
        }

        private void AddUIElements(VisualElementBuilder builder) {
            builder
            .AddPreset(factory => factory.Buttons().Button(name: "Button Name", text: "Add Current Camera", builder: builder => builder.SetStyle(style => {
                style.width = 100;

                style.color = Color.red;
            })
                .ModifyElement(x => x.clicked += () => AddCurrentCameraButton())
            ));

            Plugin.Log.LogWarning("CameraPositionSaveSystemCore Making UI");
            //RefreshCameraPositionRows();

            //foreach (var pos in _store.SavedCameraPositions) {
            //    new CameraPositionRowElement(pos.Name, this).Build(builder);
            //}

            //foreach (var row in _cameraPositionRowElements) {
            //    row.Build(builder);
            //}
        }

        private void AddCurrentCameraButton() {
            //if (_cameraPositionPatcher == null) {
            //    _log.LogError("AddCameraPosition() - Failed: Instance is null, camera not found");
            //    return;
            //}

            string cameraName = GetCameraNamePopup();
            _store.AddCameraPosition(new(cameraName, _cameraGetPositionPatcher.GetCurrentPosition()));
        }

        private string GetCameraNamePopup() {
            string randomName = "Random Name: " + new System.Random().Next(1, 99);
            //_log.LogDebug("GetCameraNamePopup() - Random name generated: " + randomName);
            return randomName;
        }

        //private void RefreshCameraPositionRows() {
        //    _cameraPositionRowElements.Clear();
        //    foreach (var pos in _cameraSaveSystem.SavedCameraPositions) {
        //        _cameraPositionRowElements.Add(new CameraPositionRowElement(pos.Name));
        //    }
        //}

        internal void RemoveCameraPosition(string cameraPositionName) => _store.RemoveCameraPosition(cameraPositionName);

        internal void SetActiveCameraPosition(string cameraPositionName) {
            if (!_store.GetCameraPosition(cameraPositionName, out CameraPositionInfo cam)) {
                _log.LogDebug("SetActiveCameraPosition() - Failed: Could not find position data for: " + cameraPositionName);
                return;
            }

            //if (_cameraPositionPatcher == null) {
            //    _log.LogError("SetActiveCameraPosition() - Failed: Instance is null, camera not found");
            //    return;
            //}

            _activePosition = cam;
            _cameraPositionPatcher.SetJumpPosition(cam);
            _log.LogDebug("SetActiveCameraPosition() - Success: Set active: " + cam.Name);
        }

        public void ResetBackToConfigValues() {
            DependencyContainer.GetInstance<CameraTweakerUI_FOV>().UseConfigValue();
            DependencyContainer.GetInstance<CameraTweakerUI_ZoomLevelLimiter>().UseConfigValue();
        }

        //private void AddDummyData() {
        //    var testCamera = new CameraPositionInfo(name: "Test position info Name", new Vector3(), 55f, 40f, 30f, 60f);
        //    var testCamera2 = new CameraPositionInfo(name: "Test position info Name 2", new Vector3(), 55f, 40f, 30f, 60f);
        //    _store.AddCameraPosition(testCamera);
        //    _store.AddCameraPosition(testCamera2);
        //}

        internal bool IsPositionActive(string positionName) {
            if (_activePosition == null) return false;
            if (_activePosition.Name == positionName) return true;
            return false;
        }
    }
}