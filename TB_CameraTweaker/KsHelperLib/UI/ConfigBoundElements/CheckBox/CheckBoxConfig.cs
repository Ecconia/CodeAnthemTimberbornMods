using BepInEx.Configuration;
using System;

namespace TB_CameraTweaker.KsHelperLib.UI.ConfigBoundElements
{
    internal class CheckBoxConfig
    {
        public CheckBoxConfig(string key, string description, bool def, string labelText, string footerText = " ") {
            Key = key;
            Description = description;

            Default = def;

            LabelText = labelText;
            FooterText = footerText;

            CreateConfigEntry();
        }

        private void CreateConfigEntry() {
            ConfigDescription desc = new(Description);
            Config = Plugin.Config.Bind(MyPluginInfo.PLUGIN_NAME, Key, Default, desc);
            Config.SettingChanged += SettingChanged;
        }

        private void SettingChanged(object sender, EventArgs e) {
            Updated?.Invoke();
        }

        public string Key { get; set; }
        public string Description { get; set; }
        public string LabelText { get; set; }
        public string FooterText { get; set; }
        public bool Default { get; set; }
        public bool Value { get => Config.Value; set => Config.Value = value; }

        public event Action Updated;

        private ConfigEntry<bool> Config { get; set; }
    }
}