using TB_CameraTweaker.KsHelperLib.UI.ConfigBoundElements;
using TimberApi.UiBuilderSystem.ElementSystem;

namespace TB_CameraTweaker.KsHelperLib.UI.Base
{
    /// <summary>
    /// Class to implement menu UI logic with <see cref="IConfigUIElement{T}"/> config
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal abstract class UIMenuConfigOnly<T> : UIMenuElement
    {
        protected IConfigUIElement<T> _configEntryUIElement; // UI Element used like slider, checkbox etc

        protected abstract IConfigUIElement<T> GenerateConfigEntry();

        public override void Load() {
            base.Load();
            _configEntryUIElement = GenerateConfigEntry();
            _configEntryUIElement.ValueChanged -= OnValueChanged;
            _configEntryUIElement.ValueChanged += OnValueChanged;
        }

        protected override void GenerateUIContent(VisualElementBuilder builder) {
            _configEntryUIElement.Build(builder);
        }

        protected abstract void OnValueChanged(T newValue);
    }
}