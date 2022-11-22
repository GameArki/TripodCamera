using System;
using UnityEngine;

namespace TripodCamera {

    public class TCCameraInfoComponent {

        Vector3 pos;
        public Vector3 Pos => pos;

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

        internal void PushIn(float value) {
            Vector3 dir;
            if (followTF == null) {
                dir = rot * Vector3.forward;
            } else {
                dir = followTF.position - pos;
            }
            dir.Normalize();
            pos += dir * value;
        }
    }

}