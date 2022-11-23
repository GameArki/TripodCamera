using UnityEngine;
using TripodCamera.Facades;

namespace TripodCamera.Domain {

    internal class TCApplyDomain {

        TCFacades facades;

        internal TCApplyDomain() {}

        internal void Inject(TCFacades facades) {
            this.facades = facades;
        }

        internal void ApplyFollow() {
            var repo = facades.CameraRepo;
            var tcCam = repo.ActiveCam;
            tcCam.Follow_Apply();
        }

        internal void ApplyLookAt() {
            var repo = facades.CameraRepo;
            var tcCam = repo.ActiveCam;
            tcCam.ApplyLookAt();
        }

        internal void ApplyToMain(TCCameraEntity tcCam, Camera mainCam) {
            var info = tcCam.CurrentInfo;
            mainCam.transform.position = info.Pos;
            mainCam.transform.rotation = info.Rot;
            mainCam.fieldOfView = info.FOV;
        }

    }

}