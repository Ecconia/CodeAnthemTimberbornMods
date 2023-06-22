using System;
using TimberApi.UiBuilderSystem.CustomElements;
using TimberApi.UiBuilderSystem.ElementSystem;
using UnityEngine.UIElements;

namespace TB_CameraTweaker.KsHelperLib.UI.Elements.Slider
{
    internal class SliderConfigUIElement : IConfigUIElement<float>
    {
        public event Action<float> ValueChanged;

        protected readonly SliderConfigOptions _config;
        private LocalizableLabel _label;
        private Length _lenght;

        public float CurrentValue { get => _config.Value; }

        public SliderConfigUIElement(SliderConfigOptions config) {
            _config = config;
            _config.Updated += () => ValueChanged?.Invoke(_config.Value);
        }

        public void Build(VisualElementBuilder builder) {
            if (_lenght == null) SetLength();

            MakeLabel(builder);
            if (_config.Step == 0) {
                MakeSlider(builder);
                return;
            }
            MakeSmoothSlider(builder);

            if (!string.IsNullOrEmpty(_config.FooterText)) FooterNote(builder);
        }

        private void FooterNote(VisualElementBuilder builder) {
            builder.AddPreset(factory => factory.Labels().GameTextSmall(text: _config.FooterText, builder: builder => builder.SetStyle(style => {
                style.alignSelf = Align.Center;
                style.fontSize = 12;
                style.paddingBottom = 15;
                style.marginTop = -10;
            })));
        }

        private void MakeLabel(VisualElementBuilder builder) {
            builder.AddPreset(factory => factory.Labels().GameTextBig(
                            name: nameof(_config.Key) + "label",
                            text: $"{_config.LabelText}: {_config.Value}",
                            builder: builder => builder.SetStyle(style => style.marginBottom = _lenght)
                             .ModifyElement(x => _label = x)
                ));
        }

        private void MakeSlider(VisualElementBuilder builder) {
            builder.AddPreset(factory => factory.Sliders().SliderCircle(_config.Min, _config.Max, _config.Value,
                name: nameof(_config.Key) + "slider",
                builder: builder => builder.SetStyle(style => style.marginBottom = _lenght)
                .ModifyElement(x => {
                    x.RegisterValueChangedCallback(changeEvent => {
                        _config.Value = changeEvent.newValue;
                        _label.text = $"{_config.LabelText}: {changeEvent.newValue}";
                    }
                    );
                }
                )
            ));
        }

        private void MakeSmoothSlider(VisualElementBuilder builder) {
            int steps = (int)Math.Ceiling((_config.Max - _config.Min) / _config.Step);
            int currentInSteps = (int)Math.Round((_config.Value - _config.Min) / _config.Step);

            builder.AddComponent(b => {
                b.SetFlexDirection(FlexDirection.Row)

                .AddPreset(factory => factory.Labels().GameTextSmall(text: $"Min: {_config.Min}"))
                .AddPreset(factory => factory.Sliders().SliderIntCircle(0, steps, currentInSteps,
                    name: nameof(_config.Key) + "slider",
                    builder: b => b
                    .SetStyle(s => {
                        s.paddingBottom = 10;
                        s.flexGrow = 1;
                        s.marginLeft = 5;
                        s.marginRight = 5;
                    }
                    ).AddClass("settings-slider__slider")
                    .ModifyElement(x => x.RegisterValueChangedCallback(changeEvent => {
                        float sum = changeEvent.newValue * _config.Step + _config.Min;
                        _config.Value = sum;
                        _label.text = $"{_config.LabelText}: {sum}";
                    }
                    ))
                ))
                .AddPreset(factory => factory.Labels().GameTextSmall(text: $"Max: {_config.Max}"));
            });
        }

        private void SetLength(int size = 10) => _lenght = new Length(size, LengthUnit.Pixel);
    }
}