using UnityEngine;
using TripodCamera.Facades;

namespace TripodCamera {

    public class TCCore {

        bool isInit;

        TCFacades facades;

        TCInitializePhase initializePhase;
        TCapplyPhase applyPhase;

        public TCCore() {

            this.isInit = false;

            this.facades = new TCFacades();

            this.initializePhase = new TCInitializePhase();
            this.applyPhase = new TCapplyPhase();

        }

        public void Initialize(Camera mainCamera) {

            // ==== Inject ====
            facades.Inject(mainCamera);

            initializePhase.Inject(facades);
            applyPhase.Inject(facades);

            // ==== Init ====
            initializePhase.Init();

            isInit = true;

        }

        /// <summary>
        /// Recommended to call this method in "LateUpdate()" or "end of Update()"
        /// </summary>
        public void Tick() {

            if (!isInit) {
                return;
            }

            applyPhase.Tick();

        }

    }

}