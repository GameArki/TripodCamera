using UnityEngine;
using GameArki.FPEasing;
using GameArki.TripodCamera.Facades;
using GameArki.TripodCamera.Entities;
using GameArki.TripodCamera.Hook;
using System;

namespace GameArki.TripodCamera.Domain {

    public class TCCameraDomain {

        TCFacades facades;

        public TCCameraDomain() { }

        public void Inject(TCFacades facades) {
            this.facades = facades;
        }

        public void Spawn(int id, Vector3 pos, Quaternion rot, float fov) {

            var repo = facades.CameraRepo;
            var tcCam = new TCCameraEntity();
            tcCam.SetID(id);
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

        public void Round_Current(Vector2 value) {
            var repo = facades.CameraRepo;
            var tcCam = repo.ActiveCam;
            tcCam.Round(value);
        }

        public void Rotate_Horizontal_Current(float x) {
            var repo = facades.CameraRepo;
            var tcCam = repo.ActiveCam;
            tcCam.Rotate_Horizontal(x);
        }

        internal TCCameraHook SpawnHook(int id) {
            var repo = facades.CameraRepo;
            bool has = repo.TryGet(id, out var cam);
            if (has) {
                var go = new GameObject($"tc_hook_{id}");
                var hook = go.AddComponent<TCCameraHook>();
                hook.Ctor(cam);
                facades.HookRepo.Add(hook);
                return hook;
            } else {
                return null;
            }
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
        public void Follow_SetInit_Current(Transform target, Vector3 offset,
        EasingType easingType_horizontal, float easingTime_horizontal,
        EasingType easingType_vertical, float easingTime_vertical) {
            var repo = facades.CameraRepo;
            var tcCam = repo.ActiveCam;
            tcCam.Follow_SetInit(target, offset, easingType_horizontal, easingTime_horizontal, easingType_vertical, easingTime_vertical);
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
        public void Enter_Shake_Current(TCShakeStateModel[] mods) {
            var repo = facades.CameraRepo;
            var tcCam = repo.ActiveCam;
            tcCam.ShakeComponent.SetShake(mods);
        }

        // - Movement
        public void Enter_Movement_Current(TCMovementStateModel[] mods, bool isExitReset, EasingType exitEasing, float exitDuration) {
            var repo = facades.CameraRepo;
            var tcCam = repo.ActiveCam;
            tcCam.MovementStateComponent.EnterMovement(mods, isExitReset, exitEasing, exitDuration);
        }

        // - Round
        public void Enter_Round_Current(TCRoundStateModel[] mods, bool isExitReset, EasingType exitEasing, float exitDuration) {
            var repo = facades.CameraRepo;
            var tcCam = repo.ActiveCam;
            tcCam.RoundStateComponent.EnterRound(mods, isExitReset, exitEasing, exitDuration);
        }

        // - Rotation
        public void Enter_Rotation_Current(TCRotationStateModel[] mods, bool isExitReset, EasingType exitEasing, float exitDuration) {
            var repo = facades.CameraRepo;
            var tcCam = repo.ActiveCam;
            tcCam.RotateStateComponent.EnterRotation(mods, isExitReset, exitEasing, exitDuration);
        }

        // - Push
        public void Enter_Push_Current(TCPushStateModel[] mods, bool isExitReset, EasingType exitEasing, float exitDuration) {
            var repo = facades.CameraRepo;
            var tcCam = repo.ActiveCam;
            tcCam.PushStateComponent.EnterPush(mods, isExitReset, exitEasing, exitDuration);
        }

    }

}