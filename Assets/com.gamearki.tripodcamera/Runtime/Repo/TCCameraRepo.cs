using System.Collections.Generic;
using GameArki.TripodCamera.Entities;

namespace GameArki.TripodCamera {

    public class TCCameraRepo {

        TCCameraEntity activeCam;
        public TCCameraEntity ActiveCam => activeCam;

        Dictionary<int, TCCameraEntity> all;
        public int Count => all.Count;

        public TCCameraRepo() {
            this.all = new Dictionary<int, TCCameraEntity>();
        }

        public void SetActiveCam(int id) {
            bool has = all.TryGetValue(id, out var cam);
            if (has) {
                activeCam = cam;
            }
        }

        public void Add(TCCameraEntity camera) {
            bool has = all.TryAdd(camera.ID, camera);
            if (has && activeCam == null) {
                activeCam = camera;
            }
        }

        public bool TryGet(int id, out TCCameraEntity entity) {
            return all.TryGetValue(id, out entity);
        }

        public void Remove(int id) {
            all.Remove(id);
        }

        public void Clear() {
            all.Clear();
        }

    }

}