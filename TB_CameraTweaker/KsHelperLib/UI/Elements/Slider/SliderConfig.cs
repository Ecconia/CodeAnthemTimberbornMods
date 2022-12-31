using BepInEx.Configuration;
using System;
using TB_CameraTweaker;

namespace TB_CameraTweaker.KsHelperLib.UI.Elements.Slider
{
    internal class SliderConfig
    {
        public SliderConfig(string key, string description, float min, float max, float def, string labelText, float step = 0, string footerText = " ")
        {
            Key = key;
            Description = description;

            Min = min;
            Max = max;
            Step = step;

            Default = def;

            LabelText = labelText;
            FooterText = footerText;

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

        public string Key { get; set; }
        public string Description { get; set; }
        public string LabelText { get; set; }
        public string FooterText { get; set; }

        public float Step { get; set; }
        public float Min { get; set; }
        public float Max { get; set; }
        public float Default { get; set; }
        public float Value { get => Config.Value; set => Config.Value = value; }

        public event EventHandler Updated;

        private ConfigEntry<float> Config { get; set; }
    }
}