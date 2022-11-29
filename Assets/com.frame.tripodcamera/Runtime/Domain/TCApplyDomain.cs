using UnityEngine;
using TripodCamera.Facades;

namespace TripodCamera.Domain {

    internal class TCApplyDomain {

        TCFacades facades;

        internal TCApplyDomain() {}

        internal void Inject(TCFacades facades) {
            this.facades = facades;
        }

        internal void ApplyFollow(TCCameraEntity tcCam, float dt) {
            tcCam.FollowComponent.TickEasing(dt);
        }

        internal void ApplyEffect(TCCameraEntity tcCam, float dt) {
            tcCam.Effect.TickDolly(dt);
        }

        internal void ApplyToMain(TCCameraEntity tcCam, Camera mainCam) {

            var info = tcCam.CurrentInfo;

            // - Pos
            Vector3 pos;
            if (tcCam.IsFollowing()) {
                pos = tcCam.FollowComponent.GetFollowPos();
            } else {
                pos = info.Pos;
            }

            // - Rot
            Quaternion rot;
            if (tcCam.IsLookingAt()) {
                rot = tcCam.LookAtComponent.GetLookAtRotation(pos, info.Rot);
            } else {
                rot = info.Rot;
            }

            // - Set
            info.SetPos(pos);
            info.SetRot(rot);

            // - Effect Pos & Rot
            var effect = tcCam.Effect;
            Vector3 posAddition = effect.GetDollyMoveOffset();
            Vector3 rotAddition = effect.GetDollyLookOffset();

            // - Apply
            mainCam.transform.position = pos + posAddition;
            mainCam.transform.rotation = rot * Quaternion.Euler(rotAddition);
            mainCam.fieldOfView = info.FOV;

        }

    }

}