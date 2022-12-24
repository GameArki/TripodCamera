using System;
using GameArki.FPEasing;
using UnityEngine;

namespace GameArki.TripodCamera.Entities {


    public class TCCameraRoundStateComponent {

        // Args
        TCRoundStateModel[] arr;

        bool isExitReset;
        EasingType exitEasing;
        float exitDuration;

        // Temp
        int index;
        Vector2 resOffset;
        Vector2 resOffset_inherit;
        float time;

        Vector2 exitStartOffset;
        bool isExiting;
        float exitTime;

        public event Action OnEndHandle;

        Transform curTar;

        public TCCameraRoundStateComponent() { }

        public void EnterRound(TCRoundStateModel[] args, bool isExitReset, EasingType exitEasing, float exitDuration) {

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
            curTar = cur.tar;
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

            if (resOffset == Vector2.zero) {
                return;
            }

            exitTime += dt;
            resOffset = EasingHelper.Ease2D(exitEasing, exitTime, exitDuration, exitStartOffset, Vector2.zero);

            if (exitTime >= exitDuration) {
                exitTime = 0;
            }

        }

        public Vector3 GetRoundPosOffset(Vector3 pos) {
            if (arr == null) {
                return Vector3.zero;
            }

            var tarPos = curTar.position;
            Vector3 dir = pos - tarPos;
            float length = dir.magnitude;
            dir.Normalize();

            // Round Dir
            float angleX = -resOffset.x;
            float angleY = resOffset.y;
            var up = dir;
            up.x = 0;
            up.z = 0;
            var right = dir;
            right.y = 0;
            right = Quaternion.Euler(0, 90, 0) * right;
            Quaternion rotX = Quaternion.AngleAxis(angleX, up);
            Quaternion rotY = Quaternion.AngleAxis(angleY, right);
            dir = rotX * rotY * dir;
            var offset = (tarPos + dir * length) - pos;

            return offset;
        }

    }

}