using UnityEngine;

namespace TripodCamera.Entities {

    public class TCCameraLookAtComponent {

        // - LookAt
        Transform lookAtTF;
        public Transform LookAtTF => lookAtTF;

        Vector3 lookAtOffset;
        public Vector3 LookAtOffset => lookAtOffset;

        public TCCameraLookAtComponent() { }

        public void OffsetAdd(Vector3 offset) {
            if (lookAtTF != null) {
                lookAtOffset += offset;
            }
        }

        public void Rotate_Horizontal(float x) {
            if (lookAtTF != null) {
                lookAtOffset.x += x;
            }
        }

        public void Rotate_Vertical(float y) {
            if (lookAtTF != null) {
                lookAtOffset.y += y;
            }
        }

        public void LookAt_SetInit(Transform target, Vector3 offset) {
            this.lookAtTF = target;
            this.lookAtOffset = offset;
        }

        public void LookAt_ChangeTarget(Transform target) {
            this.lookAtTF = target;
        }

        public void LookAt_ChangeOffset(Vector3 offset) {
            this.lookAtOffset = offset;
        }

        public bool IsLookingAt() {
            return lookAtTF != null;
        }

        public Quaternion GetLookAtRotation(Vector3 pos, Quaternion rot) {
            var lookAtPos = lookAtTF.position + lookAtOffset;
            var fwd = lookAtPos - pos;
            var z = rot.eulerAngles.z;
            rot = Quaternion.LookRotation(fwd);
            rot.eulerAngles = new Vector3(rot.eulerAngles.x, rot.eulerAngles.y, z);
            return rot;
        }

    }

}