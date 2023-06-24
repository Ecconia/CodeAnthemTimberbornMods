using System.Collections.Generic;
using TB_CameraTweaker.KsHelperLib.Patches;

namespace TB_CameraTweaker.KsHelperLib.BaseHelpers
{
    internal class GenericValue<T> : IUpdateValue<T>
    {
        public bool IsDirty { get; protected set; }
        public T NewValue { get; private set; }

        public void ChangeValue(T newValue)
        {
            if (EqualityComparer<T>.Default.Equals(NewValue, newValue)) return;
            NewValue = newValue;
            IsDirty = true;
        }
    }
}