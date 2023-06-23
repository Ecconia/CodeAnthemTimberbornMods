using System.Collections.Generic;

namespace TB_CameraTweaker.KsHelperLib.Patches
{
    internal class PatcherGenericValue<T> : IUpdateValue<T>
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