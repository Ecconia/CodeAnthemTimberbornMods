using System;
using TimberApi.UiBuilderSystem.ElementSystem;

namespace TB_CameraTweaker.KsHelperLib.UI.ConfigBoundElements
{
    internal interface IConfigUIElement<T>
    {
        public void Build(VisualElementBuilder parent);

        public event Action<T> ValueChanged;

        public T CurrentValue { get; }
    }
}