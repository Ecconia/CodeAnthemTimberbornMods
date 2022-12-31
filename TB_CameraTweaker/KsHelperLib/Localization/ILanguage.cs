using System.Collections.Generic;

namespace TB_CameraTweaker.KsHelperLib.Localization
{
    internal interface ILanguage
    {
        string Tag { get; }

        IEnumerable<LocEntryModel> GetEntries();
    }
}