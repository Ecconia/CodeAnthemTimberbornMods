using BepInEx.Configuration;
using System.Text.RegularExpressions;
using TB_CameraTweaks.Configs;
using TB_CameraTweaks.UI;
using TB_CameraTweaks.UI.Wrappers;
using TimberApi.DependencyContainerSystem;
using TimberApi.UiBuilderSystem.CustomElements;
using TimberApi.UiBuilderSystem.ElementSystem;
using Timberborn.Localization;
using UnityEngine.UIElements;

namespace TB_CameraTweaks.Patchers
{
    internal class CameraZoomPatcher
    {
        private SliderOption ZoomFactor;
        private ILoc Loc;

        public CameraZoomPatcher()
        {
            UIRegister.AddUiElements += MenuElements;
        }

        private void SetupConfig()
        {
            var cfg = new SliderConfig(
                key: "Zoom Factor",
                description: "Camera Zoom Factor (default: 1.3)",
                min: 1.3f, max: 2.8f, def: 1.3f,
                labelText: Loc.T($"{Plugin._tocTag}.menu.zoomfactor"),
                sliderText: "Move Slider"
                );
            cfg.Step = 0.1f;
            ZoomFactor = new SliderOption(cfg);

            //_zoomFactorCfg = Plugin.Config.Bind(MyPluginInfo.PLUGIN_NAME, "Zoom Factor", zoomFactor_def,
            //new ConfigDescription("Camera Zoom Factor (default: 1.3)", new AcceptableValueRange<float>(zoomFactor_min, zoomFactor_max)));
        }

        private void MenuElements(VisualElementBuilder obj)
        {
            Loc = DependencyContainer.GetInstance<ILoc>();
            SetupConfig();

            ZoomFactor.Build(obj);

            //var loc = DependencyContainer.GetInstance<ILoc>();

            //obj.AddPreset(factory => factory.Labels().GameTextBig(
            //    name: nameof(RotatingSunPlugin.Config.TemperateSunAngleLow) + "Label",
            //    text: $"{loc.T(_rotatingTemperateLowLoc)}: {RotatingSunPlugin.Config.TemperateSunAngleLow}",
            //    builder: builder => builder.SetStyle(style => style.marginBottom = new Length(10, LengthUnit.Pixel))));

            //obj.AddPreset(factory => factory.Sliders().SliderIntCircle(_sunAngleLow, _sunAngleHigh, value: RotatingSunPlugin.Config.TemperateSunAngleLow, name: nameof(RotatingSunPlugin.Config.TemperateSunAngleLow), builder: builder => builder.SetStyle(style => style.marginBottom = new Length(10, LengthUnit.Pixel))));

            //obj.AddPreset(factory => factory.Sliders(locKey: $"{Plugin._tocTag}.menu_header", name: _zoomFactorCfg.Definition.Key, builder: builder => builder.SetStyle(style => style.marginBottom = new Length(10, Pixel))));
            //obj.AddPreset(factory => factory.Labels().GameTextBig(
            //    name: nameof(_zoomFactorCfg.Definition.Key) + "label",
            //    text: $"{loc.T($"{Plugin._tocTag}.menu.zoomfactor")}: {_zoomFactorCfg.Value}",
            //    builder: builder => builder.SetStyle(style => style.marginBottom = new Length(10, LengthUnit.Pixel))
            //    .ModifyElement(x => _zoomFactorLabel = x)
            //    //.Q<Slider>(
            //    //    name: _zoomFactorCfg.Definition.Key).RegisterValueChangedCallback(changeEvent =>
            //    //    Plugin.Log.LogWarning($"executed: value: {changeEvent.newValue}"
            //    //    //x.Q<LocalizableLabel>(name: _zoomFactorCfg.Definition.Key + "label").text = $"{loc.T($"{Plugin._tocTag}.menu.zoomfactor")}: {changeEvent.newValue}"
            //    //    )))
            //    ));

            //obj.AddPreset(factory => factory.Sliders().SliderCircle(1.3f, 2.8f, _zoomFactorCfg.Value,
            //    //locKey: $"{Plugin._tocTag}.menu.zoomfactor",
            //    name: _zoomFactorCfg.Definition.Key,
            //    builder: builder => builder.SetStyle(style => style.marginBottom = new Length(10, LengthUnit.Pixel))
            //    .SetValue(_zoomFactorCfg.Value).SetLabel(_zoomFactorCfg.Value.ToString())
            //    .ModifyElement(x => x.Q<Slider>(
            //        name: _zoomFactorCfg.Definition.Key).RegisterValueChangedCallback(changeEvent =>
            //        {
            //            _zoomFactorCfg.Value = changeEvent.newValue;
            //            //Plugin.Log.LogWarning($"executed: value: {changeEvent.newValue}");
            //            //_zoomFactorLabel = x.Q<Label>(_zoomFactorCfg.Definition.Key + "label");
            //            //var test = x.Q(nameof(_zoomFactorCfg.Definition.Key) + "label");
            //            //Plugin.Log.LogWarning($"executed: {hehe.GetType()}");
            //            //if (test.GetType() == typeof(UnityEngine.UIElements.Label))
            //            //{
            //            //    Plugin.Log.LogWarning($"executed: {test.GetType()}");
            //            //}
            //            //Plugin.Log.LogWarning($"executed: value: {_zoomFactorLabel.text}");

            // _zoomFactorLabel.text = $"{loc.T($"{Plugin._tocTag}.menu.zoomfactor")}:
            // {changeEvent.newValue}"; //_zoomFactorLabel.text =
            // $"{loc.T($"{Plugin._tocTag}.menu.zoomfactor")}: {changeEvent.newValue}";
            // //x.Q<Label>(name: _zoomFactorCfg.Definition.Key + "label").text =
            // $"{loc.T($"{Plugin._tocTag}.menu.zoomfactor")}: {changeEvent.newValue}"; } ))
            // .ModifyLabelElement(x => x.name = _zoomFactorCfg.Value.ToString()) ));

            //$"{Plugin._tocTag}.menu.zoomfactor", builder: builder => builder.SetStyle(style =>
            //{
            //    style.alignSelf = Align.Center; style.marginBottom = new Length(10, LengthUnit.Pixel);
            //})));

            //obj.
            //obj.Q<Slider>(_zoomFactorCfg.Definition.Key).value = _zoomFactorCfg.Value;

            // menuContent.AddPreset(factory => factory.Labels().DefaultBig(BuildingsHeaderLocKey,
            // builder: builder => builder.SetStyle(style => { style.alignSelf = Align.Center;
            // style.marginBottom = new Length(10, Pixel); }))); foreach (var thing in
            // StatusHiderPlugin.BuildingStatusThings) { menuContent.AddPreset(factory =>
            // factory.Toggles().Checkbox(locKey: thing.LocKey, name: thing.ToggleName, builder:
            // builder => builder.SetStyle(style => style.marginBottom = new Length(10, Pixel)))); }
        }

        //[HarmonyPatch(typeof(CameraComponent), nameof(CameraComponent.VerticalAngle))]
        //public static class CameraZoomPatch
        //{
        //    public static float Prefix()
        //    {
        //        return _zoomFactorCfg.Value;
        //    }
        //}
    }
}