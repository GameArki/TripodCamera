using UnityEngine;
using TripodCamera.Facades;

namespace TripodCamera.Domain {

    internal class TCApplyDomain {

        TCFacades facades;

        internal TCApplyDomain() { }

        internal void Inject(TCFacades facades) {
            this.facades = facades;
        }

        internal void ApplyFollow(TCCameraEntity tcCam, float dt) {
            tcCam.FollowComponent.TickEasing(dt);
        }

        // ==== State ====
        // - Track
        internal void ApplyTrackState(TCCameraEntity tcCam, float dt) {
            tcCam.TrackComponent.Tick(dt);
        }

        // - Shake
        internal void ApplyShakeState(TCCameraEntity tcCam, float dt) {
            tcCam.ShakeComponent.Tick(dt);
        }

        // - Move
        internal void ApplyMoveState(TCCameraEntity tcCam, float dt) {
            tcCam.MovementStateComponent.Tick(dt);
        }

        // - Rotate
        internal void ApplyRotateState(TCCameraEntity tcCam, float dt) {
            tcCam.RotateStateComponent.Tick(dt);
        }

        // - Push
        internal void ApplyPushState(TCCameraEntity tcCam, float dt) {
            tcCam.PushStateComponent.Tick(dt);
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

            // - Shake State
            Vector3 shakePosAddition = tcCam.ShakeComponent.GetShakeOffset();

            // - Move State
            Vector3 moveAddition = tcCam.MovementStateComponent.GetMoveOffset();

            // - Rotate State
            Vector3 rotateAddition = tcCam.RotateStateComponent.GetRotateOffset();

            rot = rot * Quaternion.Euler(trackRotAddition) * Quaternion.Euler(rotateAddition);

            // - Push State
            float pushAddition = tcCam.PushStateComponent.GetPushOffset();
            var fwd = rot * Vector3.forward;
            fwd *= pushAddition;

            // - Apply
            mainCam.transform.rotation = rot;
            mainCam.transform.position = pos + trackPosAddition + shakePosAddition + moveAddition + fwd;

            mainCam.fieldOfView = info.FOV;

        }

    }

}