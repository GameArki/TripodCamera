using System;
using JackEasing;

namespace TripodCamera.Entities {


    public class TCCameraPushStateComponent {

        // Args
        TCPushStateModel[] arr;

        bool isExitReset;
        EasingType exitEasing;
        float exitDuration;

        // Temp
        int index;
        float resOffset;
        float time;

        float exitStartOffset;
        bool isExiting;
        float exitTime;

        public event Action OnEndHandle;

        public TCCameraPushStateComponent() { }

        public void EnterPush(TCPushStateModel[] args, bool isExitReset, EasingType exitEasing, float exitDuration) {

            this.arr = args;

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

            if (arr == null || index >= arr.Length) {
                return;
            }

            time += dt;

            var cur = arr[index];
            if (cur.isInherit) {
                resOffset = EasingHelper.Ease1D(cur.easingType, time, cur.duration, resOffset, cur.offset);
            } else {
                resOffset = EasingHelper.Ease1D(cur.easingType, time, cur.duration, 0, cur.offset);
            }

            if (time >= cur.duration) {
                time = 0;
                index += 1;

                bool hasNext = index < arr.Length;
                if (hasNext) {
                    var next = arr[index];
                    if (next.isInherit) {
                        next.offset += resOffset;
                    } else {
                        resOffset = 0;
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

            if (resOffset == 0) {
                return;
            }

            exitTime += dt;
            resOffset = EasingHelper.Ease1D(exitEasing, exitTime, exitDuration, exitStartOffset, 0);

            if (exitTime >= exitDuration) {
                exitTime = 0;
            }

        }

        public float GetPushOffset() {
            return resOffset;
        }

    }

}