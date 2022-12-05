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

        }

    }
}