using System;
using System.Collections.Generic;
using TB_CameraTweaker.KsHelperLib.Logger;
using TB_CameraTweaker.KsHelperLib.UI.Menu;
using TimberApi.DependencyContainerSystem;
using TimberApi.UiBuilderSystem.ElementSystem;
using Timberborn.Localization;
using Timberborn.SingletonSystem;

namespace TB_CameraTweaker.Patchers
{
    internal abstract class PatcherBase<TType> : ILoadableSingleton
    {
        //public static List<PatcherBase<TType>> Patchers { get; set; }

        public Action _valueChanged;
        protected ILoc _loc;
        protected LogProxy _log;
        private TType _newValue;

        public PatcherBase(string methodName) {
            _log = new($"[Patcher: {methodName}] ");
            _loc = DependencyContainer.GetInstance<ILoc>();
            UIRegister.AddUiElements -= AddUIElements;
            UIRegister.AddUiElements += AddUIElements;
            //Patchers.Add(this);
        }

        public bool IsDirty { get; protected set; }

        public TType NewValue {
            get => _newValue;
            protected set => ChangeValue(value);
        }

        public void Load() {
            SetupPatcher();
        }

        internal void ChangeValue(TType newValue) {
            if (EqualityComparer<TType>.Default.Equals(_newValue, newValue)) return;
            _newValue = newValue;
            IsDirty = true;
            _valueChanged?.Invoke();
        }

        protected abstract void AddUIElements(VisualElementBuilder builder);

        //[HarmonyPostfix]
        //public static void Postfix(CameraComponent __instance) {
        //}

        protected abstract void SetupPatcher();
    }
}