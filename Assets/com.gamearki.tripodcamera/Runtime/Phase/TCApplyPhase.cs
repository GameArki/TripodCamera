using UnityEngine;
using GameArki.TripodCamera.Facades;
using GameArki.TripodCamera.Domain;

namespace GameArki.TripodCamera.Controller {

    internal class TCapplyPhase {

        TCFacades facades;
        TCDomain domain;

        internal TCapplyPhase() { }

        internal void Inject(TCFacades facades, TCDomain domain) {
            this.facades = facades;
            this.domain = domain;
        }

        internal void Tick(float dt) {

            var applyDomain = domain.ApplyDomain;

            var hooks = facades.HookRepo.GetAll();
            foreach (var hook in hooks) {
                applyDomain.ApplyHook(hook);
            }

            var tcCam = facades.CameraRepo.ActiveCam;
            applyDomain.ApplyFollow(tcCam, dt);
            applyDomain.ApplyTrackState(tcCam, dt);
            applyDomain.ApplyShakeState(tcCam, dt);
            applyDomain.ApplyMoveState(tcCam, dt);
            applyDomain.ApplyRoundState(tcCam, dt);
            applyDomain.ApplyRotateState(tcCam, dt);
            applyDomain.ApplyPushState(tcCam, dt);

            var mainCam = facades.MainCamera;
            applyDomain.ApplyToMain(tcCam, mainCam);

        }

    }

}