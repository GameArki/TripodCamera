using System;
using UnityEngine;
using JackEasing;

namespace TripodCamera {

    public class TCCameraFollowComponent {

        // - Follow
        Transform followTF;
        public Transform FollowTF => followTF;

        Vector3 followOffset;
        public Vector3 FollowOffset => followOffset;

        float duration;
        EasingType easingType;
        Vector3 easePos;

        // ==== Temp ====
        Vector3 startPos;
        float time;

        public TCCameraFollowComponent() { }

        public void SetEasing(EasingType easingType, float duration) {
            this.easingType = easingType;
            this.duration = duration;
        }

        public void TickEasing(float dt) {

            if (duration == 0) {
                if (followTF != null) {
                    easePos = followTF.position;
                }
                return;
            } else {

                if (time >= duration) {
                    startPos = followTF.position;
                    easePos = followTF.position;
                    time = 0;
                    return;
                }

            }

            Vector3 destPos = followTF.position;

            time += dt;
            if (time >= duration) {
                time = duration;
            }
            easePos = EasingHelper.Ease3D(easingType, time, duration, startPos, destPos);

        }

        public void PushIn(Vector3 fwd, float value) {
            if (followTF != null) {
                followOffset += fwd * value;
            }
        }

        public void OffsetAdd(Vector3 offset) {
            if (followTF != null) {
                followOffset += offset;
            }
        }

        // - Follow
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
            time = 0;
        }

        public void ChangeOffset(Vector3 offset) {
            this.followOffset = offset;
        }

        internal Vector3 GetFollowPos() {
            return easePos + followOffset;
        }

        internal bool IsFollowing() {
            return followTF != null;
        }
    }

}