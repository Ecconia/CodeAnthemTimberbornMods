using System.Collections.Generic;

namespace TB_CameraTweaker.KsHelperLib.DataSaver
{
    internal interface IDataSaver<T> where T : class
    {
        IEnumerable<T> Load();

        bool Save(List<T> objectsToSave);
    }
}