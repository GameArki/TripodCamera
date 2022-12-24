using GameArki.TripodCamera.Domain;
using GameArki.TripodCamera.Facades;

namespace GameArki.TripodCamera.Controller {

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