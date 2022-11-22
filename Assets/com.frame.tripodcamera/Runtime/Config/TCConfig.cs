namespace TripodCamera {

    public class TCConfig {

        float fovMin;
        public float FOVMin => fovMin;

        float fovMax;
        public float FOVMax => fovMax;
        
        public TCConfig() {}

        public void SetFOV(float min, float max) {
            this.fovMin = min;
            this.fovMax = max;
        }

    }

}