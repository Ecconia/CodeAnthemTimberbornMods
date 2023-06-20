using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TB_CameraTweaker.CameraSaveSystem;
using TB_CameraTweaker.KsHelperLib.Logger;

namespace TB_CameraTweaker.CameraPositions.Saver
{
    internal class CameraPositionSaverJson : ICameraPositionSaver
    {
        private readonly LogProxy _log = new("Camera Positions: Json Saver");
        private readonly string _jsonSaveFile;

        public CameraPositionSaverJson(string saveFile) {
            _jsonSaveFile = saveFile;
        }

        public IEnumerable<CameraPositionInfo> Load() {
            IEnumerable<CameraPositionInfo> loadedData = new List<CameraPositionInfo>();

            if (!File.Exists(_jsonSaveFile)) {
                _log.LogWarning("Load() - Failed: save file does not exist: " + _jsonSaveFile);
                return loadedData;
            }

            try {
                using (StreamReader r = new StreamReader(_jsonSaveFile)) {
                    string json = r.ReadToEnd();
                    var deserializedData = JsonConvert.DeserializeObject<List<CameraPositionInfo>>(json);
                    if (deserializedData != null) {
                        loadedData = deserializedData;
                        _log.LogDebug("Load() - Success: #" + loadedData.Count());
                    }
                }
            }
            catch (System.Exception e) {
                _log.LogError("Load() - Failed: Unable to load data " + e.Message);
            }
            return loadedData;
        }

        public bool Save(List<CameraPositionInfo> cameraPositionInfos) {
            if (cameraPositionInfos == null || cameraPositionInfos?.Count == 0) {
                _log.LogError("Save() - Failed: Invalid data,");
                return false;
            }

            try {
                string objectsAsJsonString = JsonConvert.SerializeObject(cameraPositionInfos, Formatting.Indented);
                using (StreamWriter w = new StreamWriter(_jsonSaveFile, false)) {
                    w.WriteLine(objectsAsJsonString);
                }
            }
            catch (System.Exception) {
                _log.LogFatal("Save() - Failed: Unable to save data");
                // not sure if we should delete save file here to avoid corrupt data
                //DeleteSaveFile()
                return false;
            }
            return true;
        }

        private void DeleteSaveFile() {
            if (File.Exists(_jsonSaveFile)) {
                File.Delete(_jsonSaveFile);
                _log.LogError("DeleteSaveFile() - Deleted save file");
            }
        }
    }
}