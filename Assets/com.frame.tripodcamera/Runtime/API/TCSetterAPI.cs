using UnityEngine;
using TripodCamera.Facades;
using TripodCamera.Domain;

namespace TripodCamera.API {

    internal class TCSetterAPI : ITCSetterAPI {

        TCFacades facades;
        TCDomain domain;

        internal TCSetterAPI() { }

        internal void Inject(TCFacades facades, TCDomain domain) {
            this.facades = facades;
            this.domain = domain;
        }

        // ==== Basic ====
        void ITCSetterAPI.PushInCurrent(float value) {
            var cameraDomain = domain.CameraDomain;
            cameraDomain.PushInCurrent(value);
        }

        void ITCSetterAPI.MoveCurrent(Vector2 value) {
            var cameraDomain = domain.CameraDomain;
            cameraDomain.MoveCurrent(value);
        }

        void ITCSetterAPI.RotateHorizontalCurrent(float x) {
            var cameraDomain = domain.CameraDomain;
            cameraDomain.RotateHorizontalCurrent(x);
        }

        void ITCSetterAPI.RotateVerticalCurrent(float y) {
            var cameraDomain = domain.CameraDomain;
            cameraDomain.RotateVerticalCurrent(y);
        }

        void ITCSetterAPI.RotateRollCurrent(float z) {
            var cameraDomain = domain.CameraDomain;
            cameraDomain.RotateRollCurrent(z);
        }

        void ITCSetterAPI.ZoomInCurrent(float value) {
            var cameraDomain = domain.CameraDomain;
            cameraDomain.ZoomInCurrent(value);
        }

        // ==== Advanced ====
        void ITCSetterAPI.SetFollowCurrent(Transform target, Vector3 offset) {
            var cameraDomain = domain.CameraDomain;
            cameraDomain.SetFollowCurrent(target, offset);
        }

    }

}