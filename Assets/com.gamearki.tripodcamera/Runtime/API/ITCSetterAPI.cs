using UnityEngine;
using GameArki.FPEasing;
using GameArki.TripodCamera.Hook;

namespace GameArki.TripodCamera.API {

    public interface ITCSetterAPI {

        // ==== Spawn ====
        void Spawn(int id, Vector3 position, Quaternion rotation, float fov);
        void SpawnByMain(int id);
        void CutTo(int id, EasingType easingType, float duration);
        TCCameraHook GetHook(int id);

        // ==== Basic ====
        void Push_In_Current(float value);

        void Move_Current(Vector2 value);
        void Move_AndChangeLookAtOffset_Current(Vector2 value);

        void Rotate_Horizontal_Current(float x);
        void Rotate_Vertical_Current(float y);
        void Rotate_Roll_Current(float z);
        void Round_Current(Vector2 value);

        void Zoom_In_Current(float value);

        // ==== Advanced ====
        void Follow_SetInit_Current(Transform target, Vector3 offset, EasingType easingType, float easingTime);
        void Follow_SetInit_Current(Transform target, Vector3 offset, EasingType easingType_horizontal, float easingTime_horizontal, EasingType easingType_vertical, float easingTime_vertical);
        void Follow_ChangeTarget_Current(Transform target);
        void Follow_ChangeOffset_Current(Vector3 offset);

        void LookAt_SetInit_Current(Transform target, Vector3 offset);
        void LookAt_ChangeTarget_Current(Transform target);
        void LookAt_ChangeOffset_Current(Vector3 offset);

        // ==== Shake ====
        void Enter_Shake_Current(TCShakeStateModel[] args);
        void Enter_Move_Current(TCMovementStateModel[] args, bool isExitReset);
        void Enter_Move_Current(TCMovementStateModel[] args, bool isExitReset, EasingType exitEasingType, float exitDuration);
        void Enter_Round_Current(TCRoundStateModel[] args, bool isExitReset, EasingType exitEasingType, float exitDuration);
        void Enter_Push_Current(TCPushStateModel[] args, bool isExitReset, EasingType exitEasingType, float exitDuration);
        void Enter_Rotation_Current(TCRotationStateModel[] args, bool isExitReset, EasingType exitEasingType, float exitDuration);
    }

}