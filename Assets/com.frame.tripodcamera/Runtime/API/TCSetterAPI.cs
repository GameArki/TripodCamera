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

        void ITCSetterAPI.PushInCurrent(float value) {
            var cameraDomain = domain.CameraDomain;
            cameraDomain.PushInCurrent(value);
        }

        void ITCSetterAPI.Move(Vector2 value) {
            var cameraDomain = domain.CameraDomain;
            cameraDomain.Move(value);
        }

        void ITCSetterAPI.RotateHorizontal(float x) {
            var cameraDomain = domain.CameraDomain;
            cameraDomain.RotateHorizontal(x);
        }

        void ITCSetterAPI.RotateVertical(float y) {
            var cameraDomain = domain.CameraDomain;
            cameraDomain.RotateVertical(y);
        }

        void ITCSetterAPI.RotateRoll(float z) {
            var cameraDomain = domain.CameraDomain;
            cameraDomain.RotateRoll(z);
        }

        void ITCSetterAPI.ZoomInCurrent(float value) {
            var cameraDomain = domain.CameraDomain;
            cameraDomain.ZoomInCurrent(value);
        }

    }

}