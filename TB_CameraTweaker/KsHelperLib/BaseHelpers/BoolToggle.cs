using System;
using TB_CameraTweaker.KsHelperLib.Patches;

namespace TB_CameraTweaker.KsHelperLib.BaseHelpers
{
    internal abstract class BoolToggle : IUpdateValue<bool>
    {
        public Action<bool> OnValueChanged;

        public bool Toggle() {
            ChangeValue(!CurrentState);
            return CurrentState;
        }

        public bool IsDirty { get; protected set; }
        public bool CurrentState { get; private set; }

        public void ChangeValue(bool newValue) {
            if (CurrentState == newValue) return;
            CurrentState = newValue;
            ValueChanged();
            OnValueChanged?.Invoke(CurrentState);
            IsDirty = true;
        }

        protected abstract void ValueChanged();
    }
}