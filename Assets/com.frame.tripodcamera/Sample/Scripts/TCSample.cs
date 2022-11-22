using UnityEngine;

namespace TripodCamera.Sample {

    public class TCSample : MonoBehaviour {

        TCCore tcCore;

        void Awake() {
            tcCore = new TCCore();
            tcCore.Initialize(Camera.main);
        }

        void Update() {

            var tcSetter = tcCore.SetterAPI;

            var mouseScroll = Input.mouseScrollDelta.y;
            if (Input.GetKey(KeyCode.LeftControl)) {
                if (mouseScroll != 0) {
                    tcSetter.ZoomInCurrent(mouseScroll);
                }
            } else {
                tcSetter.PushInCurrent(mouseScroll);
            }

            float x = Input.GetAxis("Horizontal");
            float y = Input.GetAxis("Vertical");
            if (x != 0 || y != 0) {
                tcSetter.Move(new Vector2(x, y));
            }

        }

        void LateUpdate() {
            tcCore.Tick();
        }

    }

}