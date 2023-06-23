using System.Collections.Generic;
using TB_CameraTweaker.KsHelperLib.Localization;

namespace TB_CameraTweaker.Lang
{
    internal class Lang_enUS : ILanguage
    {
        public string Tag => "enUS";

        public IEnumerable<LocEntryModel> GetEntries() {
            return new List<LocEntryModel>()
            {
                // menu itself
                new LocEntryModel("menu.title", "Camera Tweaker", "Title of menu" ),
                new LocEntryModel("menu.options", "Options", "Options header of menu" ),

                // menu tweaks part
                new LocEntryModel("menu.tweakstitle", "Camera Tweaks", "Sub title of camera tweaks" ),
                new LocEntryModel("menu.zoomfactor", "Zoom Factor", "Header of option: Zoom Factor" ),
                new LocEntryModel("menu.zoomspeed", "Zoom Speed", "Header of option: Zoom Speed" ),
                new LocEntryModel("menu.VerticalLimiter", "Disable Automatic Camera Snap Back", "Header of option: Vertical Angel Limiter" ),
                new LocEntryModel("menu.fov", "Field Of View", "Header of option: FOV" ),

                // menu position part
                new LocEntryModel("menu.positiontitle", "Camera Position Manager", "Sub title of camera tweaks" ),
                new LocEntryModel("menu.freeze", "Freeze Camera", "Header of option: Freeze" ),

                // menu global keywords
                new LocEntryModel("single.default", "Default" ),
                new LocEntryModel("single.original", "Original" ),
                new LocEntryModel("single.on", "On" ),
                new LocEntryModel("single.off", "Off" ),
            };
        }
    }
}