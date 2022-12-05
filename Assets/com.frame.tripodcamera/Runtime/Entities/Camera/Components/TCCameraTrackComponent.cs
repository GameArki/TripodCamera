using System.Collections.Generic;
using UnityEngine;
using JackEasing;

namespace TripodCamera.Entities {

    public class TCCameraTrackComponent {

        // ==== Locomotion ====
        List<TCDollyStateModel> dollyPoints;
        int dollyIndex;

        Vector3 dollyPos;
        Vector3 dollyRot;
        float dollyZoom;

        float time;

        public TCCameraTrackComponent() {
            this.dollyPoints = new List<TCDollyStateModel>();
        }

        public void AddDollyPoint(TCDollyStateModel dollyPoint) {
            dollyPoints.Add(dollyPoint);
        }

        public void Tick(float dt) {
            bool has = TryGetCurrentDolly(out var dolly);
            if (has) {
                time += dt;
                dollyPos = EasingHelper.Ease3D(dolly.moveEasingType, time, dolly.moveMaintainTime, Vector3.zero, dolly.moveOffset);
                dollyRot = EasingHelper.Ease3D(dolly.lookEasingType, time, dolly.lookMaintainTime, Vector3.zero, dolly.lookOffset);
                dollyZoom = EasingHelper.Ease1D(dolly.zoomInEasingType, time, dolly.zoomInMaintainTime, dolly.zoomInOffset, 0);
                if (time > dolly.MaintainTimeMax) {
                    time = 0;
                    dollyIndex += 1;
                }
            }
        }

        bool TryGetCurrentDolly(out TCDollyStateModel model) {
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