using System.Collections.Generic;
using TB_CameraTweaks.KsHelperLib.Localization;

namespace TB_CameraTweaks.Lang
{
    internal class Lang_enUS : ILanguage
    {
        public string Tag => "enUS";

        public IEnumerable<LocEntryModel> GetEntries()
        {
            return new List<LocEntryModel>()
            {
                new LocEntryModel("menu.title", "Camera Tweaker", "Title of menu" ),
                new LocEntryModel("menu.options", "Options", "Options header of menu" ),
                new LocEntryModel("menu.zoomfactor", "Zoom Factor", "Header of option: Zoom Factor" ),
                new LocEntryModel("menu.VerticalLimiter", "Disable Automatic Camera Snap Back", "Header of option: Vertical Angel Limiter" ),
                new LocEntryModel("menu.fov", "Field Of View", "Header of option: FOV" ),
                new LocEntryModel("single.default", "Default" ),
                new LocEntryModel("single.original", "Original" ),
                new LocEntryModel("single.on", "On" ),
                new LocEntryModel("single.off", "Off" ),
            };
        }
    }
}