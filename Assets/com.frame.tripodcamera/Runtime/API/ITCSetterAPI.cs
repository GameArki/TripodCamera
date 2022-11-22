using UnityEngine;

namespace TripodCamera.API {

    public interface ITCSetterAPI {

        // ==== Basic ====
        void PushInCurrent(float value);
        void MoveCurrent(Vector2 value);
        void RotateHorizontalCurrent(float x);
        void RotateVerticalCurrent(float y);
        void RotateRollCurrent(float z);
        void ZoomInCurrent(float value);

        // ==== Advanced ====
        void SetFollowCurrent(Transform target, Vector3 offset);

    }

}