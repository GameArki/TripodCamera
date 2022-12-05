using UnityEngine;
using TripodCamera.Facades;
using TripodCamera.Domain;
using TripodCamera.Controller;
using TripodCamera.API;

namespace TripodCamera {

    public class TCCore {

        bool isInit;
        
        bool isPause;
        public bool IsPause => isPause;

        // ==== API ====
        TCSetterAPI setterAPI;
        public ITCSetterAPI SetterAPI => setterAPI;

        // ==== Facades ====
        TCFacades facades;
        TCDomain domain;

        // ==== Controller ====
        TCInitializePhase initializePhase;
        TCStatePhase statePhase;
        TCapplyPhase applyPhase;

        public TCCore() {

            this.isInit = false;
            this.isPause = false;

            this.setterAPI = new TCSetterAPI();

            this.facades = new TCFacades();
            this.domain = new TCDomain();

            this.initializePhase = new TCInitializePhase();
            this.statePhase = new TCStatePhase();
            this.applyPhase = new TCapplyPhase();

        }

        public void Initialize(Camera mainCamera) {

            // ==== Inject ====
            // - API
            setterAPI.Inject(facades, domain);

            // - Facades
            facades.Inject(mainCamera);
            domain.Inject(facades);

            // - Controller
            initializePhase.Inject(facades, domain);
            statePhase.Inject(facades, domain);
            applyPhase.Inject(facades, domain);

            // ==== Init ====
            initializePhase.Init();

            isInit = true;

        }

        public void Pause() {
            isPause = true;
        }

        public void Resume() {
            isPause = false;
        }

        /// <summary>
        /// Recommended to call this method in "LateUpdate()" or "end of Update()"
        /// </summary>
        public void Tick(float dt) {

            if (!isInit) {
                return;
            }

            if (isPause) {
                return;
            }

            statePhase.Tick(dt);
            applyPhase.Tick(dt);

        }

        // ==== Unsafe API ====
        public TCFacades GetFacadesThatYouCanVisitEverythingButNotRecommended() {
            return facades;
        }

        public TCDomain GetDomainThatYouCanVisitEverythingButNotRecommended() {
            return domain;
        }

    }

}