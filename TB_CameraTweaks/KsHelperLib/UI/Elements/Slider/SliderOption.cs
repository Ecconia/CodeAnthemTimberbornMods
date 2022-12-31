using System;
using System.Xml.Linq;
using TimberApi.ConfigSystem;
using TimberApi.UiBuilderSystem.CustomElements;
using TimberApi.UiBuilderSystem.ElementSystem;
using Timberborn.SettingsSystem;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

namespace TB_CameraTweaks.KsHelperLib.UI.Elements.Slider
{
    internal class SliderOption
    {
        private LocalizableLabel _label;
        private Length _lenght;

        public SliderOption(SliderConfig config)
        {
            Config = config;
        }

        public SliderConfig Config { get; }

        public void Build(VisualElementBuilder builder)
        {
            if (_lenght == null) SetLength();

            MakeLabel(builder);
            if (Config.Step == 0)
            {
                MakeSlider(builder);
                return;
            }
            MakeSmoothSlider(builder);
        }

        public void SetLength(int size = 10) => _lenght = new Length(size, LengthUnit.Pixel);

        private void MakeLabel(VisualElementBuilder builder)
        {
            builder.AddPreset(factory => factory.Labels().GameTextBig(
                            name: nameof(Config.Key) + "label",
                            text: $"{Config.LabelText}: {Config.Value}",
                            builder: builder => builder.SetStyle(style => style.marginBottom = _lenght)
                             .ModifyElement(x => _label = x)
                ));
        }

        private void MakeSlider(VisualElementBuilder builder)
        {
            builder.AddPreset(factory => factory.Sliders().SliderCircle(Config.Min, Config.Max, Config.Value,
                name: nameof(Config.Key) + "slider",
                builder: builder => builder.SetStyle(style => style.marginBottom = _lenght)
                .ModifyElement(x =>
                    {
                        x.RegisterValueChangedCallback(changeEvent =>
                            {
                                Config.Value = changeEvent.newValue;
                                _label.text = $"{Config.LabelText}: {changeEvent.newValue}";
                            }
                        );
                    }
                )
            ));
        }

        private void MakeSmoothSlider(VisualElementBuilder builder)
        {
            int steps = (int)Math.Ceiling((Config.Max - Config.Min) / Config.Step);
            int currentInSteps = (int)Math.Round((Config.Value - Config.Min) / Config.Step);

            builder.AddComponent(b =>
            {
                b.SetFlexDirection(FlexDirection.Row)

                .AddPreset(factory => factory.Labels().GameTextSmall(text: $"Min: {Config.Min}"))
                .AddPreset(factory => factory.Sliders().SliderIntCircle(0, steps, currentInSteps,
                    name: nameof(Config.Key) + "slider",
                    builder: b => b
                    .SetStyle(s =>
                    {
                        s.paddingBottom = 10;
                        s.flexGrow = 1;
                        s.marginLeft = 5;
                        s.marginRight = 5;
                    }
                    ).AddClass("settings-slider__slider")
                    .ModifyElement(x => x.RegisterValueChangedCallback(changeEvent =>
                    {
                        float sum = changeEvent.newValue * Config.Step + Config.Min;
                        Config.Value = sum;
                        _label.text = $"{Config.LabelText}: {sum}";
                    }
                    ))
                ))
                .AddPreset(factory => factory.Labels().GameTextSmall(text: $"Max: {Config.Max}"));
            });
        }
    }
}