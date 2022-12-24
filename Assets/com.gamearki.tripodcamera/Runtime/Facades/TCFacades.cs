using UnityEngine;

namespace GameArki.TripodCamera.Facades {

    public class TCFacades {

        TCConfig config;
        public TCConfig Config => config;

        Camera camera;
        public Camera MainCamera => camera;

        TCCameraRepo cameraRepo;
        public TCCameraRepo CameraRepo => cameraRepo;

        public TCFacades() {
            this.config = new TCConfig();
            this.cameraRepo = new TCCameraRepo();
        }

        public void Inject(Camera camera) {
            this.camera = camera;
        }

    }

}