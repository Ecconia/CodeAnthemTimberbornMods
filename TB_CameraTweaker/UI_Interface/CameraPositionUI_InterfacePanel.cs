using System;
using TB_CameraTweaker.KsHelperLib.Localization;
using TimberApi.UiBuilderSystem;
using TimberApi.UiBuilderSystem.ElementSystem;
using Timberborn.CoreUI;
using Timberborn.SingletonSystem;
using Timberborn.UILayoutSystem;
using UnityEngine.UIElements;

namespace TB_CameraTweaker.UI_Interface
{
    internal class CameraPositionUI_InterfacePanel : ILoadableSingleton, ICameraPositionUI_InterfacePanel
    {
        public Button CameraAddButton { get; private set; }
        public Button CameraFreezeButton { get; private set; }
        public Button CameraJumpButton { get; private set; }
        public Button CameraNextButton { get; private set; }
        public Button CameraPreviousButton { get; private set; }
        public Button CameraRemoveButton { get; private set; }

        private const int _panelOrder = 8;

        private readonly UIBuilder _uiBuilder;
        private readonly CameraPositionUI_InterfacePresetFactory _uiFactory;
        private readonly UILayout _uiLayout;
        private readonly InputBoxShower _inputBoxShower;

        public CameraPositionUI_InterfacePanel(CameraPositionUI_InterfacePresetFactory uiFactory, UIBuilder uiBuilder, UILayout uiLayout, InputBoxShower inputBoxShower) {
            _uiFactory = uiFactory;
            _uiBuilder = uiBuilder;
            _uiLayout = uiLayout;
            _inputBoxShower = inputBoxShower;
        }

        public void Load() {
            VisualElementBuilder panelBuilder = CreateInterface();
            VisualElement uiRoot = CreateUIRoot(panelBuilder);
        }

        public void GetNamePopup(string locKey, Action<string> callback) {
            _inputBoxShower.Create()
                .SetLocalizedMessage(locKey)
                .SetConfirmButton(callback)
                .Show();
        }

        private VisualElementBuilder CreatePanelBuilder() {
            return _uiBuilder
                .CreateComponentBuilder()
                .CreateVisualElement()
                .AddClass("top-right-item")
                .AddClass("square-large--green")
                .SetFlexDirection(FlexDirection.Row)
                .SetFlexWrap(Wrap.Wrap)
                .SetJustifyContent(Justify.Center);
        }

        private VisualElementBuilder CreateInterface() {
            var panelBuilder = CreatePanelBuilder();
            _uiFactory.AddLocalizableLabel(panelBuilder, $"{LocConfig.LocTag}.interface.label");
            _uiFactory.AddLocalizableButton(panelBuilder, $"{LocConfig.LocTag}.interface.freezebutton", nameof(CameraFreezeButton));
            _uiFactory.AddLocalizableButton(panelBuilder, $"{LocConfig.LocTag}.interface.addbutton", nameof(CameraAddButton));
            _uiFactory.AddLocalizableButton(panelBuilder, $"{LocConfig.LocTag}.interface.removebutton", nameof(CameraRemoveButton));
            _uiFactory.AddSpacer(panelBuilder, 20);
            _uiFactory.AddSmallButton(panelBuilder, "<", nameof(CameraPreviousButton));
            _uiFactory.AddTextButton(panelBuilder, "none", nameof(CameraJumpButton));
            _uiFactory.AddSmallButton(panelBuilder, ">", nameof(CameraNextButton));
            return panelBuilder;
        }

        private VisualElement CreateUIRoot(VisualElementBuilder panelBuilder) {
            var uiRoot = panelBuilder.BuildAndInitialize();

            CameraFreezeButton = uiRoot.Q<Button>(name: nameof(CameraFreezeButton));
            CameraAddButton = uiRoot.Q<Button>(name: nameof(CameraAddButton));
            CameraRemoveButton = uiRoot.Q<Button>(name: nameof(CameraRemoveButton));
            CameraPreviousButton = uiRoot.Q<Button>(name: nameof(CameraPreviousButton));
            CameraJumpButton = uiRoot.Q<Button>(name: nameof(CameraJumpButton));
            CameraNextButton = uiRoot.Q<Button>(name: nameof(CameraNextButton));

            _uiLayout.AddTopRight(uiRoot, _panelOrder);
            return uiRoot;
        }
    }
}