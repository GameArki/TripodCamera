using System.Collections.Generic;
using UnityEngine;
using JackEasing;

namespace TripodCamera {

    public class TCCameraEffectComponent {

        // ==== Locomotion ====
        List<TCDollyPointModel> dollyPoints;
        int dollyIndex;
        float time;

        // ==== Shake ====
        // - Shake Position

        public TCCameraEffectComponent() {
            this.dollyPoints = new List<TCDollyPointModel>();
        }

        public void AddDollyPoint(TCDollyPointModel dollyPoint) {
            dollyPoints.Add(dollyPoint);
        }

        public void TickDolly(float dt) {
            bool has = TryGetCurrentDolly(out var dolly);
            if (has) {
                time += dt;
                if (time > dolly.MaintainTimeMax) {
                    time = 0;
                    dollyIndex += 1;
                }
            }
        }

        bool TryGetCurrentDolly(out TCDollyPointModel model) {
            if (dollyIndex >= dollyPoints.Count) {
                model = default;
                return false;
            } else {
                model = dollyPoints[dollyIndex];
                return true;
            }
        }

        public Vector3 GetDollyMoveOffset() {
            bool has = TryGetCurrentDolly(out var dolly);
            if (has) {
                float t = Mathf.Min(time, dolly.moveMaintainTime);
                Vector3 value = EasingHelper.Ease3D(dolly.moveEasingType, t, dolly.moveMaintainTime, Vector3.zero, dolly.moveOffset);
                return value;
            } else {
                return Vector3.zero;
            }
        }

        public Vector3 GetDollyLookOffset() {
            bool has = TryGetCurrentDolly(out var dolly);
            if (has) {
                float t = Mathf.Min(time, dolly.lookMaintainTime);
                Vector3 value = EasingHelper.Ease3D(dolly.lookEasingType, t, dolly.lookMaintainTime, Vector3.zero, dolly.lookOffset);
                return value;
            } else {
                return Vector3.zero;
            }
        }

        public float GetDollyZoomInOffset() {
            bool has = TryGetCurrentDolly(out var dolly);
            if (has) {
                float t = Mathf.Min(time, dolly.zoomInMaintainTime);
                float value = EasingHelper.Ease1D(dolly.zoomInEasingType, t, dolly.zoomInMaintainTime, 0, dolly.zoomInOffset);
                return value;
            } else {
                return 0;
            }
        }

        public void Reset() {
            dollyPoints.Clear();
            dollyIndex = 0;
        }

    }

}