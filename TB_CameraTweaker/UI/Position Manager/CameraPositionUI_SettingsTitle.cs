using TB_CameraTweaker.KsHelperLib.Localization;
using TB_CameraTweaker.KsHelperLib.UI.Base;
using TB_CameraTweaker.KsHelperLib.UI.Elements;
using TimberApi.UiBuilderSystem.ElementSystem;

namespace TB_CameraTweaker.UI.Position_Manager
{
    internal class CameraPositionUI_SettingsTitle : UIMenuElement
    {
        public override void Load() {
            _uiPriorityOrder = 100;
            base.Load();
        }

        protected override void GenerateUIContent(VisualElementBuilder builder) {
            StaticLabels.Spacer(builder, 5);
            StaticLabels.MenuSubTitle(builder, $"{LocConfig.LocTag}.menu.positiontitle");
        }
    }
}