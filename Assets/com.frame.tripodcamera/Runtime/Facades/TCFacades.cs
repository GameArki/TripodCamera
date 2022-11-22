using UnityEngine;

namespace TripodCamera.Facades {

    public class TCFacades {

        Camera camera;
        public Camera MainCamera => camera;

        TCCameraRepo cameraRepo;
        public TCCameraRepo CameraRepo => cameraRepo;

        public TCFacades() {
            this.cameraRepo = new TCCameraRepo();
        }

        public void Inject(Camera camera) {
            this.camera = camera;
        }

    }

}