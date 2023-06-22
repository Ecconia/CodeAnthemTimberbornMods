using System;
using TimberApi.UiBuilderSystem.ElementSystem;
using UnityEngine.UIElements;

namespace TB_CameraTweaker.KsHelperLib.UI.Elements.CheckBox
{
    internal class CheckBoxUIElement : IConfigUIElement<bool>
    {
        public event Action<bool> ValueChanged;

        public bool CurrentValue => _config.Value;

        private readonly CheckBoxConfig _config;
        //private LocalizableLabel _label;

        public CheckBoxUIElement(CheckBoxConfig config) {
            _config = config;
            _config.Updated += () => ValueChanged?.Invoke(_config.Value);
        }

        public void Build(VisualElementBuilder builder) {
            //MakeLabel(builder);
            MakeCheckBox(builder);
            FooterNote(builder);
        }

        private void FooterNote(VisualElementBuilder builder) {
            builder.AddPreset(factory => factory.Labels().GameTextSmall(text: _config.FooterText, builder: builder => builder.SetStyle(style => {
                style.alignSelf = Align.FlexStart;
                style.fontSize = 12;
                style.paddingBottom = 15;
                style.marginTop = -3;
                style.marginLeft = 2;
            })));
        }

        private void MakeCheckBox(VisualElementBuilder builder) {
            builder
                .AddPreset(factory => factory.Toggles().Checkbox(name: nameof(_config.Key) + "CheckBox", text: _config.LabelText, builder: builder => builder.SetStyle(style => {
                    // could place style here
                }).ModifyElement(x => {
                    x.value = _config.Value;
                    x.RegisterValueChangedCallback(changeEvent => _config.Value = changeEvent.newValue);
                })));
        }

        //private void MakeLabel(VisualElementBuilder builder) {
        //    builder.AddPreset(factory => factory.Labels().GameTextBig(
        //                    name: nameof(_config.Key) + "label",
        //                    text: $"{_config.LabelText}: {_config.Value}",
        //                    builder: builder => builder.SetStyle(style => style.marginTop = 10)
        //                     .ModifyElement(x => _label = x)
        //        ));
        //}
    }
}