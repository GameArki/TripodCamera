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
            tcCam.TrackComponent.TickDolly(dt);
        }

        internal void ApplyShake(TCCameraEntity tcCam, float dt) {
            tcCam.ShakeComponent.TickShake(dt);
        }

        internal void ApplyToMain(TCCameraEntity tcCam, Camera mainCam) {

            var info = tcCam.CurrentInfoComponent;

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

            // - Track Pos & Rot
            var track = tcCam.TrackComponent;
            Vector3 trackPosAddition = track.GetDollyMoveOffset();
            Vector3 trackRotAddition = track.GetDollyLookOffset();

            // - Shake
            Vector3 shakePosAddition = tcCam.ShakeComponent.GetShakeOffset();

            // - Apply
            mainCam.transform.position = pos + trackPosAddition + shakePosAddition;
            mainCam.transform.rotation = rot * Quaternion.Euler(trackRotAddition);
            mainCam.fieldOfView = info.FOV;

        }

    }

}