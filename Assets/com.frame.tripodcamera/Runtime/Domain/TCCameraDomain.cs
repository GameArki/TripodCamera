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

        public void PushInCurrent(float value) {
            var repo = facades.CameraRepo;
            var tcCam = repo.ActiveCam;
            tcCam.PushIn(value);
        }

        public void Move(Vector2 value) {
            var repo = facades.CameraRepo;
            var tcCam = repo.ActiveCam;
            tcCam.Move(value);
        }

        public void ZoomInCurrent(float value) {
            var repo = facades.CameraRepo;
            var tcCam = repo.ActiveCam;
            tcCam.ZoomIn(value);
        }

    }

}