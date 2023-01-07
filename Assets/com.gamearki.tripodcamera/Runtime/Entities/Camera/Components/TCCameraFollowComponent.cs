using UnityEngine;
using GameArki.FPEasing;

namespace GameArki.TripodCamera.Entities {

    public class TCCameraFollowComponent {

        // - Follow
        Transform followTF;
        public Transform FollowTF => followTF;

        Vector2 roundOffset;

        Vector3 followOffset;
        public Vector3 FollowOffset => followOffset;

        EasingType easingType_horizontal;
        public EasingType XEasing => easingType_horizontal;

        EasingType easingType_vertical;
        public EasingType YEasing => easingType_vertical;

        float duration_horizontal;
        public float XDuration => duration_horizontal;
    
        float duration_vertical;
        public float YDuration => duration_vertical;

        Vector3 easePos;

        // ==== Temp ====
        Vector3 startPos;
        Vector3 dstPos;
        float time_horizontal;
        float time_vertical;

        public TCCameraFollowComponent() { }

        public void SetEasing(EasingType easingType_horizontal, float duration_horizontal, EasingType easingType_vertical, float duration_vertical) {
            this.easingType_horizontal = easingType_horizontal;
            this.easingType_vertical = easingType_vertical;
            this.duration_horizontal = duration_horizontal;
            this.duration_vertical = duration_vertical;
        }

        public void TickEasing(float dt) {

            if (duration_horizontal == 0) {
                if (followTF != null) {
                    easePos = followTF.position;
                }
                return;
            }

            if (dstPos != followTF.position) {
                startPos = easePos;
                dstPos = followTF.position;
                time_horizontal = 0;
                time_vertical = 0;
            }

            if (time_horizontal < duration_horizontal) {
                time_horizontal += dt;
                var x = EasingHelper.Ease1D(easingType_horizontal, time_horizontal, duration_horizontal, startPos.x, dstPos.x);
                var z = EasingHelper.Ease1D(easingType_horizontal, time_horizontal, duration_horizontal, startPos.z, dstPos.z);
                easePos.x = x;
                easePos.z = z;
            }

            if (time_vertical < duration_vertical) {
                time_vertical += dt;
                var y = EasingHelper.Ease1D(easingType_vertical, time_vertical, duration_vertical, startPos.y, dstPos.y);
                easePos.y = y;
            }

        }

        public void PushIn(Vector3 fwd, float value) {
            if (followTF != null) {
                followOffset += fwd * value;
            }
        }

        public void RoundOffsetAdd(Vector2 offset) {
            if (followTF != null) {
                roundOffset += offset;
            }
        }

        // - Follow
        public void FollowOffsetAdd(Vector3 offset) {
            if (followTF != null) {
                followOffset += offset;
            }
        }

        public void SetInit(Transform tf, Vector3 offset) {
            ChangeTarget(tf);
            ChangeOffset(offset);
        }

        public void ChangeTarget(Transform tf) {
            if (tf == null) {
                return;
            }
            this.followTF = tf;
            startPos = tf.position;
            time_horizontal = 0;
            roundOffset = Vector2.zero;
        }

        public void ChangeOffset(Vector3 offset) {
            this.followOffset = offset;
        }

        public void ChangeXEasing(EasingType easingType, float duration) {
            this.easingType_horizontal = easingType;
            this.duration_horizontal = duration;
        }

        public void ChangeYEasing(EasingType easingType, float duration) {
            this.easingType_vertical = easingType;
            this.duration_vertical = duration;
        }

        internal Vector3 GetFollowPos(Vector3 forward) {
            var rot = Quaternion.LookRotation(forward);
            Quaternion roundRot = Quaternion.Euler(roundOffset.y, roundOffset.x, 0);
            var finalOffset = rot * roundRot * followOffset;
            return easePos + finalOffset;
        }

        internal bool IsFollowing() {
            return followTF != null;
        }
    }

}