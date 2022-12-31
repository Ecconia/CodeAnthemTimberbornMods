using System;
using TimberApi.UiBuilderSystem.CustomElements;
using TimberApi.UiBuilderSystem.ElementSystem;
using UnityEngine.UIElements;

namespace TB_CameraTweaks.KsHelperLib.UI.Elements.CheckBox
{
    internal class CheckBoxOption
    {
        private LocalizableLabel _label;

        public CheckBoxOption(CheckBoxConfig config)
        {
            Config = config;
        }

        public CheckBoxConfig Config { get; }

        public void Build(VisualElementBuilder builder)
        {
            //MakeLabel(builder);
            MakeCheckBox(builder);
            FooterNote(builder);
        }

        private void MakeLabel(VisualElementBuilder builder)
        {
            builder.AddPreset(factory => factory.Labels().GameTextBig(
                            name: nameof(Config.Key) + "label",
                            text: $"{Config.LabelText}: {Config.Value}",
                            builder: builder => builder.SetStyle(style => style.marginTop = 10)
                             .ModifyElement(x => _label = x)
                ));
        }

        private void MakeCheckBox(VisualElementBuilder builder)
        {
            builder
                .AddPreset(factory => factory.Toggles().Checkbox(name: nameof(Config.Key) + "CheckBox", text: Config.LabelText, builder: builder => builder.SetStyle(style =>
                {
                    // could place style here
                }).ModifyElement(x =>
                {
                    x.value = Config.Value;
                    x.RegisterValueChangedCallback(changeEvent => Config.Value = changeEvent.newValue);
                })));
        }

        private void FooterNote(VisualElementBuilder builder)
        {
            builder.AddPreset(factory => factory.Labels().GameTextSmall(text: Config.FooterText, builder: builder => builder.SetStyle(style =>
            {
                style.alignSelf = Align.FlexStart;
                style.fontSize = 12;
                style.paddingBottom = 15;
                style.marginTop = -3;
                style.marginLeft = 2;
            })));
        }
    }
}