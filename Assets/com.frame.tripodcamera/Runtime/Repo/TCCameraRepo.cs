using System.Collections.Generic;

namespace TripodCamera {

    public class TCCameraRepo {

        TCCameraEntity activeCam;
        public TCCameraEntity ActiveCam => activeCam;

        List<TCCameraEntity> all = new List<TCCameraEntity>();

        public TCCameraRepo() {}

        public void SetActiveCam(TCCameraEntity cam) {
            this.activeCam = cam;
        }

        public void Add(TCCameraEntity camera) {
            all.Add(camera);
        }

        public void Remove(TCCameraEntity camera) {
            all.Remove(camera);
        }

        public void Clear() {
            all.Clear();
        }

        public TCCameraEntity Get(int index) {
            return all[index];
        }

        public int Count => all.Count;

    }

}