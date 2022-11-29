using TripodCamera.Facades;
using TripodCamera.Domain;

namespace TripodCamera {

    internal class TCInitializePhase {

        TCFacades facades;
        TCDomain domain;

        internal TCInitializePhase() {}

        internal void Inject(TCFacades facades, TCDomain domain) {
            this.facades = facades;
            this.domain = domain;
        }

        internal void Init() {

            // ==== Config ====
            var config = facades.Config;
            config.SetFOV(1, 179);

            // ==== First Camera ====
            var cameraDomain = domain.CameraDomain;
            var cam = facades.MainCamera;
            var tf = cam.transform;
            cameraDomain.Spawn(tf.position, tf.rotation, cam.fieldOfView);

        }

    }
}