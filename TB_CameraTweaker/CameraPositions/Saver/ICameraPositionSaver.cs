using System.Collections.Generic;
using TB_CameraTweaker.CameraSaveSystem;

namespace TB_CameraTweaker.CameraPositions.Saver
{
    internal interface ICameraPositionSaver
    {
        IEnumerable<CameraPositionInfo> Load();

        bool Save(List<CameraPositionInfo> cameraPositionInfos);
    }
}