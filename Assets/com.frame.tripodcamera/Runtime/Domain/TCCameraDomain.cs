using UnityEngine;
using TripodCamera.Facades;

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
        public void PushInCurrent(float value) {
            var repo = facades.CameraRepo;
            var tcCam = repo.ActiveCam;
            tcCam.PushIn(value);
        }

        public void MoveCurrent(Vector2 value) {
            var repo = facades.CameraRepo;
            var tcCam = repo.ActiveCam;
            tcCam.Move(value);
        }

        public void RotateHorizontalCurrent(float x) {
            var repo = facades.CameraRepo;
            var tcCam = repo.ActiveCam;
            tcCam.RotateHorizontal(x);
        }

        public void RotateVerticalCurrent(float y) {
            var repo = facades.CameraRepo;
            var tcCam = repo.ActiveCam;
            tcCam.RotateVertical(y);
        }

        public void RotateRollCurrent(float z) {
            var repo = facades.CameraRepo;
            var tcCam = repo.ActiveCam;
            tcCam.RotateRoll(z);
        }

        public void ZoomInCurrent(float value) {
            var repo = facades.CameraRepo;
            var config = facades.Config;
            var tcCam = repo.ActiveCam;
            tcCam.ZoomIn(value, config.FOVMin, config.FOVMax);
        }

        // ==== Advance ====
        public void SetFollowCurrent(Transform target, Vector3 offset) {
            var repo = facades.CameraRepo;
            var tcCam = repo.ActiveCam;
            tcCam.SetFollow(target, offset);
        }

        public void ApplyFollowCurrent() {
            var repo = facades.CameraRepo;
            var tcCam = repo.ActiveCam;
            tcCam.ApplyFollow();
        }

    }

}