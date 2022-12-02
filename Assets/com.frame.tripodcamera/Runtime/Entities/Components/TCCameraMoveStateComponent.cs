using System;
using UnityEngine;
using JackEasing;

namespace TripodCamera {

    public class TCCameraMovementStateComponent {

        // Args
        TCMovementStateArgs[] args;

        bool isExitReset;
        EasingType exitEasing;
        float exitDuration;

        // Temp
        int index;
        Vector2 resOffset;
        float time;

        Vector2 exitStartOffset;
        bool isExiting;
        float exitTime;

        public event Action OnEndHandle;

        public TCCameraMovementStateComponent() { }

        public void EnterMovement(TCMovementStateArgs[] args, bool isExitReset, EasingType exitEasing, float exitDuration) {

            this.args = args;

            this.isExitReset = isExitReset;
            this.exitEasing = exitEasing;
            this.exitDuration = exitDuration;

            this.index = 0;
            this.time = 0;
            this.exitTime = 0;
            this.isExiting = false;
        }

        public void Tick(float dt) {

            if (isExiting && isExitReset) {
                Exiting(dt);
                return;
            }

            Execute(dt);

        }

        void Execute(float dt) {

            if (args == null || index >= args.Length) {
                return;
            }

            time += dt;

            var cur = args[index];
            if (cur.isInherit) {
                resOffset = EasingHelper.Ease2D(cur.easingType, time, cur.duration, resOffset, cur.offset);
            } else {
                resOffset = EasingHelper.Ease2D(cur.easingType, time, cur.duration, Vector2.zero, cur.offset);
            }

            if (time >= cur.duration) {
                time = 0;
                index += 1;

                bool hasNext = index < args.Length;
                if (hasNext) {
                    var next = args[index];
                    if (next.isInherit) {
                        next.offset += resOffset;
                    } else {
                        resOffset = Vector2.zero;
                    }
                } else {
                    if (OnEndHandle != null) {
                        OnEndHandle.Invoke();
                    }
                    exitStartOffset = resOffset;
                    isExiting = true;
                }

            }
        }

        void Exiting(float dt) {

            if (resOffset == Vector2.zero) {
                return;
            }

            exitTime += dt;
            resOffset = EasingHelper.Ease2D(exitEasing, exitTime, exitDuration, exitStartOffset, Vector2.zero);

            if (exitTime >= exitDuration) {
                exitTime = 0;
            }

        }

        public Vector3 GetMoveOffset() {
            return resOffset;
        }

    }

}