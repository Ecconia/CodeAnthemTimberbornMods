using Newtonsoft.Json;
using Timberborn.CameraSystem;
using UnityEngine;

namespace TB_CameraTweaker.Models
{
    internal class CameraPositionInfo
    {
        [JsonIgnore]
        public CameraState CameraState;
        public float Fov { get; set; }
        public string Name { get; set; }

        public float HorizontalAngle => CameraState.HorizontalAngle;

        public Vector3 Target => CameraState.Target;

        public float VerticalAngle => CameraState.VerticalAngle;

        public float ZoomLevel => CameraState.ZoomLevel;

        [JsonConstructor]
        public CameraPositionInfo(string name, Vector3 target, float zoomLevel, float horizontalAngle, float verticalAngle, float fov)
            : this(name, new CameraState(target, zoomLevel, horizontalAngle, verticalAngle), fov) { }

        public CameraPositionInfo(string name, CameraState cameraState, float fov) {
            Name = name;
            CameraState = cameraState;
            Fov = fov;
        }

        public CameraPositionInfo(CameraPositionInfo toCopy) : this(toCopy.Name, toCopy) { }

        public CameraPositionInfo(string name, CameraPositionInfo toCopy) : this(
            name: name,
            cameraState: toCopy.CameraState,
            fov: toCopy.Fov) { }

        public override string ToString() {
            return $"Camera Position Info: Name: {Name}\n" +
                //$"Target: {Target}\n" +
                //$"ZoomLevel: {ZoomLevel}\n" +
                //$"HorizontalAngle: {HorizontalAngle}\n" +
                //$"VerticalAngle: {VerticalAngle}\n" +
                $"Fov: {Fov}\n" +
                $"CameraState: {CameraState}\n";
        }

        public bool IsSamePosition(CameraPositionInfo instance) {
            bool isEqual = true;
            if (Target != instance.Target) isEqual = false;
            if (ZoomLevel != instance.ZoomLevel) isEqual = false;
            if (HorizontalAngle != instance.HorizontalAngle) isEqual = false;
            if (VerticalAngle != instance.VerticalAngle) isEqual = false;
            if (Fov != instance.Fov) isEqual = false;
            return isEqual;
        }
    }
}