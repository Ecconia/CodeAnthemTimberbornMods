using System;
using System.Collections.Generic;
using System.Linq;

namespace TB_CameraTweaker.KsHelperLib.UI.StoreHelper
{
    internal class ActionPriorityStore<T>
    {
        private readonly Dictionary<Action<T>, int> _uiFeaturesDict = new();

        public void RegisterFeature(Action<T> feature, int priority) {
            if (feature == null) {
                Plugin.Log.LogFatal("RegisterFeature() - Feature is null");
                return;
            }

            if (_uiFeaturesDict.TryAdd(feature, priority)) return;
            Plugin.Log.LogFatal("RegisterFeature() - Failed to add feature: " + nameof(feature));
        }

        public void UnregisterFeature(Action<T> feature) {
            if (feature == null) {
                Plugin.Log.LogFatal("UnregisterFeature() - Feature is null");
                return;
            }

            if (!_uiFeaturesDict.ContainsKey(feature)) {
                Plugin.Log.LogFatal("UnregisterFeature() - Feature not in list: " + nameof(feature));
                return;
            }

            _uiFeaturesDict.Remove(feature);
        }

        public IEnumerable<Action<T>> GetAllFeaturesByDescendingPriority() {
            return _uiFeaturesDict
                .OrderByDescending(pair => pair.Value)
                .Select(x => x.Key)
                .ToList();
        }

        public IEnumerable<Action<T>> GetAllFeaturesByAscendingPriority() {
            return _uiFeaturesDict
                .OrderBy(pair => pair.Value)
                .Select(x => x.Key)
                .ToList();
        }


        public void InvokeActions(T parameter, bool useDescendingPriority = false) {
            if (useDescendingPriority) {
                foreach (var action in GetAllFeaturesByDescendingPriority()) {
                    action?.Invoke(parameter);
                }
                return;
            }
            foreach (var action in GetAllFeaturesByAscendingPriority()) {
                action?.Invoke(parameter);
            }
        }
    }
}