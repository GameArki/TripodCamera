using UnityEngine;

namespace TripodCamera.API {

    public interface ITCSetterAPI {

        void PushInCurrent(float value);
        void Move(Vector2 value);
        void ZoomInCurrent(float value);

    }

}