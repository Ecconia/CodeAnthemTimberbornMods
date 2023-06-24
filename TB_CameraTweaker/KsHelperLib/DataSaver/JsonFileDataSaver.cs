using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using TB_CameraTweaker.KsHelperLib.Logger;

namespace TB_CameraTweaker.KsHelperLib.DataSaver
{
    internal class JsonFileDataSaver<T> : IDataSaver<T>
    {
        private readonly LogProxy _log = new("Json Saver: " + nameof(T));
        public string SaveFile { get; set; }

        public IEnumerable<T> Load() {
            IEnumerable<T> loadedData = new List<T>();

            if (!File.Exists(SaveFile)) {
                _log.LogDebug("Load() - Failed: file does not exist: " + SaveFile);
                return loadedData;
            }

            try {
                using (StreamReader r = new(SaveFile)) {
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
                // not sure if we should delete save file here to avoid corrupt data
                DeleteSaveFile();
                _log.LogInfo("Save() - Failed: No objects, deleted save file");
                return false;
            }

            try {
                string objectsAsJsonString = JsonConvert.SerializeObject(objectsToSave, Formatting.Indented);
                using (StreamWriter w = new(SaveFile, false)) {
                    w.WriteLine(objectsAsJsonString);
                }
            }
            catch (System.Exception e) {
                _log.LogFatal("Save() - Failed: Unable to save data. Error: " + e);
                // not sure if we should delete save file here to avoid corrupt data
                DeleteSaveFile();
                return false;
            }
            return true;
        }

        private void DeleteSaveFile() {
            if (File.Exists(SaveFile)) {
                File.Delete(SaveFile);
            }
        }
    }
}