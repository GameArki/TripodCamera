using TripodCamera.Domain;
using TripodCamera.Facades;

namespace TripodCamera.Controller {

    public class TCStatePhase {

        TCFacades facades;
        TCDomain domain;

        public TCStatePhase() { }

        public void Inject(TCFacades facades, TCDomain domain) {
            this.facades = facades;
            this.domain = domain;
        }

        public void Tick(float dt) {
        }

        void NormalState() {
        }

    }

}