using UnityEngine;
using JackEasing;

namespace TripodCamera {

    public struct TCRotationStateArgs {

        public Vector3 offset;
        public float duration;
        
        public EasingType easingType;
        public bool isInherit;

    }
}