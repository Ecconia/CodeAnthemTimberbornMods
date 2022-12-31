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
            };
        }
    }
}