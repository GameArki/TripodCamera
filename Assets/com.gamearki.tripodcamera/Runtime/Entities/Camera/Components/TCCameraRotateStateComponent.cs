using System;
using UnityEngine;
using GameArki.FPEasing;

namespace GameArki.TripodCamera.Entities {

    public class TCCameraRotateStateComponent {

        // Args
        TCRotationStateModel[] arr;

        bool isExitReset;
        EasingType exitEasing;
        float exitDuration;

        // Temp
        int index;
        Vector3 resOffset;
        Vector3 resOffset_inherit;
        float time;

        Vector2 exitStartOffset;
        bool isExiting;
        float exitTime;

        public event Action OnEndHandle;

        public TCCameraRotateStateComponent() { }

        public void EnterRotation(TCRotationStateModel[] args, bool isExitReset, EasingType exitEasing, float exitDuration) {
            
            if (args.Length == 0) return;
            var args_0 = args[0];
            if (args_0.isInherit) {
                resOffset_inherit = resOffset;
                args_0.offset += resOffset;
                args[0] = args_0;
            }
            
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
                resOffset = EasingHelper.Ease2D(cur.easingType, time, cur.duration, resOffset_inherit, cur.offset);
            } else {
                resOffset = EasingHelper.Ease2D(cur.easingType, time, cur.duration, Vector2.zero, cur.offset);
            }

            if (time >= cur.duration) {
                time = 0;
                index += 1;

                bool hasNext = index < arr.Length;
                if (hasNext) {
                    var next = arr[index];
                    if (next.isInherit) {
                        next.offset += resOffset;
                        resOffset_inherit = resOffset;
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

            if (resOffset == Vector3.zero) {
                return;
            }

            exitTime += dt;
            resOffset = EasingHelper.Ease2D(exitEasing, exitTime, exitDuration, exitStartOffset, Vector2.zero);

            if (exitTime >= exitDuration) {
                exitTime = 0;
            }

        }

        public Vector3 GetRotateOffset() {
            return resOffset;
        }

    }

}