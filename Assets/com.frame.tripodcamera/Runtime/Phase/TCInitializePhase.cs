using TripodCamera.Facades;
using TripodCamera.Domain;

namespace TripodCamera {

    public class TCInitializePhase {

        TCFacades facades;
        TCDomain domain;

        public TCInitializePhase() {}

        public void Inject(TCFacades facades, TCDomain domain) {
            this.facades = facades;
            this.domain = domain;
        }

        public void Init() {

            var cameraDomain = domain.CameraDomain;
            var cam = facades.MainCamera;
            var tf = cam.transform;
            cameraDomain.Spawn(tf.position, tf.rotation, cam.fieldOfView);
            
        }

    }
}