using TB_CameraTweaker.KsHelperLib.UI.Menu;
using TimberApi.DependencyContainerSystem;
using TimberApi.UiBuilderSystem.ElementSystem;
using Timberborn.Localization;
using Timberborn.SingletonSystem;

namespace TB_CameraTweaker.KsHelperLib.UI.Base
{
    /// <summary>
    /// Class to implement pure menu UI logic with nothing else"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal abstract class UIMenuElement : ILoadableSingleton
    {
        protected readonly ILoc _loc;
        private readonly OptionsMenu _optionsMenu;
        protected int _uiPriorityOrder = 0;

        public UIMenuElement() {
            _loc = DependencyContainer.GetInstance<ILoc>();
            _optionsMenu = DependencyContainer.GetInstance<OptionsMenu>();
        }

        protected void UpdateRequired() => _optionsMenu.RequireUpdate();

        public virtual void Load() {
            _optionsMenu.RegisterFeature(GenerateUIContent, _uiPriorityOrder);
        }

        protected abstract void GenerateUIContent(VisualElementBuilder builder);
    }
}