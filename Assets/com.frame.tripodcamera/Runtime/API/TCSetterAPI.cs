using UnityEngine;
using JackEasing;
using TripodCamera.Facades;
using TripodCamera.Domain;

namespace TripodCamera.API {

    internal class TCSetterAPI : ITCSetterAPI {

        TCFacades facades;
        TCDomain domain;

        internal TCSetterAPI() { }

        internal void Inject(TCFacades facades, TCDomain domain) {
            this.facades = facades;
            this.domain = domain;
        }

        // ==== Spawn ====
        void ITCSetterAPI.SpawnByMain(int id) {
            var cameraDomain = domain.CameraDomain;
            var cam = facades.MainCamera;
            var tf = cam.transform;
            cameraDomain.Spawn(id, tf.position, tf.rotation, cam.fieldOfView);
        }

        void ITCSetterAPI.Spawn(int id, Vector3 position, Quaternion rotation, float fov) {
            var cameraDomain = domain.CameraDomain;
            cameraDomain.Spawn(id, position, rotation, fov);
        }

        void ITCSetterAPI.CutTo(int id, EasingType easingType, float duration) {
            var directorDomain = domain.DirectorDomain;
            directorDomain.CutTo(id, easingType, duration);
        }

        // ==== Basic ====
        void ITCSetterAPI.Push_In_Current(float value) {
            var cameraDomain = domain.CameraDomain;
            cameraDomain.Push_In_Current(value);
        }

        void ITCSetterAPI.Move_Current(Vector2 value) {
            var cameraDomain = domain.CameraDomain;
            cameraDomain.Move_Current(value);
        }

        void ITCSetterAPI.Move_AndChangeLookAtOffset_Current(Vector2 value) {
            var cameraDomain = domain.CameraDomain;
            cameraDomain.Move_AndChangeLookAtOffset_Current(value);
        }

        void ITCSetterAPI.Rotate_Horizontal_Current(float x) {
            var cameraDomain = domain.CameraDomain;
            cameraDomain.Rotate_Horizontal_Current(x);
        }

        void ITCSetterAPI.Rotate_Vertical_Current(float y) {
            var cameraDomain = domain.CameraDomain;
            cameraDomain.Rotate_Vertical_Current(y);
        }

        void ITCSetterAPI.Rotate_Roll_Current(float z) {
            var cameraDomain = domain.CameraDomain;
            cameraDomain.Rotate_Roll_Current(z);
        }

        void ITCSetterAPI.Zoom_In_Current(float value) {
            var cameraDomain = domain.CameraDomain;
            cameraDomain.Zoom_In_Current(value);
        }

        // ==== Advanced ====
        // - Follow
        void ITCSetterAPI.Follow_SetInit_Current(Transform target, Vector3 offset, EasingType easingType, float easingTime) {
            var cameraDomain = domain.CameraDomain;
            cameraDomain.Follow_SetInit_Current(target, offset, easingType, easingTime, easingType, easingTime);
        }

        void ITCSetterAPI.Follow_SetInit_Current(Transform target, Vector3 offset,
        EasingType easingType_horizontal, float easingTime_horizontal,
        EasingType easingType_vertical, float easingTime_vertical) {
            var cameraDomain = domain.CameraDomain;
            cameraDomain.Follow_SetInit_Current(target, offset, easingType_horizontal, easingTime_horizontal, easingType_vertical, easingTime_vertical);
        }

        void ITCSetterAPI.Follow_ChangeTarget_Current(Transform target) {
            var cameraDomain = domain.CameraDomain;
            cameraDomain.Follow_ChangeTarget_Current(target);
        }

        void ITCSetterAPI.Follow_ChangeOffset_Current(Vector3 offset) {
            var cameraDomain = domain.CameraDomain;
            cameraDomain.Follow_ChangeOffset_Current(offset);
        }

        // - LookAt
        void ITCSetterAPI.LookAt_SetInit_Current(Transform target, Vector3 offset) {
            var cameraDomain = domain.CameraDomain;
            cameraDomain.LookAt_SetInit_Current(target, offset);
        }

        void ITCSetterAPI.LookAt_ChangeTarget_Current(Transform target) {
            var cameraDomain = domain.CameraDomain;
            cameraDomain.LookAt_ChangeTarget_Current(target);
        }

        void ITCSetterAPI.LookAt_ChangeOffset_Current(Vector3 offset) {
            var cameraDomain = domain.CameraDomain;
            cameraDomain.LookAt_ChangeOffset_Current(offset);
        }

        // ==== Shake ====
        void ITCSetterAPI.Enter_Shake_Current(TCShakeStateModel[] mods) {
            var cameraDomain = domain.CameraDomain;
            cameraDomain.Enter_Shake_Current(mods);
        }

        // ==== Move ====
        void ITCSetterAPI.Enter_Move_Current(TCMovementStateModel[] mods, bool isExitReset) {
            var cameraDomain = domain.CameraDomain;
            cameraDomain.Enter_Movement_Current(mods, isExitReset, default, 0);
        }

        void ITCSetterAPI.Enter_Move_Current(TCMovementStateModel[] mods, bool isExitReset, EasingType exitEasingType, float exitDuration) {
            var cameraDomain = domain.CameraDomain;
            cameraDomain.Enter_Movement_Current(mods, isExitReset, exitEasingType, exitDuration);
        }

        void ITCSetterAPI.Enter_Round_Current(TCRoundStateModel[] mods, bool isExitReset, EasingType exitEasingType, float exitDuration) {
            var cameraDomain = domain.CameraDomain;
            cameraDomain.Enter_Round_Current(mods, isExitReset, exitEasingType, exitDuration);
        }

        // ==== Push ====
        void ITCSetterAPI.Enter_Push_Current(TCPushStateModel[] mods, bool isExitReset, EasingType exitEasingType, float exitDuration) {
            var cameraDomain = domain.CameraDomain;
            cameraDomain.Enter_Push_Current(mods, isExitReset, exitEasingType, exitDuration);
        }

        // ==== Rotation ====
        void ITCSetterAPI.Enter_Rotation_Current(TCRotationStateModel[] mods, bool isExitReset, EasingType exitEasingType, float exitDuration) {
            var cameraDomain = domain.CameraDomain;
            cameraDomain.Enter_Rotation_Current(mods, isExitReset, exitEasingType, exitDuration);
        }

    }

}