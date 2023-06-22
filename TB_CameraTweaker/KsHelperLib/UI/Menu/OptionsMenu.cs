using System;
using TB_CameraTweaker.KsHelperLib.Localization;
using TimberApi.DependencyContainerSystem;
using TimberApi.UiBuilderSystem;
using TimberApi.UiBuilderSystem.ElementSystem;
using Timberborn.CoreUI;
using Timberborn.Localization;
using Timberborn.SingletonSystem;
using UnityEngine.UIElements;
using static UnityEngine.UIElements.Length.Unit;

namespace TB_CameraTweaker.KsHelperLib.UI.Menu
{
    internal class OptionsMenu : IPanelController, IUpdatableSingleton
    {
        public event Action<VisualElementBuilder> UpdateUIContent;

        public static Action OpenOptionsDelegate;

        private static bool _requireContentUpdate;
        private readonly ILoc _loc;
        private readonly PanelStack _panelStack;
        private readonly UIBuilder _uiBuilder;
        private VisualElement _currentParent;
        private VisualElementBuilder _currentVisualElementBuilder;

        public OptionsMenu(UIBuilder uiBuilder, PanelStack panelStack) {
            _uiBuilder = uiBuilder;
            _panelStack = panelStack;

            _loc = DependencyContainer.GetInstance<ILoc>();
            OpenOptionsDelegate = OpenOptionsPanel;
        }

        public static void RequireUpdate() => _requireContentUpdate = true;

        /// <summary>
        /// Create the Options Panel
        /// </summary>
        /// <returns></returns>
        public VisualElement GetPanel() {
            UIBoxBuilder boxBuilder = _uiBuilder.CreateBoxBuilder()
                .SetHeight(new Length(600, Pixel))
                .SetWidth(new Length(600, Pixel))
                .ModifyScrollView(builder => builder.SetName("elementPreview"));

            VisualElementBuilder menuContent = _uiBuilder.CreateComponentBuilder().CreateVisualElement();
            AddOptionsTitle(menuContent);

            _currentVisualElementBuilder = menuContent;
            _currentParent = menuContent.Build();
            _requireContentUpdate = true;
            CallInFeaturesUIContent();

            boxBuilder.AddComponent(_currentParent);

            string menuTitle = _loc.T($"{LocConfig.LocTag}.menu.title");
            VisualElement root = boxBuilder.AddCloseButton("CloseButton").SetBoxInCenter().AddHeader(null,
                $"{menuTitle} v" + MyPluginInfo.PLUGIN_VERSION).BuildAndInitialize();
            root.Q<Button>(name: "CloseButton").clicked += OnUICancelled;
            return root;
        }

        public void OnUICancelled() => _panelStack.Pop(this);

        public bool OnUIConfirmed() => false;

        public void UpdateSingleton() {
            CallInFeaturesUIContent();
        }

        /// <summary>
        /// Adds an option title to the menu
        /// </summary>
        /// <returns></returns>
        private static void AddOptionsTitle(VisualElementBuilder menuContent) {
            menuContent.AddPreset(factory => factory.Labels().DefaultHeader(
                $"{LocConfig.LocTag}.menu.options",
                builder: builder => builder.SetStyle(
                    style => {
                        style.alignSelf = Align.Center; style.marginBottom = new Length(10, Pixel);
                    })));
        }

        private void CallInFeaturesUIContent() {
            if (!_requireContentUpdate) return;

            _currentParent.Clear();
            UpdateUIContent?.Invoke(_currentVisualElementBuilder);
            _requireContentUpdate = false;
        }

        private void OpenOptionsPanel() {
            _panelStack.HideAndPush(this);
        }
    }
}