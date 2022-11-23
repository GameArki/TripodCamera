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
        internal void Push_In(float value) {
            currentInfo.Push_In(value);
        }

        // - Move
        internal void Move(Vector2 value) {
            currentInfo.Move(value);
        }

        internal void Move_AndChangeLookAtOffset(Vector2 value) {
            currentInfo.Move_AndChangeLookAtOffset(value);
        }

        // - Rotate
        internal void Rotate_Horizontal(float x) {
            currentInfo.Rotate_Horizontal(x);
        }

        internal void Rotate_Vertical(float y) {
            currentInfo.Rotate_Vertical(y);
        }

        internal void Rotate_Roll(float z) {
            currentInfo.Rotate_Roll(z);
        }

        // - Zoom
        internal void ZoomIn(float value, float min, float max) {
            currentInfo.Zoom_In(value, min, max);
        }

        // ==== Advance ====
        // - Follow
        internal void Follow_SetInit(Transform target, Vector3 offset) {
            currentInfo.Follow_SetInit(target, offset);
        }

        internal void Follow_ChangeTarget(Transform target) {
            currentInfo.Follow_ChangeTarget(target);
        }

        internal void Follow_ChangeOffset(Vector3 offset) {
            currentInfo.Follow_ChangeOffset(offset);
        }

        internal void Follow_Apply() {
            currentInfo.Follow_Apply();
        }

        // - LookAt
        internal void LookAt_SetInit(Transform target, Vector3 offset) {
            currentInfo.LookAt_SetInit(target, offset);
        }

        internal void LookAt_ChangeTarget(Transform target) {
            currentInfo.LookAt_ChangeTarget(target);
        }

        internal void LookAt_ChangeOffset(Vector3 offset) {
            currentInfo.LookAt_ChangeOffset(offset);
        }

        internal void ApplyLookAt() {
            currentInfo.LookAt_Apply();
        }
        
    }

}