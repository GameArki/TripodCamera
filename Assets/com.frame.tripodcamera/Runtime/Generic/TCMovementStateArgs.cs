using UnityEngine;
using JackEasing;

namespace TripodCamera {

    public struct TCMovementStateArgs {

        public Vector2 offset;
        public float duration;
        public EasingType easingType;
        public bool isInherit;

    }
}