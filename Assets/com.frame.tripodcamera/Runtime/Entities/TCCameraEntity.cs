using System;
using UnityEngine;

namespace TripodCamera {

    public class TCCameraEntity {

        // ==== Info ====
        TCCameraInfoComponent defaultInfo;

        TCCameraInfoComponent currentInfo;
        public TCCameraInfoComponent CurrentInfo => currentInfo;

        public TCCameraEntity() {
            this.defaultInfo = new TCCameraInfoComponent();
            this.currentInfo = new TCCameraInfoComponent();
        }

        // ==== Info ====
        public void InitInfo(Vector3 pos, Quaternion rot, float fov) {
            defaultInfo.Init(pos, rot, fov);
            currentInfo.Init(pos, rot, fov);
        }

        public void SaveAsDefault() {
            // Save pos, rot, fov
            defaultInfo.CloneFrom(currentInfo);
        }

        public void RestoreByDefault() {
            // Restore pos, rot, fov
            currentInfo.CloneFrom(defaultInfo);
        }

        // ==== Basic ====
        // - Push
        internal void PushIn(float value) {
            currentInfo.PushIn(value);
        }

        // - Move
        internal void Move(Vector2 value) {
            currentInfo.Move(value);
        }

        // - Rotate
        internal void RotateHorizontal(float x) {
            currentInfo.RotateHorizontal(x);
        }

        internal void RotateVertical(float y, float min, float max) {
            currentInfo.RotateVertical(y, min, max);
        }

        // - Zoom
        internal void ZoomIn(float value, float min, float max) {
            currentInfo.ZoomIn(value, min, max);
        }

    }

}