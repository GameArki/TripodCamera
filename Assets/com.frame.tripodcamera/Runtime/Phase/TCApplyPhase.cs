using UnityEngine;
using TripodCamera.Facades;
using TripodCamera.Domain;

namespace TripodCamera.Controller {

    public class TCapplyPhase {

        TCFacades facades;
        TCDomain domain;

        public TCapplyPhase() {}

        public void Inject(TCFacades facades, TCDomain domain) {
            this.facades = facades;
            this.domain = domain;
        }

        public void Tick(float dt) {
            var mainCam = facades.MainCamera;
            var tcCam = facades.CameraRepo.ActiveCam;
            ApplyCamera(tcCam, mainCam);
        }

        void ApplyCamera(TCCameraEntity tcCam, Camera mainCam) {
            var info = tcCam.CurrentInfo;
            mainCam.transform.position = info.Pos;
            mainCam.transform.rotation = info.Rot;
            mainCam.fieldOfView = info.FOV;
        }

    }

}