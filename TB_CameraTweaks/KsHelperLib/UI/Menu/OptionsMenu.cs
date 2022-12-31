using System;
using TB_CameraTweaks.KsHelperLib.Localization;
using TimberApi.DependencyContainerSystem;
using TimberApi.UiBuilderSystem;
using TimberApi.UiBuilderSystem.ElementSystem;
using Timberborn.CoreUI;
using Timberborn.Localization;
using UnityEngine.UIElements;
using static UnityEngine.UIElements.Length.Unit;

namespace TB_CameraTweaks.KsHelperLib.UI.Menu
{
    internal class OptionsMenu : IPanelController
    {
        public static Action OpenOptionsDelegate;
        private readonly PanelStack _panelStack;
        private readonly UIBuilder _uiBuilder;

        public OptionsMenu(
            UIBuilder uiBuilder,
            PanelStack panelStack)
        {
            _uiBuilder = uiBuilder;
            _panelStack = panelStack;

            OpenOptionsDelegate = OpenOptionsPanel;
        }

        private void OpenOptionsPanel()
        {
            _panelStack.HideAndPush(this);
        }

        /// <summary>
        /// Create the Options Panel
        /// </summary>
        /// <returns></returns>
        public VisualElement GetPanel()
        {
            UIBoxBuilder boxBuilder = _uiBuilder.CreateBoxBuilder()
                .SetHeight(new Length(600, Pixel))
                .SetWidth(new Length(600, Pixel))
                .ModifyScrollView(builder => builder.SetName("elementPreview"));

            VisualElementBuilder menuContent = _uiBuilder.CreateComponentBuilder().CreateVisualElement();
            AddOptionsTitle(menuContent);
            UIRegister.UpdateCallers(menuContent);
            boxBuilder.AddComponent(menuContent.Build());

            var loc = DependencyContainer.GetInstance<ILoc>();
            string menuTitle = loc.T($"{LocConfig.LocTag}.menu.title");

            VisualElement root = boxBuilder.AddCloseButton("CloseButton").SetBoxInCenter().AddHeader(null,
                $"{menuTitle} v" + MyPluginInfo.PLUGIN_VERSION).BuildAndInitialize();
            root.Q<Button>(name: "CloseButton").clicked += OnUICancelled;
            UIRegister.UpdateCallers(root);
            return root;
        }

        /// <summary>
        /// Adds an option title to the menu
        /// </summary>
        /// <returns></returns>
        private static void AddOptionsTitle(VisualElementBuilder menuContent)
        {
            menuContent.AddPreset(factory => factory.Labels().DefaultHeader(
                $"{LocConfig.LocTag}.menu.options",
                builder: builder => builder.SetStyle(
                    style =>
                    {
                        style.alignSelf = Align.Center; style.marginBottom = new Length(10, Pixel);
                    })));
        }

        public bool OnUIConfirmed()
        {
            return false;
        }

        public void OnUICancelled()
        {
            _panelStack.Pop(this);
        }
    }
}