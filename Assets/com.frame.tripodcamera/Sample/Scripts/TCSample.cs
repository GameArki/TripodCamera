using UnityEngine;

namespace TripodCamera.Sample {

    public class TCSample : MonoBehaviour {

        TCCore tcCore;

        void Awake() {
            tcCore = new TCCore();
            tcCore.Initialize(Camera.main);
        }

        void Update() {
            tcCore.SetterAPI.PushInCurrent(0.01f);
        }

        void LateUpdate() {
            tcCore.Tick();
        }

    }
    
}