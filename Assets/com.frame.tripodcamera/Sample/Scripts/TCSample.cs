using UnityEngine;

namespace TripodCamera.Sample {

    public class TCSample : MonoBehaviour {

        TCCore tcCore;

        void Awake() {
            tcCore = new TCCore();
            tcCore.Initialize(Camera.main);
        }

        void Update() {
            
            var mouseScroll = Input.mouseScrollDelta.y;
            if (Input.GetKey(KeyCode.LeftControl)) {
                if (mouseScroll != 0) {
                    tcCore.SetterAPI.ZoomInCurrent(mouseScroll);
                }
            } else {
                tcCore.SetterAPI.PushInCurrent(mouseScroll);
            }

        }

        void LateUpdate() {
            tcCore.Tick();
        }

    }

}