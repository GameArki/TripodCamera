using UnityEngine;

namespace TripodCamera.Sample {

    public class TCSample : MonoBehaviour {

        TCCore tcCore;

        Vector2 mousePos;

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
            } else if (Input.GetKey(KeyCode.LeftShift)) {
                tcSetter.PushInCurrent(mouseScroll);
            } else if (Input.GetKey(KeyCode.LeftAlt)) {
                if (mouseScroll != 0) {
                    tcSetter.RotateRoll(mouseScroll);
                }
            }

            float x = Input.GetAxis("Horizontal");
            float y = Input.GetAxis("Vertical");
            if (x != 0 || y != 0) {
                tcSetter.Move(new Vector2(x, y));
            }

            if (Input.GetMouseButton(1)) {
                var mouseDelta = (Vector2)Input.mousePosition - mousePos;
                if (mouseDelta != Vector2.zero) {
                    tcSetter.RotateHorizontal(mouseDelta.x);
                    tcSetter.RotateVertical(mouseDelta.y);
                }
            }

            mousePos = Input.mousePosition;

        }

        void LateUpdate() {
            tcCore.Tick();
        }

    }

}