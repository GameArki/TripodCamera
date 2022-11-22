using System;
using UnityEngine;

namespace TripodCamera {

    public class TCCameraInfoComponent {

        Vector3 pos;
        public Vector3 Pos => pos;

        Quaternion rot;
        public Quaternion Rot => rot;

        float fov;
        public float FOV => fov;

        // ==== Advance ====
        // - Follow
        Transform followTF;
        public Transform FollowTF => followTF;

        Vector3 followOffset;
        public Vector3 FollowOffset => followOffset;

        // - LookAt
        Transform lookAtTF;
        public Transform LookAtTF => lookAtTF;

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

        // ==== Basic ====
        internal void PushIn(float value) {
            Vector3 fwd;
            if (lookAtTF == null) {
                fwd = rot * Vector3.forward;
            } else {
                fwd = lookAtTF.position - pos;
            }
            fwd.Normalize();
            pos += fwd * value;
        }

        internal void Move(Vector2 value) {
            Vector3 up;
            Vector3 right;
            if (lookAtTF == null) {
                up = rot * Vector3.up;
                right = rot * Vector3.right;
            } else {
                var fwd = lookAtTF.position - pos;
                right = Vector3.Cross(fwd, Vector3.up);
                up = Vector3.Cross(fwd, right);
            }
            up.Normalize();
            right.Normalize();
            pos += right * value.x;
            pos += up * value.y;
        }

        internal void RotateHorizontal(float x) {
            var euler = rot.eulerAngles;
            euler.y += x;
            rot = Quaternion.Euler(euler);
        }

        internal void RotateVertical(float y) {
            var euler = rot.eulerAngles;
            euler.x += y;
            rot = Quaternion.Euler(euler);
        }

        internal void RotateRoll(float z) {
            var euler = rot.eulerAngles;
            euler.z += z;
            rot = Quaternion.Euler(euler);
        }

        internal void ZoomIn(float value, float min, float max) {
            fov -= value;
            if (fov < min) {
                fov = min;
            } else if (fov > max) {
                fov = max;
            }
        }

        // ==== Advanced ====
        internal void SetFollow(Transform tf, Vector3 offset) {
            this.followTF = tf;
            this.followOffset = offset;
        }

        internal void ApplyFollow() {
            if (followTF != null) {
                pos = followTF.position + followOffset;
            }
        }

    }

}