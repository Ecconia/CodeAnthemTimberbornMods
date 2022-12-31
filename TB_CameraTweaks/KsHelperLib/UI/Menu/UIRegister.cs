using System;
using TimberApi.UiBuilderSystem.ElementSystem;
using UnityEngine.UIElements;

namespace TB_CameraTweaks.KsHelperLib.UI.Menu
{
    internal static class UIRegister
    {
        // a sad excuse of a class, cause OptionsMenu doesn't exist, before patchers are called

        public static event Action<VisualElement> AddUiLogic;

        public static event Action<VisualElementBuilder> AddUiElements;

        public static void UpdateCallers(VisualElementBuilder builder) => AddUiElements?.Invoke(builder);

        public static void UpdateCallers(VisualElement root) => AddUiLogic?.Invoke(root);
    }
}