using UnityEngine;
using TripodCamera.Facades;
using TripodCamera.Domain;

namespace TripodCamera.Controller {

    internal class TCapplyPhase {

        TCFacades facades;
        TCDomain domain;

        internal TCapplyPhase() {}

        internal void Inject(TCFacades facades, TCDomain domain) {
            this.facades = facades;
            this.domain = domain;
        }

        internal void Tick(float dt) {
            var mainCam = facades.MainCamera;
            var tcCam = facades.CameraRepo.ActiveCam;
            var applyDomain = domain.ApplyDomain;
            applyDomain.ApplyFollow(tcCam, dt);
            applyDomain.ApplyEffect(tcCam, dt);
            applyDomain.ApplyToMain(tcCam, mainCam);
        }

    }

}