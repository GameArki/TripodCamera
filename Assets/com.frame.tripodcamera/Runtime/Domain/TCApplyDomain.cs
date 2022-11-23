using UnityEngine;
using TripodCamera.Facades;

namespace TripodCamera.Domain {

    public class TCApplyDomain {

        TCFacades facades;

        public TCApplyDomain() {}

        public void Inject(TCFacades facades) {
            this.facades = facades;
        }

        public void ApplyFollow() {
            var repo = facades.CameraRepo;
            var tcCam = repo.ActiveCam;
            tcCam.Follow_Apply();
        }

        public void ApplyLookAt() {
            var repo = facades.CameraRepo;
            var tcCam = repo.ActiveCam;
            tcCam.ApplyLookAt();
        }

        public void ApplyToMain(TCCameraEntity tcCam, Camera mainCam) {
            var info = tcCam.CurrentInfo;
            mainCam.transform.position = info.Pos;
            mainCam.transform.rotation = info.Rot;
            mainCam.fieldOfView = info.FOV;
        }

    }

}