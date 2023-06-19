using UnityEngine;

namespace TB_CameraTweaker.CameraSaveSystem
{
    internal class CameraPositionInfo
    {
        public CameraPositionInfo(string name, Vector3 target, float zoomLevel, float horizontalAngle, float verticalAngle, float fov) {
            Name = name;
            Target = target;
            ZoomLevel = zoomLevel;
            HorizontalAngle = horizontalAngle;
            VerticalAngle = verticalAngle;
            Fov = fov;
        }

        public string Name { get; set; }
        public Vector3 Target { get; set; }
        public float ZoomLevel { get; set; }
        public float HorizontalAngle { get; set; }
        public float VerticalAngle { get; set; }
        public float Fov { get; set; }
    }
}