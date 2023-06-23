using TB_CameraTweaker.KsHelperLib.Localization;
using TB_CameraTweaker.KsHelperLib.UI.Base;
using TB_CameraTweaker.KsHelperLib.UI.Elements;
using TimberApi.UiBuilderSystem.ElementSystem;

namespace TB_CameraTweaker.UI.Tweaks
{
    internal class CameraTweakerUI_SettingsTitle : UIMenuElement
    {
        public override void Load() {
            _uiPriorityOrder = 0;
            base.Load();
        }

        protected override void GenerateUIContent(VisualElementBuilder builder) {
            StaticLabels.MenuSubTitle(builder, $"{LocConfig.LocTag}.menu.tweakstitle");
        }
    }
}