using UnityEngine;
using GameArki.FPEasing;

namespace GameArki.TripodCamera.Entities {

    public class TCCameraEntity {

        int id;
        public int ID => id;
        public void SetID(int value) => id = value;

        // ==== Info ====
        TCCameraInfoComponent savedInfoComponent;

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

        TCCameraRoundStateComponent roundStateComponent;
        public TCCameraRoundStateComponent RoundStateComponent => roundStateComponent;

        // - Rotate State
        TCCameraRotateStateComponent rotateStateComponent;
        public TCCameraRotateStateComponent RotateStateComponent => rotateStateComponent;

        // - Push State
        TCCameraPushStateComponent pushStateComponent;
        public TCCameraPushStateComponent PushStateComponent => pushStateComponent;

        public TCCameraEntity() {
            this.savedInfoComponent = new TCCameraInfoComponent();
            this.currentInfoComponent = new TCCameraInfoComponent();
            this.followComponent = new TCCameraFollowComponent();
            this.lookAtComponent = new TCCameraLookAtComponent();

            this.trackComponent = new TCCameraTrackComponent();
            this.shakeStateComponent = new TCCameraShakeStateComponent();
            this.movementStateComponent = new TCCameraMovementStateComponent();
            this.roundStateComponent = new TCCameraRoundStateComponent();
            this.rotateStateComponent = new TCCameraRotateStateComponent();
            this.pushStateComponent = new TCCameraPushStateComponent();
        }

        // ==== Info ====
        public void InitInfo(Vector3 pos, Quaternion rot, float fov) {
            savedInfoComponent.Init(pos, rot, fov);
            currentInfoComponent.Init(pos, rot, fov);
        }

        public void SaveAsDefault() {
            // Save pos, rot, fov
            savedInfoComponent.CloneFrom(currentInfoComponent);
        }

        public void RestoreByDefault() {
            // Restore pos, rot, fov
            currentInfoComponent.CloneFrom(savedInfoComponent);
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

            if (followComponent.IsFollowing()) {
                followComponent.OffsetAdd(right + up);
            } else {
                currentInfoComponent.SetPos(pos);
            }
        }

        public void Round(Vector2 roundOffset, Transform tar) {
            var offset = GetRoundOffset(roundOffset, tar);
            followComponent.OffsetAdd(offset);
        }

        public Vector3 GetRoundOffset(Vector2 roundOffset, Transform tar) {
            var pos = currentInfoComponent.Pos;
            Vector3 dir = pos - tar.position;
            float length = dir.magnitude;
            dir.Normalize();

            // Rotate Dir
            float angleX = -roundOffset.x;
            float angleY = roundOffset.y;
            Quaternion rotX = Quaternion.AngleAxis(angleX, Vector3.up);
            Quaternion rotY = Quaternion.AngleAxis(angleY, Vector3.right);
            dir = rotX * rotY * dir;

            return (tar.position + dir * length) - pos;
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
        public void Follow_SetInit(Transform target,
                                   Vector3 offset,
                                   EasingType easingType_horizontal,
                                   float easingTime_horizontal,
                                   EasingType easingType_vertical,
                                   float easingTime_vertical) {
            followComponent.SetInit(target, offset);
            followComponent.SetEasing(easingType_horizontal, easingTime_horizontal, easingType_vertical, easingTime_vertical);
        }

        public void Follow_ChangeTarget(Transform target) {
            followComponent.ChangeTarget(target);
        }

        public void Follow_ChangeXEasing(EasingType easingType, float easingTime) {
            followComponent.ChangeXEasing(easingType, easingTime);
        }

        public void Follow_ChangeYEasing(EasingType easingType, float easingTime) {
            followComponent.ChangeYEasing(easingType, easingTime);
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