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
            var lookAtComponent = tcCam.LookAtComponent;
            var followComponent = tcCam.FollowComponent;
            var lookAtTF = lookAtComponent.LookAtTF;
            var followTF = followComponent.FollowTF;

            // - Pos
            bool hasLookAt = tcCam.IsLookingAt();
            Vector3 pos = info.Pos;
            Vector3 forward = Vector3.forward;
            if (hasLookAt && lookAtTF != followTF) {
                forward = (lookAtTF.position - pos);
                forward.y = 0;
                forward.Normalize();
            }
            if (tcCam.IsFollowing()) {
                pos = followComponent.GetFollowPos(forward);
            }
            info.SetPos(pos);

            // - Rot
            Quaternion rot;
            if (hasLookAt) {
                rot = lookAtComponent.GetLookAtRotation(pos, info.Rot);
            } else {
                rot = info.Rot;
            }
            info.SetRot(rot);

            // - FOV
            mainCam.fieldOfView = info.FOV;

            // *********************************** Start Position Caculation 

            Vector3 resPos = pos;
            // - DollyTrack
            var track = tcCam.TrackComponent;
            Vector3 trackPosAddition = track.GetDollyMoveOffset();
            resPos += trackPosAddition;
            // - Shake State
            Vector3 shakePosAddition = tcCam.ShakeComponent.GetShakeOffset();
            resPos += shakePosAddition;
            // - Move State
            Vector3 moveAddition = tcCam.MovementStateComponent.GetMoveOffset();
            resPos += moveAddition;
            // - Push State
            float pushAddition = tcCam.PushStateComponent.GetPushOffset();
            var fwd = rot * Vector3.forward;
            fwd *= pushAddition;
            resPos += fwd;
            // - Round State
            var rsc = tcCam.RoundStateComponent;
            var roundPosOffset = rsc.GetRoundPosOffset(resPos);
            resPos += roundPosOffset;
            // - Set
            mainCam.transform.position = resPos;

            // *********************************** End Position Caculation 

            // *********************************** Start Rotation Caculation 

            // - LookAt
            if (hasLookAt) {
                rot = lookAtComponent.GetLookAtRotation(resPos, info.Rot);
            }
            // - Rotate State
            Vector3 trackRotAddition = track.GetDollyLookOffset();
            Vector3 rotateAddition = tcCam.RotateStateComponent.GetRotateOffset();
            rot = rot * Quaternion.Euler(trackRotAddition) * Quaternion.Euler(rotateAddition);
            // - Set
            mainCam.transform.rotation = rot;

            // *********************************** End Rotation Caculation 

        }

    }

}