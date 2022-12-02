using UnityEngine;
using JackEasing;

namespace TripodCamera {

    public class TCCameraShakeStateComponent {

        // Args
        TCShakeStateArgs[] args;
        int index;

        // Temp
        Vector3 resOffset;
        float time;

        public TCCameraShakeStateComponent() { }

        public void SetShake(TCShakeStateArgs[] args) {
            this.args = args;
            this.index = 0;
            this.time = 0;
        }

        public void Tick(float dt) {

            if (args == null || index >= args.Length) {
                return;
            }

            time += dt;

            var cur = args[index];
            float x = WaveHelper.SinWaveReductionEasing(cur.easingType, time, cur.duration, cur.amplitudeOffset.x, cur.frequency, 0);
            float y = WaveHelper.SinWaveReductionEasing(cur.easingType, time, cur.duration, cur.amplitudeOffset.y, cur.frequency, 0);
            resOffset = new Vector3(x, y);

            if (time >= cur.duration) {
                time = 0;
                index += 1;
                resOffset = Vector3.zero;
            }

        }

        public Vector3 GetShakeOffset() {
            return resOffset;
        }

    }

}