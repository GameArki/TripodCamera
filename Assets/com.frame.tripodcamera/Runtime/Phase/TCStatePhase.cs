using TripodCamera.Domain;
using TripodCamera.Facades;

namespace TripodCamera.Controller {

    internal class TCStatePhase {

        TCFacades facades;
        TCDomain domain;

        internal TCStatePhase() { }

        internal void Inject(TCFacades facades, TCDomain domain) {
            this.facades = facades;
            this.domain = domain;
        }

        internal void Tick(float dt) {
        }

        void NormalState() {
        }

    }

}