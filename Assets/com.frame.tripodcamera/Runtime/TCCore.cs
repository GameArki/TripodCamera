using UnityEngine;
using TripodCamera.Facades;
using TripodCamera.Domain;
using TripodCamera.Controller;
using TripodCamera.API;

namespace TripodCamera {

    public class TCCore {

        bool isInit;

        // ==== API ====
        TCSetterAPI setterAPI;
        public ITCSetterAPI SetterAPI => setterAPI;

        // ==== Facades ====
        TCFacades facades;
        TCDomain domain;

        // ==== Controller ====
        TCInitializePhase initializePhase;
        TCapplyPhase applyPhase;

        public TCCore() {

            this.isInit = false;

            this.setterAPI = new TCSetterAPI();

            this.facades = new TCFacades();
            this.domain = new TCDomain();

            this.initializePhase = new TCInitializePhase();
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
            applyPhase.Inject(facades, domain);

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