using UnityEngine;
using GameArki.TripodCamera.Entities;
using GameArki.FPEasing;

namespace GameArki.TripodCamera.Hook {

    public class TCCameraHook : MonoBehaviour {

        TCCameraEntity entity;

        [Header("Follow")]
        [SerializeField] Vector3 followOffset;
        [SerializeField] EasingType followXEasing;
        [SerializeField] float followXDuration;
        [SerializeField] EasingType followYEasing;
        [SerializeField] float followYDuration;

        [Header("LookAt")]
        [SerializeField] Vector3 lookatOffset;

        [SerializeField] bool enableChange;

        public void Ctor(TCCameraEntity entity) {

            this.entity = entity;

        }

        public void Tick() {
            if (enableChange) {
                ApplyToCam();
            } else {
                RecordFromCam();
            }
        }

        void RecordFromCam() {

            var followCom = entity.FollowComponent;
            followOffset = followCom.FollowOffset;

            followXEasing = entity.FollowComponent.XEasing;
            followXDuration = entity.FollowComponent.XDuration;

            followYEasing = entity.FollowComponent.YEasing;
            followYDuration = entity.FollowComponent.YDuration;

            var lookatCom = entity.LookAtComponent;
            lookatOffset = lookatCom.LookAtOffset;

        }

        void ApplyToCam() {
            
            entity.Follow_ChangeOffset(followOffset);
            entity.Follow_ChangeXEasing(followXEasing, followXDuration);
            entity.Follow_ChangeYEasing(followYEasing, followYDuration);

            entity.LookAt_ChangeOffset(lookatOffset);

        }

    }

}