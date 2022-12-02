using UnityEngine;
using JackEasing;

namespace TripodCamera.API {

    public interface ITCSetterAPI {

        // ==== Basic ====
        void Push_In_Current(float value);

        void Move_Current(Vector2 value);
        void Move_AndChangeLookAtOffset_Current(Vector2 value);

        void Rotate_Horizontal_Current(float x);
        void Rotate_Vertical_Current(float y);
        void Rotate_Roll_Current(float z);

        void Zoom_In_Current(float value);

        // ==== Advanced ====
        void Follow_SetInit_Current(Transform target, Vector3 offset, EasingType easingType, float easingTime);
        void Follow_ChangeTarget_Current(Transform target);
        void Follow_ChangeOffset_Current(Vector3 offset);

        void LookAt_SetInit_Current(Transform target, Vector3 offset);
        void LookAt_ChangeTarget_Current(Transform target);
        void LookAt_ChangeOffset_Current(Vector3 offset);

        // ==== Shake ====
        void Enter_Shake_Current(TCShakeStateArgs[] args);
        void Enter_Move_Current(TCMovementStateArgs[] args);
        void Enter_Move_Current(TCMovementStateArgs[] args, EasingType exitEasingType, float exitDuration);
        void Enter_Push_Current(TCPushStateArgs[] args, EasingType exitEasingType, float exitDuration);
        void Enter_Rotation_Current(TCRotationStateArgs[] args, EasingType exitEasingType, float exitDuration);

    }

}