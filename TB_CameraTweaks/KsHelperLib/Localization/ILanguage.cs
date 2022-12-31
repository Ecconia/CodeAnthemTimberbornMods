using System.Collections.Generic;

namespace TB_CameraTweaks.KsHelperLib.Localization
{
    internal interface ILanguage
    {
        string Tag { get; }

        IEnumerable<LocEntryModel> GetEntries();
    }
}