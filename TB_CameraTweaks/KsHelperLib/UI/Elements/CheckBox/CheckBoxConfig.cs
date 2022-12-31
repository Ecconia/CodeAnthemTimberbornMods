using BepInEx.Configuration;
using System;

namespace TB_CameraTweaks.KsHelperLib.UI.Elements.CheckBox
{
    internal class CheckBoxConfig
    {
        public CheckBoxConfig(string key, string description, bool def, string labelText)
        {
            Key = key;
            Description = description;

            Default = def;

            LabelText = labelText;

            CreateConfigEntry();
        }

        private void CreateConfigEntry()
        {
            ConfigDescription desc = new ConfigDescription(Description);
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
        public bool Default { get; }
        public bool Value { get => Config.Value; set => Config.Value = value; }

        public event EventHandler Updated;

        private ConfigEntry<bool> Config { get; set; }
    }
}