using UnityEngine;
using TripodCamera.Facades;
using TripodCamera.Entities;

namespace TripodCamera.Domain {

    public class TCApplyDomain {

        TCFacades facades;

        public TCApplyDomain() { }

        public void Inject(TCFacades facades) {
            this.facades = facades;
        }

        public void ApplyFollow(TCCameraEntity tcCam, float dt) {
            tcCam.FollowComponent.TickEasing(dt);
        }

        // ==== State ====
        // - Track
        public void ApplyTrackState(TCCameraEntity tcCam, float dt) {
            tcCam.TrackComponent.Tick(dt);
        }

        // - Shake
        public void ApplyShakeState(TCCameraEntity tcCam, float dt) {
            tcCam.ShakeComponent.Tick(dt);
        }

        // - Move
        public void ApplyMoveState(TCCameraEntity tcCam, float dt) {
            tcCam.MovementStateComponent.Tick(dt);
        }

        // - Round
        public void ApplyRoundState(TCCameraEntity tcCam, float dt) {
            tcCam.RoundStateComponent.Tick(dt);
        }

        // - Rotate
        public void ApplyRotateState(TCCameraEntity tcCam, float dt) {
            tcCam.RotateStateComponent.Tick(dt);
        }

        // - Push
        public void ApplyPushState(TCCameraEntity tcCam, float dt) {
            tcCam.PushStateComponent.Tick(dt);
        }

        public void ApplyToMain(TCCameraEntity tcCam, Camera mainCam) {

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

            // # Round Calculation
            var resPos = pos + trackPosAddition + shakePosAddition + moveAddition + fwd;
            var rsc = tcCam.RoundStateComponent;
            var roundPosOffset = rsc.GetRoundPosOffset(resPos);
            resPos += roundPosOffset;

            mainCam.transform.position = resPos;
            var lookAtComponent = tcCam.LookAtComponent;
            if (lookAtComponent.IsLookingAt()) {
                rot = lookAtComponent.GetLookAtRotation(resPos, info.Rot);
            }
            mainCam.transform.rotation = rot;
            mainCam.fieldOfView = info.FOV;

        }

    }

}