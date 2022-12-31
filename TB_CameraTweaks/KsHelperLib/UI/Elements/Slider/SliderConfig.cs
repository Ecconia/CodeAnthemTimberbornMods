using BepInEx.Configuration;
using System;

namespace TB_CameraTweaks.KsHelperLib.UI.Elements.Slider
{
    internal class SliderConfig
    {
        public SliderConfig(string key, string description, float min, float max, float def, string labelText, float step = 0)
        {
            Key = key;
            Description = description;

            Min = min;
            Max = max;
            Step = step;

            Default = def;

            LabelText = labelText;

            CreateConfigEntry();
        }

        private void CreateConfigEntry()
        {
            ConfigDescription desc = new ConfigDescription(Description, new AcceptableValueRange<float>(Min, Max));
            Config = Plugin.Config.Bind(MyPluginInfo.PLUGIN_NAME, Key, Default, desc);
            Config.SettingChanged += SettingChanged;
        }

        private void SettingChanged(object sender, EventArgs e)
        {
            Updated?.Invoke(sender, e);
        }

        public string Key { get; }
        public string Description { get; }
        public string LabelText { get; }
        public float Step { get; set; }
        public float Min { get; }
        public float Max { get; }
        public float Default { get; }
        public float Value { get => Config.Value; set => Config.Value = value; }

        public event EventHandler Updated;

        private ConfigEntry<float> Config { get; set; }
    }
}