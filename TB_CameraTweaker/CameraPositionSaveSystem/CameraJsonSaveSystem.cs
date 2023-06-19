using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TB_CameraTweaker.KsHelperLib.Logger;
using Timberborn.Common;

//TODO: maybe add events for updated/changed position info and trigger save method

namespace TB_CameraTweaker.CameraSaveSystem
{
    internal class CameraJsonSaveSystem
    {
        public ReadOnlyList<CameraPositionInfo> CameraPositionList = new();
        private static LogProxy Log = new("[Camera Position Save System] ");
        private readonly List<CameraPositionInfo> _savedCameraPositions = new();
        private string _cameraPositionConfigFilePath;

        public CameraJsonSaveSystem() {
            _savedCameraPositions.Clear();
            _cameraPositionConfigFilePath = $@"{BepInEx.Paths.ConfigPath}\{MyPluginInfo.PLUGIN_GUID}_cameraPositions.json";
            LoadData();
        }

        public bool AddCameraPositionInfo(CameraPositionInfo cameraPositionInfo) {
            if (cameraPositionInfo == null) return false;

            bool isNameAlreadyUsed = _savedCameraPositions.Any(x => x.Name == cameraPositionInfo.Name);
            if (isNameAlreadyUsed) {
                Log.LogError("a camera position with the same name already exists");
                return false;
            }

            _savedCameraPositions.Add(cameraPositionInfo);
            SaveData();
            return true;
        }

        public bool RemoveCameraPositionInfo(string cameraPositionName) {
            bool doesCameraDataExist = GetCameraPositionInfo(cameraPositionName, out CameraPositionInfo cameraPositionInfo);

            if (!doesCameraDataExist) { return false; }

            _savedCameraPositions.Remove(cameraPositionInfo);
            SaveData();
            return true;
        }

        private bool GetCameraPositionInfo(string cameraPositionName, out CameraPositionInfo cameraPositionInfo) {
            cameraPositionInfo = _savedCameraPositions.Where(x => x.Name == cameraPositionName).FirstOrDefault();
            if (cameraPositionInfo == null) {
                Log.LogError("could not find a matching camera position");
                return false;
            }
            return true;
        }

        private bool LoadData() {
            _savedCameraPositions.Clear();

            if (!File.Exists(_cameraPositionConfigFilePath)) return true;

            try {
                using (StreamReader r = new StreamReader(_cameraPositionConfigFilePath)) {
                    string json = r.ReadToEnd();
                    List<CameraPositionInfo> items = JsonConvert.DeserializeObject<List<CameraPositionInfo>>(json);
                    _savedCameraPositions.AddRange(items);
                }
                return true;
            }
            catch (System.Exception) {
                Log.LogError("Failed to load camera position data");
                return false;
            }
        }

        private void SaveData() {
            try {
                string objectsAsJsonString = JsonConvert.SerializeObject(_savedCameraPositions, Formatting.Indented);
                using (StreamWriter w = new StreamWriter(_cameraPositionConfigFilePath, true)) {
                    w.WriteLine(objectsAsJsonString);
                }
            }
            catch (System.Exception) {
                LogProxy._logger.LogError("Failed to save camera position data");
            }
        }
    }
}