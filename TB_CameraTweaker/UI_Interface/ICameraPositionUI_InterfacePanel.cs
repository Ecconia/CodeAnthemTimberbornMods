using System;
using UnityEngine.UIElements;

namespace TB_CameraTweaker.UI_Interface
{
    internal interface ICameraPositionUI_InterfacePanel
    {
        Button CameraAddButton { get; }
        Button CameraFreezeButton { get; }
        Button CameraJumpButton { get; }
        Button CameraNextButton { get; }
        Button CameraPreviousButton { get; }
        Button CameraRemoveButton { get; }

        void GetNamePopup(string locKey, Action<string> callback);
    }
}