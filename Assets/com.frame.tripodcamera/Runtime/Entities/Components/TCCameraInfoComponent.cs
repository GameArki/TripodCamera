using System;
using UnityEngine;

namespace TripodCamera {

    public class TCCameraInfoComponent {

        Vector3 pos;
        public Vector3 Pos => pos;
        public void SetPos(Vector3 value) => pos = value;

        Quaternion rot;
        public Quaternion Rot => rot;
        public void SetRot(Quaternion value) => rot = value;

        float fov;
        public float FOV => fov;
        public void SetFOV(float value) => fov = value;

        public void Init(Vector3 pos, Quaternion rot, float fov) {
            this.pos = pos;
            this.rot = rot;
            this.fov = fov;
        }

        public void CloneFrom(TCCameraInfoComponent other) {
            this.pos = other.pos;
            this.rot = other.rot;
            this.fov = other.fov;
        }

        // ==== Basic ====
        public void Rotate_Horizontal(float x) {
            var euler = rot.eulerAngles;
            euler.y += x;
            rot = Quaternion.Euler(euler);
        }

        public void Rotate_Vertical(float y) {
            var euler = rot.eulerAngles;
            euler.x -= y;
            rot = Quaternion.Euler(euler);
        }

        public void Rotate_Roll(float z) {
            var euler = rot.eulerAngles;
            euler.z += z;
            rot = Quaternion.Euler(euler);
        }

        public void Zoom_In(float value, float min, float max) {
            fov -= value;
            if (fov < min) {
                fov = min;
            } else if (fov > max) {
                fov = max;
            }
        }

    }

}