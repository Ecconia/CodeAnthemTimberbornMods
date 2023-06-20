using System.Collections.Generic;
using TB_CameraTweaker.CameraSaveSystem;

namespace TB_CameraTweaker.CameraPositions.Store
{
    internal interface ICameraPositionStore
    {
        List<CameraPositionInfo> SavedCameraPositions { get; }

        bool GetCameraPosition(string cameraName, out CameraPositionInfo cameraPositionInfo);

        bool AddCameraPosition(CameraPositionInfo cameraPositionInfo);

        bool RemoveCameraPosition(CameraPositionInfo cameraPositionInfo);

        bool RemoveCameraPosition(string cameraName);
    }
}