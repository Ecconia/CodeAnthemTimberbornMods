using TB_CameraTweaker.KsHelperLib.Patches;

namespace TB_CameraTweaker.KsHelperLib.UI.Base
{
    /// <summary>
    /// Class to implement menu UI logic with <see cref="IUpdateValue{T}"/> patcher
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal abstract class UIMenuPatcherOnly<T> : UIMenuElement
    {
        protected IUpdateValue<T> _patcher; // associated patcher

        public UIMenuPatcherOnly(IUpdateValue<T> patcher) {
            _patcher = patcher;
        }
    }
}