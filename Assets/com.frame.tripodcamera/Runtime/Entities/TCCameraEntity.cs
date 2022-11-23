using System;
using UnityEngine;

namespace TripodCamera {

    public class TCCameraEntity {

        // ==== Info ====
        TCCameraInfoComponent defaultInfo;

        TCCameraInfoComponent currentInfo;
        public TCCameraInfoComponent CurrentInfo => currentInfo;

        TCCameraFSMComponent fsm;
        public TCCameraFSMComponent FSM => fsm;

        public TCCameraEntity() {
            this.defaultInfo = new TCCameraInfoComponent();
            this.currentInfo = new TCCameraInfoComponent();
            this.fsm = new TCCameraFSMComponent();
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
        public void Push_In(float value) {
            currentInfo.Push_In(value);
        }

        // - Move
        public void Move(Vector2 value) {
            currentInfo.Move(value);
        }

        public void Move_AndChangeLookAtOffset(Vector2 value) {
            currentInfo.Move_AndChangeLookAtOffset(value);
        }

        // - Rotate
        public void Rotate_Horizontal(float x) {
            currentInfo.Rotate_Horizontal(x);
        }

        public void Rotate_Vertical(float y) {
            currentInfo.Rotate_Vertical(y);
        }

        public void Rotate_Roll(float z) {
            currentInfo.Rotate_Roll(z);
        }

        // - Zoom
        public void ZoomIn(float value, float min, float max) {
            currentInfo.Zoom_In(value, min, max);
        }

        // ==== Advance ====
        // - Follow
        public void Follow_SetInit(Transform target, Vector3 offset) {
            currentInfo.Follow_SetInit(target, offset);
        }

        public void Follow_ChangeTarget(Transform target) {
            currentInfo.Follow_ChangeTarget(target);
        }

        public void Follow_ChangeOffset(Vector3 offset) {
            currentInfo.Follow_ChangeOffset(offset);
        }

        public void Follow_Apply() {
            currentInfo.Follow_Apply();
        }

        // - LookAt
        public void LookAt_SetInit(Transform target, Vector3 offset) {
            currentInfo.LookAt_SetInit(target, offset);
        }

        public void LookAt_ChangeTarget(Transform target) {
            currentInfo.LookAt_ChangeTarget(target);
        }

        public void LookAt_ChangeOffset(Vector3 offset) {
            currentInfo.LookAt_ChangeOffset(offset);
        }

        public void ApplyLookAt() {
            currentInfo.LookAt_Apply();
        }
        
    }

}