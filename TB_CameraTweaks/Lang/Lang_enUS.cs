using System.Collections.Generic;
using TB_CameraTweaks.KsHelperLib.Localization;

namespace TB_CameraTweaks.Lang
{
    internal class Lang_enUS : ILanguage
    {
        public string Tag => "enUS";

        public IEnumerable<TocEntryModel> GetEntries()
        {
            return new List<TocEntryModel>()
            {
                new TocEntryModel("menu.title", "Camera Tweaker", "Title of menu" ),
                new TocEntryModel("menu.options", "Options", "Options header of menu" ),
                new TocEntryModel("menu.zoomfactor", "Zoom Factor", "Header of option: Zoom Factor" ),
                new TocEntryModel("menu.VerticalLimiter", "Disable Automatic Camera Snap Back", "Header of option: Vertical Angel Limiter" ),
                new TocEntryModel("menu.fov", "Field Of View", "Header of option: FOV" ),
                new TocEntryModel("single.default", "Default" ),
                new TocEntryModel("single.on", "On" ),
                new TocEntryModel("single.off", "Off" ),
            };
        }
    }
}