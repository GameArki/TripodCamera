using UnityEngine;
using JackEasing;
using TripodCamera.Facades;
using TripodCamera.Entities;

namespace TripodCamera.Domain {

    public class TCCameraDomain {

        TCFacades facades;

        public TCCameraDomain() { }

        public void Inject(TCFacades facades) {
            this.facades = facades;
        }

        public void Spawn(Vector3 pos, Quaternion rot, float fov) {

            var repo = facades.CameraRepo;
            var tcCam = new TCCameraEntity();
            tcCam.InitInfo(pos, rot, fov);

            repo.Add(tcCam);

        }

        // ==== Basic ====
        public void Push_In_Current(float value) {
            var repo = facades.CameraRepo;
            var tcCam = repo.ActiveCam;
            tcCam.Push_In(value);
        }

        public void Move_Current(Vector2 value) {
            var repo = facades.CameraRepo;
            var tcCam = repo.ActiveCam;
            tcCam.Move(value);
        }

        public void Move_AndChangeLookAtOffset_Current(Vector2 value) {
            var repo = facades.CameraRepo;
            var tcCam = repo.ActiveCam;
            tcCam.Move_AndChangeLookAtOffset(value);
        }

        public void Rotate_Horizontal_Current(float x) {
            var repo = facades.CameraRepo;
            var tcCam = repo.ActiveCam;
            tcCam.Rotate_Horizontal(x);
        }

        public void Rotate_Vertical_Current(float y) {
            var repo = facades.CameraRepo;
            var tcCam = repo.ActiveCam;
            tcCam.Rotate_Vertical(y);
        }

        public void Rotate_Roll_Current(float z) {
            var repo = facades.CameraRepo;
            var tcCam = repo.ActiveCam;
            tcCam.Rotate_Roll(z);
        }

        public void Zoom_In_Current(float value) {
            var repo = facades.CameraRepo;
            var config = facades.Config;
            var tcCam = repo.ActiveCam;
            tcCam.ZoomIn(value, config.FOVMin, config.FOVMax);
        }

        // ==== Advance ====
        // - Follow
        public void Follow_SetInit_Current(Transform target, Vector3 offset, EasingType easingType, float easingTime) {
            var repo = facades.CameraRepo;
            var tcCam = repo.ActiveCam;
            tcCam.Follow_SetInit(target, offset, easingType, easingTime);
        }

        public void Follow_ChangeTarget_Current(Transform target) {
            var repo = facades.CameraRepo;
            var tcCam = repo.ActiveCam;
            tcCam.Follow_ChangeTarget(target);
        }

        public void Follow_ChangeOffset_Current(Vector3 offset) {
            var repo = facades.CameraRepo;
            var tcCam = repo.ActiveCam;
            tcCam.Follow_ChangeOffset(offset);
        }

        // - LookAt
        public void LookAt_SetInit_Current(Transform target, Vector3 offset) {
            var repo = facades.CameraRepo;
            var tcCam = repo.ActiveCam;
            tcCam.LookAt_SetInit(target, offset);
        }

        public void LookAt_ChangeTarget_Current(Transform target) {
            var repo = facades.CameraRepo;
            var tcCam = repo.ActiveCam;
            tcCam.LookAt_ChangeTarget(target);
        }

        public void LookAt_ChangeOffset_Current(Vector3 offset) {
            var repo = facades.CameraRepo;
            var tcCam = repo.ActiveCam;
            tcCam.LookAt_ChangeOffset(offset);
        }

        // ==== State ====
        // - Shake
        public void Enter_Shake_Current(TCShakeStateModel[] args) {
            var repo = facades.CameraRepo;
            var tcCam = repo.ActiveCam;
            tcCam.ShakeComponent.SetShake(args);
        }

        // - Movement
        public void Enter_Movement_Current(TCMovementStateModel[] args, bool isExitReset, EasingType exitEasing, float exitDuration) {
            var repo = facades.CameraRepo;
            var tcCam = repo.ActiveCam;
            tcCam.MovementStateComponent.EnterMovement(args, isExitReset, exitEasing, exitDuration);
        }

        // - Rotation
        public void Enter_Rotation_Current(TCRotationStateModel[] args, bool isExitReset, EasingType exitEasing, float exitDuration) {
            var repo = facades.CameraRepo;
            var tcCam = repo.ActiveCam;
            tcCam.RotateStateComponent.EnterRotation(args, isExitReset, exitEasing, exitDuration);
        }

        // - Push
        public void Enter_Push_Current(TCPushStateModel[] args, bool isExitReset, EasingType exitEasing, float exitDuration) {
            var repo = facades.CameraRepo;
            var tcCam = repo.ActiveCam;
            tcCam.PushStateComponent.EnterPush(args, isExitReset, exitEasing, exitDuration);
        }

    }

}