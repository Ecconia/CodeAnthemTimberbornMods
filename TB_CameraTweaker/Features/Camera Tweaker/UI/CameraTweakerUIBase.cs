using TB_CameraTweaker.KsHelperLib.UI.Menu;
using TimberApi.DependencyContainerSystem;
using TimberApi.UiBuilderSystem.ElementSystem;
using Timberborn.Localization;
using Timberborn.SingletonSystem;

namespace TB_CameraTweaker.KsHelperLib.UI.Elements
{
    internal abstract class CameraTweakerUIBase<T> : ILoadableSingleton
    {
        protected IConfigUIElement<T> _uiElement; // UI Element used like slider, checkbox etc
        protected IUpdateValue<T> _patcher; // associated patcher
        protected ILoc _loc;
        private readonly OptionsMenu _optionsMenu;

        public CameraTweakerUIBase(IUpdateValue<T> patcher) {
            _loc = DependencyContainer.GetInstance<ILoc>();
            _optionsMenu = DependencyContainer.GetInstance<OptionsMenu>();
            _patcher = patcher;
        }

        public virtual void Load() {
            _uiElement = GenerateUIElement();
            _uiElement.ValueChanged -= OnValueChanged;
            _uiElement.ValueChanged += OnValueChanged;
            _optionsMenu.UpdateUIContent -= UpdateUIContent;
            _optionsMenu.UpdateUIContent += UpdateUIContent;
        }

        protected virtual void OnValueChanged(T newValue) {
            _patcher.ChangeValue(newValue);
        }

        private void UpdateUIContent(VisualElementBuilder builder) => _uiElement.Build(builder);

        public void UseConfigValue() => _patcher.ChangeValue(_uiElement.CurrentValue);

        protected abstract IConfigUIElement<T> GenerateUIElement();
    }
}