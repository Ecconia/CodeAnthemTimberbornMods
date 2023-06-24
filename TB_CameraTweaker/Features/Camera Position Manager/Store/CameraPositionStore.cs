using System;
using System.Collections.Generic;
using System.Linq;
using TB_CameraTweaker.KsHelperLib.DataSaver;
using TB_CameraTweaker.KsHelperLib.Logger;
using TB_CameraTweaker.Models;
using Timberborn.Common;
using Timberborn.SingletonSystem;

//TODO: maybe add events for updated/changed position info and trigger save method

namespace TB_CameraTweaker.CameraPositions.Store
{
    internal class CameraPositionStore : ILoadableSingleton
    {
        public event Action ListChanged;

        public ReadOnlyList<CameraPositionInfo> SavedCameraPositions { get; }

        private readonly LogProxy _log = new("[Camera Positions: Store] ");
        private readonly List<CameraPositionInfo> _savedCameraPositions = new();
        private readonly IDataSaver<CameraPositionInfo> _saver;

        public CameraPositionStore(IDataSaver<CameraPositionInfo> saver) {
            _saver = saver;
            _saver.SaveFile = $@"{BepInEx.Paths.ConfigPath}\{MyPluginInfo.PLUGIN_GUID}_cameraPositions.json";
            SavedCameraPositions = _savedCameraPositions.AsReadOnlyList();
        }

        public bool AddCameraPosition(CameraPositionInfo cameraPositionInfo) {
            if (cameraPositionInfo == null) {
                _log.LogError("AddCameraPosition() - Failed: object is null");
                return false;
            }
            bool isNameAlreadyUsed = _savedCameraPositions.Any(x => x.Name == cameraPositionInfo.Name);
            if (isNameAlreadyUsed) {
                _log.LogError($"AddCameraPosition() - Failed: a camera position with the name '{cameraPositionInfo.Name}' already exists");
                return false;
            }
            _savedCameraPositions.Add(cameraPositionInfo);
            _log.LogWarning("AddCameraPosition() - Success: " + cameraPositionInfo.Name);
            _saver.Save(_savedCameraPositions);
            ListChanged?.Invoke();
            return true;
        }

        public bool GetCameraPosition(string cameraName, out CameraPositionInfo cameraPositionInfo) {
            cameraPositionInfo = _savedCameraPositions.Where(x => x.Name == cameraName).FirstOrDefault();
            if (cameraPositionInfo == null) {
                _log.LogWarning("GetCameraPosition() - could not find a matching camera position with name: " + cameraName);
                return false;
            }
            return true;
        }

        public CameraPositionInfo GetNext(CameraPositionInfo current) {
            if (current == null) {
                return _savedCameraPositions.FirstOrDefault();
            }
            return _savedCameraPositions.SkipWhile(x => x != current).Skip(1).DefaultIfEmpty(_savedCameraPositions[0]).FirstOrDefault();
        }

        public CameraPositionInfo GetPrevious(CameraPositionInfo current) {
            if (current == null) {
                return _savedCameraPositions.LastOrDefault();
            }
            return _savedCameraPositions.TakeWhile(x => x != current).DefaultIfEmpty(_savedCameraPositions[^1]).LastOrDefault();
        }

        public void Load() {
            _savedCameraPositions.AddRange(_saver.Load());
        }

        public bool RemoveCameraPosition(CameraPositionInfo cameraPositionInfo) {
            if (cameraPositionInfo == null) {
                _log.LogError("RemoveCameraPosition() - Failed: object is null");
                return false;
            }

            if (_savedCameraPositions.Remove(cameraPositionInfo)) {
                _log.LogDebug("RemoveCameraPosition() - Success: " + cameraPositionInfo.Name);
                _saver.Save(_savedCameraPositions);
                ListChanged?.Invoke();
                return true;
            }
            _log.LogWarning("RemoveCameraPosition() - Failed: " + cameraPositionInfo.Name);
            return false;
        }

        public bool RemoveCameraPosition(string cameraName) {
            bool doesCameraDataExist = GetCameraPosition(cameraName, out CameraPositionInfo cameraPositionInfo);
            if (!doesCameraDataExist) {
                _log.LogError("RemoveCameraPosition() - Failed: object not found");
                return false;
            }
            return RemoveCameraPosition(cameraPositionInfo);
        }

        //internal CameraPositionInfo GetFirst() {
        //    if (_savedCameraPositions.Count == 0) return new CameraPositionInfo("none", DependencyContainer.GetInstance<CameraGetPositionPatcher>().GetCurrentPosition());
        //    return _savedCameraPositions.FirstOrDefault();
        //}
    }
}