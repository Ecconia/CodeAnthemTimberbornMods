using System;
using TB_CameraTweaks.Configs;
using TimberApi.UiBuilderSystem.CustomElements;
using TimberApi.UiBuilderSystem.ElementSystem;
using UnityEngine;
using UnityEngine.UIElements;

namespace TB_CameraTweaks.UI.Wrappers
{
    internal class SliderOption
    {
        private LocalizableLabel _label;
        private LocalizableLabel _label2;
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
                .SetLabel(Config.SliderText)
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
            builder
                //.SetFlexDirection(FlexDirection.Row)
                .AddPreset(factory => factory.Labels().GameTextSmall(text: $"[Min: {Config.Min}]"))

                .AddPreset(factory => factory.Sliders().SliderIntCircle(0, steps, (int)Math.Ceiling(Config.Value),
                name: nameof(Config.Key) + "slider", width: 0,
                builder: builder => builder
                .SetStyle(style =>
                    {
                        //style.position = Position.Relative;
                        //style.flexShrink = 0;
                        //style.justifyContent = Justify.Center;
                        //style.alignSelf = Align.Auto;
                    }
                )

                //.SetLabel($"[Min: {Config.Min}]")
                .AddPreset(factory => factory.Labels().GameTextSmall(text: $"[Max: {Config.Max}]"))
                .ModifyElement(x =>
                {
                    x.RegisterValueChangedCallback(changeEvent =>
                        {
                            float sum = (changeEvent.newValue * Config.Step) + Config.Min;
                            Config.Value = sum;
                            _label.text = $"{Config.LabelText}: {sum}";
                        }
                    );
                })
            ));
        }

        private void MakeSmoothSliderBackup(VisualElementBuilder builder)
        {
            int steps = (int)Math.Ceiling((Config.Max - Config.Min) / Config.Step);
            builder
                //.SetFlexDirection(FlexDirection.Row)
                .AddPreset(factory => factory.Labels().GameTextSmall(text: $"[Min: {Config.Min}]", builder: builder => builder.SetStyle(style =>
                {
                    //style.alignSelf = Align.Auto;
                    //style.marginLeft = new Length(5, LengthUnit.Pixel);
                    //style.marginRight = new Length(5, LengthUnit.Pixel);
                    //style.alignContent = Align.Center;
                    //style.position = Position.Relative;
                    //style.color = new Color(255, 100, 255);
                    //style.fontSize = 20;
                }
                )

                //.AddPreset(factory => factory.Labels().GameTextBig(text: $"[Min: {Config.Min}]"))
                //builder: builder => builder.SetStyle(style => style.marginBottom = _lenght)
                .AddPreset(factory => factory.Sliders().SliderIntCircle(0, steps, (int)Math.Ceiling(Config.Value),
                name: nameof(Config.Key) + "slider", width: 0,
                builder: builder => builder
                .SetStyle(style =>
                {
                    //style.position = Position.Relative;
                    //style.flexShrink = 0;
                    //style.justifyContent = Justify.Center;
                    //style.alignSelf = Align.Auto;
                    //style.marginLeft = new Length(5, LengthUnit.Pixel);
                    //style.marginRight = new Length(5, LengthUnit.Pixel);
                }
                )
                //.SetLabel($"[Min: {Config.Min}]")

                //.ModifyElement(x =>
                //{
                //    x.RegisterValueChangedCallback(changeEvent =>
                //        {
                //            float sum = (changeEvent.newValue * Config.Step) + Config.Min;
                //            Config.Value = sum;
                //            _label.text = $"{Config.LabelText}: {sum}";
                //        }
                //    );
                //})
                .AddPreset(factory => factory.Labels().GameTextSmall(text: $"[Max: {Config.Max}]"))
                .SetStyle(style =>
                {
                    //style.marginBottom = _lenght
                    //style.alignContent = Align.Center
                    //style.alignItems = Align.Stretch;
                    //style.position = Position.Relative;
                    //style.alignContent = Align.Stretch;
                    //style.justifyContent = Justify.Center;
                    style.fontSize = 20;

                    //style.alignSelf = Align.Center;
                }
                )
                .ModifyElement(x =>
                {
                    x.RegisterValueChangedCallback(changeEvent =>
                    {
                        float sum = (changeEvent.newValue * Config.Step) + Config.Min;
                        Config.Value = sum;
                        _label.text = $"{Config.LabelText}: {sum}";
                    }
                    );
                })
                .Build()
            ))));
        }
    }
}