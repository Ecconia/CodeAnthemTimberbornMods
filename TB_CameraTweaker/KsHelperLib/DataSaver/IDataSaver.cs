using System.Collections.Generic;

namespace TB_CameraTweaker.KsHelperLib.DataSaver
{
    internal interface IDataSaver<T>
    {
        IEnumerable<T> Load();

        bool Save(List<T> objectsToSave);
    }
}