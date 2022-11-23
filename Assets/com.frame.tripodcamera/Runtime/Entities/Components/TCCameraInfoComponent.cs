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

        Vector3 lookAtOffset;
        public Vector3 LookAtOffset => lookAtOffset;

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
            this.followOffset = other.followOffset;
            this.lookAtOffset = other.lookAtOffset;
        }

        // ==== Basic ====
        internal void Push_In(float value) {

            Vector3 fwd;
            if (lookAtTF == null) {
                fwd = rot * Vector3.forward;
            } else {
                fwd = lookAtTF.position - pos;
            }
            fwd.Normalize();

            if (followTF != null) {
                followOffset += fwd * value;
            } else {
                pos += fwd * value;
            }

        }

        internal void Move(Vector2 value) {

            if (lookAtTF == null) {
                Vector3 up;
                Vector3 right;
                up = rot * Vector3.up;
                right = rot * Vector3.right;
                up.Normalize();
                right.Normalize();
                up = up * value.y;
                right = right * value.x;
                pos += right + up;
            } else {

                Vector3 dir = pos - lookAtTF.position;
                float length = dir.magnitude;
                dir.Normalize();

                // Rotate Dir
                float angleX = -value.x;
                float angleY = value.y;
                Quaternion rotX = Quaternion.AngleAxis(angleX, Vector3.up);
                Quaternion rotY = Quaternion.AngleAxis(angleY, Vector3.right);
                dir = rotX * rotY * dir;

                // Move
                pos = lookAtTF.position + dir * length;

            }

        }

        internal void Move_AndChangeLookAtOffset(Vector2 value) {
            Vector3 up;
            Vector3 right;
            up = rot * Vector3.up;
            right = rot * Vector3.right;
            up.Normalize();
            right.Normalize();
            right = right * value.x;
            up = up * value.y;
            pos += right + up;
            if (lookAtTF != null) {
                lookAtOffset += right + up;
            }
        }

        internal void Rotate_Horizontal(float x) {
            var euler = rot.eulerAngles;
            euler.y += x;
            rot = Quaternion.Euler(euler);
            if (lookAtTF != null) {
                lookAtOffset.x -= x;
            }
        }

        internal void Rotate_Vertical(float y) {
            var euler = rot.eulerAngles;
            euler.x -= y;
            rot = Quaternion.Euler(euler);
            if (lookAtTF != null) {
                lookAtOffset.y += y;
            }
        }

        internal void Rotate_Roll(float z) {
            var euler = rot.eulerAngles;
            euler.z += z;
            rot = Quaternion.Euler(euler);
            if (lookAtTF != null) {
                lookAtOffset.z += z;
            }
        }

        internal void Zoom_In(float value, float min, float max) {
            fov -= value;
            if (fov < min) {
                fov = min;
            } else if (fov > max) {
                fov = max;
            }
        }

        // ==== Advanced ====
        // - Follow
        internal void Follow_SetInit(Transform tf, Vector3 offset) {
            this.followTF = tf;
            this.followOffset = offset;
        }

        internal void Follow_ChangeTarget(Transform tf) {
            this.followTF = tf;
        }

        internal void Follow_ChangeOffset(Vector3 offset) {
            this.followOffset = offset;
        }

        internal void Follow_Apply() {
            if (followTF != null) {
                pos = followTF.position + followOffset;
            }
        }

        // - LookAt
        internal void LookAt_SetInit(Transform target, Vector3 offset) {
            this.lookAtTF = target;
            this.lookAtOffset = offset;
        }

        internal void LookAt_ChangeTarget(Transform target) {
            this.lookAtTF = target;
        }

        internal void LookAt_ChangeOffset(Vector3 offset) {
            this.lookAtOffset = offset;
        }

        internal void LookAt_Apply() {
            if (lookAtTF != null) {
                var lookAtPos = lookAtTF.position + lookAtOffset;
                var fwd = lookAtPos - pos;
                rot = Quaternion.LookRotation(fwd);
            }
        }

    }

}