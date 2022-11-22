using UnityEngine;

namespace TripodCamera {

    public class TCCameraInfoComponent {

        public Vector3 pos;
        public Quaternion rot;
        public float fov;

        public Transform followTF;
        public Transform lookAtTF;

        public void Init(Vector3 pos, Quaternion rot, float fov) {
            this.pos = pos;
            this.rot = rot;
            this.fov = fov;
        }

        public void CloneFrom(TCCameraInfoComponent other) {
            this.pos = other.pos;
            this.rot = other.rot;
            this.fov = other.fov;
            this.followTF = other.followTF;
            this.lookAtTF = other.lookAtTF;
        }

    }

}