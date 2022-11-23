using UnityEngine;
using TripodCamera.Facades;

namespace TripodCamera.Domain {

    internal class TCCameraDomain {

        TCFacades facades;

        internal TCCameraDomain() { }

        internal void Inject(TCFacades facades) {
            this.facades = facades;
        }

        internal void Spawn(Vector3 pos, Quaternion rot, float fov) {

            var repo = facades.CameraRepo;
            var tcCam = new TCCameraEntity();
            tcCam.InitInfo(pos, rot, fov);

            repo.Add(tcCam);

        }

        // ==== Basic ====
        internal void Push_In_Current(float value) {
            var repo = facades.CameraRepo;
            var tcCam = repo.ActiveCam;
            tcCam.Push_In(value);
        }

        internal void Move_Current(Vector2 value) {
            var repo = facades.CameraRepo;
            var tcCam = repo.ActiveCam;
            tcCam.Move(value);
        }

        internal void Move_AndChangeLookAtOffset_Current(Vector2 value) {
            var repo = facades.CameraRepo;
            var tcCam = repo.ActiveCam;
            tcCam.Move_AndChangeLookAtOffset(value);
        }

        internal void Rotate_Horizontal_Current(float x) {
            var repo = facades.CameraRepo;
            var tcCam = repo.ActiveCam;
            tcCam.Rotate_Horizontal(x);
        }

        internal void Rotate_Vertical_Current(float y) {
            var repo = facades.CameraRepo;
            var tcCam = repo.ActiveCam;
            tcCam.Rotate_Vertical(y);
        }

        internal void Rotate_Roll_Current(float z) {
            var repo = facades.CameraRepo;
            var tcCam = repo.ActiveCam;
            tcCam.Rotate_Roll(z);
        }

        internal void Zoom_In_Current(float value) {
            var repo = facades.CameraRepo;
            var config = facades.Config;
            var tcCam = repo.ActiveCam;
            tcCam.ZoomIn(value, config.FOVMin, config.FOVMax);
        }

        // ==== Advance ====
        // - Follow
        internal void Follow_SetInit_Current(Transform target, Vector3 offset) {
            var repo = facades.CameraRepo;
            var tcCam = repo.ActiveCam;
            tcCam.Follow_SetInit(target, offset);
        }

        internal void Follow_ChangeTarget_Current(Transform target) {
            var repo = facades.CameraRepo;
            var tcCam = repo.ActiveCam;
            tcCam.Follow_ChangeTarget(target);
        }

        internal void Follow_ChangeOffset_Current(Vector3 offset) {
            var repo = facades.CameraRepo;
            var tcCam = repo.ActiveCam;
            tcCam.Follow_ChangeOffset(offset);
        }

        // - LookAt
        internal void LookAt_SetInit_Current(Transform target, Vector3 offset) {
            var repo = facades.CameraRepo;
            var tcCam = repo.ActiveCam;
            tcCam.LookAt_SetInit(target, offset);
        }

        internal void LookAt_ChangeTarget_Current(Transform target) {
            var repo = facades.CameraRepo;
            var tcCam = repo.ActiveCam;
            tcCam.LookAt_ChangeTarget(target);
        }

        internal void LookAt_ChangeOffset_Current(Vector3 offset) {
            var repo = facades.CameraRepo;
            var tcCam = repo.ActiveCam;
            tcCam.LookAt_ChangeOffset(offset);
        }

    }

}