using System.Collections.Generic;
using UnityEngine;
using JackEasing;

namespace TripodCamera {

    public class TCCameraTrackComponent {

        // ==== Locomotion ====
        List<TCDollyPointModel> dollyPoints;
        int dollyIndex;
        
        Vector3 dollyPos;
        Vector3 dollyRot;
        float dollyZoom;

        float time;

        public TCCameraTrackComponent() {
            this.dollyPoints = new List<TCDollyPointModel>();
        }

        public void AddDollyPoint(TCDollyPointModel dollyPoint) {
            dollyPoints.Add(dollyPoint);
        }

        public void TickDolly(float dt) {
            bool has = TryGetCurrentDolly(out var dolly);
            if (has) {
                time += dt;
                dollyPos = EasingHelper.Ease3D(dolly.moveEasingType, time, dolly.moveMaintainTime, dolly.moveOffset, Vector3.zero);
                dollyRot = EasingHelper.Ease3D(dolly.lookEasingType, time, dolly.lookMaintainTime, dolly.lookOffset, Vector3.zero);
                dollyZoom = EasingHelper.Ease1D(dolly.zoomInEasingType, time, dolly.zoomInMaintainTime, dolly.zoomInOffset, 0);
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
                return dollyPos;
            } else {
                return Vector3.zero;
            }
        }

        public Vector3 GetDollyLookOffset() {
            bool has = TryGetCurrentDolly(out var dolly);
            if (has) {
                return dollyRot;
            } else {
                return Vector3.zero;
            }
        }

        public float GetDollyZoomInOffset() {
            bool has = TryGetCurrentDolly(out var dolly);
            if (has) {
                return dollyZoom;
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