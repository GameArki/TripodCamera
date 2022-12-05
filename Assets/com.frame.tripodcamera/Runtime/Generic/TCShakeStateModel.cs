using UnityEngine;
using JackEasing;

namespace TripodCamera {

    public struct TCShakeStateModel {

        public Vector2 amplitudeOffset;
        public EasingType easingType;
        public float frequency;
        public float duration;
    }
}