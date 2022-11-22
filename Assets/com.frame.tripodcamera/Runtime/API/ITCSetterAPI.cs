using UnityEngine;

namespace TripodCamera.API {

    public interface ITCSetterAPI {

        void PushInCurrent(float value);
        void Move(Vector2 value);
        void RotateHorizontal(float x);
        void RotateVertical(float y);
        void RotateRoll(float z);
        void ZoomInCurrent(float value);

    }

}