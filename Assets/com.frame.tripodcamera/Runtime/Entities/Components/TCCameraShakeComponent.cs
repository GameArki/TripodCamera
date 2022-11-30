using UnityEngine;
using JackEasing;

namespace TripodCamera {

    public class TCCameraShakeComponent {

        Vector2 amplitudeOffset;
        float frequency;
        float duration;

        float time;

        public TCCameraShakeComponent() { }

        public void SetShake(Vector2 amplitudeOffset, float frequency, float duration) {
            this.amplitudeOffset = amplitudeOffset;
            this.frequency = frequency;
            this.duration = duration;
            this.time = 0;
        }

        public void TickShake(float dt) {

            if (duration == 0 || time >= duration) {
                return;
            }

            time += dt;
            if (time >= duration) {
                time = duration;
            }

        }

        public Vector3 GetShakeOffset() {
            if (duration == 0 || time >= duration) {
                return Vector3.zero;
            }

            float x = WaveHelper.SinWaveReduction(time, duration, amplitudeOffset.x, frequency, 0);
            float y = WaveHelper.SinWaveReduction(time, duration, amplitudeOffset.y, frequency, 0);
            return new Vector3(x, y, 0);
        }

    }

}