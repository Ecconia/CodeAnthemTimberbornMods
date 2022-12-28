using System;
using System.Collections.Generic;
using System.Text;
using TimberApi.UiBuilderSystem;
using UnityEngine.UIElements;

namespace TB_CameraTweaks.UI
{
    internal class bla
    {
        public VisualElement GetPanel()
        {
            UIBoxBuilder boxBuilder = _uiBuilder.CreateBoxBuilder()
                .SetHeight(new Length(520, Pixel))
                .SetWidth(new Length(600, Pixel))
                .ModifyScrollView(builder => builder.SetName("elementPreview"));

            var sunOptionsContent = _uiBuilder.CreateComponentBuilder().CreateVisualElement();

            sunOptionsContent.AddPreset(factory => factory.Labels().DefaultHeader(_rotatingOptionsHeaderLoc, builder: builder => builder.SetStyle(style => { style.alignSelf = Align.Center; style.marginBottom = new Length(10, Pixel); })));
            sunOptionsContent.AddPreset(factory => factory.Toggles().Checkbox(locKey: _rotatingEnabledLoc, name: "EnableSunRotation", builder: builder => builder.SetStyle(style => style.marginBottom = new Length(10, Pixel))));

            sunOptionsContent.AddPreset(factory => factory.Labels().GameTextBig(name: nameof(RotatingSunPlugin.Config.TemperateSunAngleLow) + "Label", text: $"{_loc.T(_rotatingTemperateLowLoc)}: {RotatingSunPlugin.Config.TemperateSunAngleLow}", builder: builder => builder.SetStyle(style => style.marginBottom = new Length(10, Pixel))));
            sunOptionsContent.AddPreset(factory => factory.Sliders().SliderIntCircle(_sunAngleLow, _sunAngleHigh, value: RotatingSunPlugin.Config.TemperateSunAngleLow, name: nameof(RotatingSunPlugin.Config.TemperateSunAngleLow), builder: builder => builder.SetStyle(style => style.marginBottom = new Length(10, Pixel))));
            sunOptionsContent.AddPreset(factory => factory.Labels().GameTextBig(name: nameof(RotatingSunPlugin.Config.TemperateSunAngleHigh) + "Label", text: $"{_loc.T(_rotatingTemperateHighLoc)}: {RotatingSunPlugin.Config.TemperateSunAngleHigh}", builder: builder => builder.SetStyle(style => style.marginBottom = new Length(10, Pixel))));
            sunOptionsContent.AddPreset(factory => factory.Sliders().SliderIntCircle(_sunAngleLow, _sunAngleHigh, value: RotatingSunPlugin.Config.TemperateSunAngleHigh, name: nameof(RotatingSunPlugin.Config.TemperateSunAngleHigh), builder: builder => builder.SetStyle(style => style.marginBottom = new Length(10, Pixel))));

            sunOptionsContent.AddPreset(factory => factory.Labels().GameTextBig(name: nameof(RotatingSunPlugin.Config.DroughtSunAngleLow) + "Label", text: $"{_loc.T(_rotatingDroughtLowLoc)}: {RotatingSunPlugin.Config.DroughtSunAngleLow}", builder: builder => builder.SetStyle(style => style.marginBottom = new Length(10, Pixel))));
            sunOptionsContent.AddPreset(factory => factory.Sliders().SliderIntCircle(_sunAngleLow, _sunAngleHigh, value: RotatingSunPlugin.Config.DroughtSunAngleLow, name: nameof(RotatingSunPlugin.Config.DroughtSunAngleLow), builder: builder => builder.SetStyle(style => style.marginBottom = new Length(10, Pixel))));
            sunOptionsContent.AddPreset(factory => factory.Labels().GameTextBig(name: nameof(RotatingSunPlugin.Config.DroughtSunAngleHigh) + "Label", text: $"{_loc.T(_rotatingDroughtHighLoc)}: {RotatingSunPlugin.Config.DroughtSunAngleHigh}", builder: builder => builder.SetStyle(style => style.marginBottom = new Length(10, Pixel))));
            sunOptionsContent.AddPreset(factory => factory.Sliders().SliderIntCircle(_sunAngleLow, _sunAngleHigh, value: RotatingSunPlugin.Config.DroughtSunAngleHigh, name: nameof(RotatingSunPlugin.Config.DroughtSunAngleHigh), builder: builder => builder.SetStyle(style => style.marginBottom = new Length(10, Pixel))));

            sunOptionsContent.AddPreset(factory => factory.Labels().GameTextBig(name: nameof(RotatingSunPlugin.Config.MoonAngle) + "Label", text: $"{_loc.T(_rotatingMoonAngleLoc)}: {RotatingSunPlugin.Config.MoonAngle}", builder: builder => builder.SetStyle(style => style.marginBottom = new Length(10, Pixel))));
            sunOptionsContent.AddPreset(factory => factory.Sliders().SliderIntCircle(_sunAngleLow, _sunAngleHigh, value: RotatingSunPlugin.Config.MoonAngle, name: nameof(RotatingSunPlugin.Config.MoonAngle), builder: builder => builder.SetStyle(style => style.marginBottom = new Length(10, Pixel))));

            sunOptionsContent.AddPreset(factory => factory.Toggles().Checkbox(locKey: _rotatingSunFlowerRotationEnabledLoc, name: "EnableSunflowerRotation"));

            boxBuilder.AddComponent(sunOptionsContent.Build());

            VisualElement root = boxBuilder.AddCloseButton("CloseButton").SetBoxInCenter().AddHeader(_rotatingOptionsHeaderLoc).BuildAndInitialize();
            root.Q<Button>("CloseButton").clicked += OnUICancelled;
            root.Q<Toggle>("EnableSunRotation").RegisterValueChangedCallback(ToggleSunEnabled);
            root.Q<Toggle>("EnableSunRotation").value = RotatingSunPlugin.Config.RotatingSunEnabled;
            root.Q<Toggle>("EnableSunflowerRotation").RegisterValueChangedCallback(ToggleSunflowerEnabled);
            root.Q<Toggle>("EnableSunflowerRotation").value = RotatingSunPlugin.Config.RotatingSunFlowersEnabled;

            var temperateLowSlider = root.Q<SliderInt>(nameof(RotatingSunPlugin.Config.TemperateSunAngleLow)).RegisterValueChangedCallback(TemperateSunAngleLowSliderChangedCallback);
            var temperateHighSlider = root.Q<SliderInt>(nameof(RotatingSunPlugin.Config.TemperateSunAngleHigh)).RegisterValueChangedCallback(TemperateSunAngleHighSliderChangedCallback);

            _temperateLowLabel = root.Q<Label>(nameof(RotatingSunPlugin.Config.TemperateSunAngleLow) + "Label");
            _temperateHighLabel = root.Q<Label>(nameof(RotatingSunPlugin.Config.TemperateSunAngleHigh) + "Label");

            var droughtLowSlider = root.Q<SliderInt>(nameof(RotatingSunPlugin.Config.DroughtSunAngleLow)).RegisterValueChangedCallback(DroughtSunAngleLowSliderChangedCallback);
            var droughtHighSlider = root.Q<SliderInt>(nameof(RotatingSunPlugin.Config.DroughtSunAngleHigh)).RegisterValueChangedCallback(DroughtSunAngleHighSliderChangedCallback);

            _droughtLowLabel = root.Q<Label>(nameof(RotatingSunPlugin.Config.DroughtSunAngleLow) + "Label");
            _droughtHighLabel = root.Q<Label>(nameof(RotatingSunPlugin.Config.DroughtSunAngleHigh) + "Label");

            var moongAngleSlider = root.Q<SliderInt>(nameof(RotatingSunPlugin.Config.MoonAngle)).RegisterValueChangedCallback(MoongAngleSliderChangedCallback);
            _moongAngleLabel = root.Q<Label>(nameof(RotatingSunPlugin.Config.MoonAngle) + "Label");

            return root;
        }
    }
}