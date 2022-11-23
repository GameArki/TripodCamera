using UnityEngine;

namespace TripodCamera.Facades {

    internal class TCFacades {

        TCConfig config;
        internal TCConfig Config => config;

        Camera camera;
        internal Camera MainCamera => camera;

        TCCameraRepo cameraRepo;
        internal TCCameraRepo CameraRepo => cameraRepo;

        internal TCFacades() {
            this.config = new TCConfig();
            this.cameraRepo = new TCCameraRepo();
        }

        internal void Inject(Camera camera) {
            this.camera = camera;
        }

    }

}