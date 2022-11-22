using TripodCamera.Facades;

namespace TripodCamera {

    public class TCInitializePhase {

        TCFacades facades;

        public TCInitializePhase() {}

        public void Inject(TCFacades facades) {
            this.facades = facades;
        }

        public void Init() {
            
            var cam = facades.MainCamera;

            var repo = facades.CameraRepo;
            var tcCam = new TCCameraEntity();
            tcCam.InitInfo(cam.transform.position, cam.transform.rotation, cam.fieldOfView);

            repo.Add(tcCam);
            repo.SetActiveCam(tcCam);
            
        }

    }
}