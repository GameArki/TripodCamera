using UnityEngine;
using GameArki.FPEasing;
using GameArki.TripodCamera.Facades;
using GameArki.TripodCamera.Entities;

namespace GameArki.TripodCamera.Domain {

    public class TCDirectorDomain {

        TCFacades facades;

        public TCDirectorDomain() { }

        public void Inject(TCFacades facades) {
            this.facades = facades;
        }

        public void CutTo(int targetID, EasingType easingType, float duration) {
            // Reset Cur States
            // Change Active Camera: Cur -> Target
            // Enter Director FSM
        }

    }

}