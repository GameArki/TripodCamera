using UnityEngine;
using GameArki.FPEasing;

namespace GameArki.TripodCamera {

    public struct TCRoundStateModel {

        public Transform tar;
        public Vector2 offset;
        public float duration;
        public EasingType easingType;
        public bool isInherit;

    }
}