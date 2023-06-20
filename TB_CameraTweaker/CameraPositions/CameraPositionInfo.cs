using Newtonsoft.Json;
using UnityEngine;

namespace TB_CameraTweaker.CameraSaveSystem
{
    internal class CameraPositionInfo
    {
        public float Fov { get; set; }

        public float HorizontalAngle { get; set; }

        public string Name { get; set; }

        public Vector3 Target { get; set; }

        public float VerticalAngle { get; set; }

        public float ZoomLevel { get; set; }

        [JsonConstructor]
        public CameraPositionInfo(string name, Vector3 target, float zoomLevel, float horizontalAngle, float verticalAngle, float fov) {
            Name = name;
            Target = target;
            ZoomLevel = zoomLevel;
            HorizontalAngle = horizontalAngle;
            VerticalAngle = verticalAngle;
            Fov = fov;
        }

        public CameraPositionInfo(CameraPositionInfo toCopy) : this(toCopy.Name, toCopy) { }

        public CameraPositionInfo(string name, CameraPositionInfo toCopy) : this(
            name: name,
            target: toCopy.Target,
            zoomLevel: toCopy.ZoomLevel,
            horizontalAngle: toCopy.HorizontalAngle,
            verticalAngle: toCopy.VerticalAngle,
            fov: toCopy.Fov) { }
    }
}