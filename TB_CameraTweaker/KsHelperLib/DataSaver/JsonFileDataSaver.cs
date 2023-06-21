using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using TB_CameraTweaker.KsHelperLib.Logger;

namespace TB_CameraTweaker.KsHelperLib.DataSaver
{
    internal class JsonFileDataSaver<T> : IDataSaver<T> where T : class
    {
        private readonly LogProxy _log = new("Json Saver: " + nameof(T));
        private readonly string _jsonSaveFile;

        public JsonFileDataSaver(string saveFile) {
            _jsonSaveFile = saveFile;
        }

        public IEnumerable<T> Load() {
            IEnumerable<T> loadedData = new List<T>();

            if (!File.Exists(_jsonSaveFile)) {
                _log.LogWarning("Load() - Failed: file does not exist: " + _jsonSaveFile);
                return loadedData;
            }

            try {
                using (StreamReader r = new StreamReader(_jsonSaveFile)) {
                    string json = r.ReadToEnd();
                    var deserializedData = JsonConvert.DeserializeObject<List<T>>(json);
                    if (deserializedData != null) {
                        loadedData = deserializedData;
                        _log.LogDebug("Load() - Success: #" + deserializedData.Count);
                    }
                }
            }
            catch (System.Exception e) {
                _log.LogError("Load() - Failed: Unable to load data " + e.Message);
            }
            return loadedData;
        }

        public bool Save(List<T> objectsToSave) {
            if (objectsToSave == null || objectsToSave?.Count == 0) {
                _log.LogError("Save() - Failed: Invalid data");
                return false;
            }

            try {
                string objectsAsJsonString = JsonConvert.SerializeObject(objectsToSave, Formatting.Indented);
                using (StreamWriter w = new StreamWriter(_jsonSaveFile, false)) {
                    w.WriteLine(objectsAsJsonString);
                }
            }
            catch (System.Exception e) {
                _log.LogFatal("Save() - Failed: Unable to save data. Error: " + e);
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