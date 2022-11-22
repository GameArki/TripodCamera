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

        void ITCSetterAPI.ZoomInCurrent(float value) {
            var cameraDomain = domain.CameraDomain;
            cameraDomain.ZoomInCurrent(value);
        }

    }

}