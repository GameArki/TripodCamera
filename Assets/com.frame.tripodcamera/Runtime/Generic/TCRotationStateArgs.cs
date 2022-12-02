using UnityEngine;
using JackEasing;

namespace TripodCamera {

    public struct TCRotationStateArgs {

        public Vector3 angle;
        public float maintainTime;
        public EasingType easingType;
        public bool isInherit;

    }
}