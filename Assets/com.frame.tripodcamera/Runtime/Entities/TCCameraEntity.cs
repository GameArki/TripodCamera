using System;
using UnityEngine;
using JackEasing;

namespace TripodCamera {

    public class TCCameraEntity {

        // ==== Info ====
        TCCameraInfoComponent defaultInfoComponent;

        TCCameraInfoComponent currentInfoComponent;
        public TCCameraInfoComponent CurrentInfoComponent => currentInfoComponent;

        // - Follow
        TCCameraFollowComponent followComponent;
        public TCCameraFollowComponent FollowComponent => followComponent;

        // - LookAt
        TCCameraLookAtComponent lookAtComponent;
        public TCCameraLookAtComponent LookAtComponent => lookAtComponent;

        // ==== State ====
        // - Track State
        TCCameraTrackComponent trackComponent;
        public TCCameraTrackComponent TrackComponent => trackComponent;

        // - Shake State
        TCCameraShakeStateComponent shakeStateComponent;
        public TCCameraShakeStateComponent ShakeComponent => shakeStateComponent;

        // - Move State
        TCCameraMovementStateComponent movementStateComponent;
        public TCCameraMovementStateComponent MovementStateComponent => movementStateComponent;

        // - Rotate State

        public TCCameraEntity() {
            this.defaultInfoComponent = new TCCameraInfoComponent();
            this.currentInfoComponent = new TCCameraInfoComponent();
            this.followComponent = new TCCameraFollowComponent();
            this.lookAtComponent = new TCCameraLookAtComponent();

            this.trackComponent = new TCCameraTrackComponent();
            this.shakeStateComponent = new TCCameraShakeStateComponent();
            this.movementStateComponent = new TCCameraMovementStateComponent();
        }

        // ==== Info ====
        public void InitInfo(Vector3 pos, Quaternion rot, float fov) {
            defaultInfoComponent.Init(pos, rot, fov);
            currentInfoComponent.Init(pos, rot, fov);
        }

        public void SaveAsDefault() {
            // Save pos, rot, fov
            defaultInfoComponent.CloneFrom(currentInfoComponent);
        }

        public void RestoreByDefault() {
            // Restore pos, rot, fov
            currentInfoComponent.CloneFrom(defaultInfoComponent);
        }

        // ==== Basic ====
        // - Push
        public void Push_In(float value) {

            Vector3 fwd;
            var pos = currentInfoComponent.Pos;
            var rot = currentInfoComponent.Rot;
            var lookAtTF = lookAtComponent.LookAtTF;
            if (lookAtTF != null) {
                fwd = lookAtTF.position - pos;
            } else {
                fwd = rot * Vector3.forward;
            }
            fwd.Normalize();

            if (followComponent.IsFollowing()) {
                followComponent.PushIn(fwd, value);
            } else {
                pos += fwd * value;
                currentInfoComponent.SetPos(pos);
            }

        }

        // - Move
        public void Move(Vector2 value) {
            var pos = currentInfoComponent.Pos;
            var rot = currentInfoComponent.Rot;
            Vector3 up = rot * Vector3.up;
            Vector3 right = rot * Vector3.right;
            up.Normalize();
            right.Normalize();
            up = up * value.y;
            right = right * value.x;
            pos += right + up;

            var lookAtTF = lookAtComponent.LookAtTF;
            if (lookAtTF != null) {

                Vector3 dir = pos - lookAtTF.position;
                float length = dir.magnitude;
                dir.Normalize();

                // Rotate Dir
                float angleX = -value.x;
                float angleY = value.y;
                Quaternion rotX = Quaternion.AngleAxis(angleX, Vector3.up);
                Quaternion rotY = Quaternion.AngleAxis(angleY, Vector3.right);
                dir = rotX * rotY * dir;

                // Move
                pos = lookAtTF.position + dir * length;

            }

            currentInfoComponent.SetPos(pos);

            // - Follow Component
            followComponent.OffsetAdd(right + up);

        }

        public void Move_AndChangeLookAtOffset(Vector2 value) {
            var pos = currentInfoComponent.Pos;
            var rot = currentInfoComponent.Rot;
            Vector3 up;
            Vector3 right;
            up = rot * Vector3.up;
            right = rot * Vector3.right;
            up.Normalize();
            right.Normalize();
            right = right * value.x;
            up = up * value.y;
            pos += right + up;

            currentInfoComponent.SetPos(pos);

            // - LookAt Component
            lookAtComponent.OffsetAdd(right + up);

        }

        // - Rotate
        public void Rotate_Horizontal(float x) {
            currentInfoComponent.Rotate_Horizontal(x);
            lookAtComponent.Rotate_Horizontal(-x);
        }

        public void Rotate_Vertical(float y) {
            currentInfoComponent.Rotate_Vertical(y);
            lookAtComponent.Rotate_Vertical(y);
        }

        public void Rotate_Roll(float z) {
            currentInfoComponent.Rotate_Roll(z);
        }

        // - Zoom
        public void ZoomIn(float value, float min, float max) {
            currentInfoComponent.Zoom_In(value, min, max);
        }

        // ==== Advance ====
        // - Follow
        public void Follow_SetInit(Transform target, Vector3 offset, EasingType easingType, float easingTime) {
            followComponent.SetInit(target, offset);
            followComponent.SetEasing(easingType, easingTime);
        }

        public void Follow_ChangeTarget(Transform target) {
            followComponent.ChangeTarget(target);
        }

        public void Follow_ChangeOffset(Vector3 offset) {
            followComponent.ChangeOffset(offset);
        }

        public bool IsFollowing() {
            return followComponent.IsFollowing();
        }

        public bool IsLookingAt() {
            return lookAtComponent.IsLookingAt();
        }

        // - LookAt
        public void LookAt_SetInit(Transform target, Vector3 offset) {
            lookAtComponent.LookAt_SetInit(target, offset);
        }

        public void LookAt_ChangeTarget(Transform target) {
            lookAtComponent.LookAt_ChangeTarget(target);
        }

        public void LookAt_ChangeOffset(Vector3 offset) {
            lookAtComponent.LookAt_ChangeOffset(offset);
        }

    }

}