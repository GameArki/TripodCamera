using UnityEngine;
using TripodCamera.Facades;

namespace TripodCamera {

    public class TCapplyPhase {

        TCFacades facades;

        public TCapplyPhase() {}

        public void Inject(TCFacades facades) {
            this.facades = facades;
        }

        public void Tick() {
            var mainCam = facades.MainCamera;
            var tcCam = facades.CameraRepo.ActiveCam;
            ApplyCamera(tcCam, mainCam);
        }

        void ApplyCamera(TCCameraEntity tcCam, Camera mainCam) {
            var info = tcCam.CurrentInfo;
            mainCam.transform.position = info.pos;
            mainCam.transform.rotation = info.rot;
            mainCam.fieldOfView = info.fov;
        }

    }

}