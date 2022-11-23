using System.Collections.Generic;

namespace TripodCamera {

    internal class TCCameraRepo {

        TCCameraEntity activeCam;
        internal TCCameraEntity ActiveCam => activeCam;

        List<TCCameraEntity> all = new List<TCCameraEntity>();

        internal TCCameraRepo() {}

        internal void SetActiveCam(TCCameraEntity cam) {
            this.activeCam = cam;
        } 

        internal void Add(TCCameraEntity camera) {
            if (activeCam == null) {
                activeCam = camera;
            }
            all.Add(camera);
        }

        internal void Remove(TCCameraEntity camera) {
            all.Remove(camera);
        }

        internal void Clear() {
            all.Clear();
        }

        internal TCCameraEntity Get(int index) {
            return all[index];
        }

        internal int Count => all.Count;

    }

}