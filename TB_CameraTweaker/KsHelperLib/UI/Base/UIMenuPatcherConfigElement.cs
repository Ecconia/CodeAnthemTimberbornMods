using TB_CameraTweaker.KsHelperLib.Patches;
using TB_CameraTweaker.KsHelperLib.UI.ConfigBoundElements;
using TimberApi.UiBuilderSystem.ElementSystem;

namespace TB_CameraTweaker.KsHelperLib.UI.Base
{
    /// <summary>
    /// Class to implement menu UI logic with <see cref="IUpdateValue{T}"/> patcher and <see cref="IConfigUIElement{T}"/> config
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal abstract class UIMenuPatcherConfigElement<T> : UIMenuPatcherOnly<T>
    {
        protected IConfigUIElement<T> _configEntryUIElement; // UI Element used like slider, checkbox etc

        protected UIMenuPatcherConfigElement(IUpdateValue<T> patcher) : base(patcher) { }

        protected abstract IConfigUIElement<T> GenerateConfigEntry();

        public void UseConfigValue() => _patcher.ChangeValue(_configEntryUIElement.CurrentValue);

        public override void Load() {
            base.Load();
            _configEntryUIElement = GenerateConfigEntry();
            _configEntryUIElement.ValueChanged -= OnValueChanged;
            _configEntryUIElement.ValueChanged += OnValueChanged;
        }

        protected override void GenerateUIContent(VisualElementBuilder builder) {
            _configEntryUIElement.Build(builder);
        }

        protected virtual void OnValueChanged(T newValue) {
            _patcher.ChangeValue(newValue);
        }
    }
}