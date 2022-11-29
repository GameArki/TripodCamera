using System;
using UnityEngine;
using JackEasing;

namespace TripodCamera {

    public class TCCameraEntity {

        // ==== Info ====
        TCCameraInfoComponent defaultInfo;

        TCCameraInfoComponent currentInfo;
        public TCCameraInfoComponent CurrentInfo => currentInfo;

        TCCameraEffectComponent effect;
        public TCCameraEffectComponent Effect => effect;

        TCCameraFollowComponent followComponent;
        public TCCameraFollowComponent FollowComponent => followComponent;

        TCCameraLookAtComponent lookAtComponent;
        public TCCameraLookAtComponent LookAtComponent => lookAtComponent;

        public TCCameraEntity() {
            this.defaultInfo = new TCCameraInfoComponent();
            this.currentInfo = new TCCameraInfoComponent();
            this.followComponent = new TCCameraFollowComponent();
            this.lookAtComponent = new TCCameraLookAtComponent();
            this.effect = new TCCameraEffectComponent();
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

            Vector3 fwd;
            var pos = currentInfo.Pos;
            var rot = currentInfo.Rot;
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
                currentInfo.SetPos(pos);
            }

        }

        // - Move
        public void Move(Vector2 value) {
            var pos = currentInfo.Pos;
            var rot = currentInfo.Rot;
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

            currentInfo.SetPos(pos);

            // - Follow Component
            followComponent.OffsetAdd(right + up);

        }

        public void Move_AndChangeLookAtOffset(Vector2 value) {
            var pos = currentInfo.Pos;
            var rot = currentInfo.Rot;
            Vector3 up;
            Vector3 right;
            up = rot * Vector3.up;
            right = rot * Vector3.right;
            up.Normalize();
            right.Normalize();
            right = right * value.x;
            up = up * value.y;
            pos += right + up;

            currentInfo.SetPos(pos);

            // - LookAt Component
            lookAtComponent.OffsetAdd(right + up);

        }

        // - Rotate
        public void Rotate_Horizontal(float x) {
            currentInfo.Rotate_Horizontal(x);
            lookAtComponent.Rotate_Horizontal(-x);
        }

        public void Rotate_Vertical(float y) {
            currentInfo.Rotate_Vertical(y);
            lookAtComponent.Rotate_Vertical(y);
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