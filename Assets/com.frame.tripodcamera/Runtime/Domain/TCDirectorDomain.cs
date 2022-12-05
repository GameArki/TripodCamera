using UnityEngine;
using JackEasing;
using TripodCamera.Facades;
using TripodCamera.Entities;

namespace TripodCamera.Domain {

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