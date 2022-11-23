using UnityEngine;

namespace TripodCamera.Sample {

    public class TCSample : MonoBehaviour {

        TCCore tcCore;

        Vector2 mousePos;

        GameObject target;

        void Awake() {
            tcCore = new TCCore();
            tcCore.Initialize(Camera.main);

            target = GameObject.CreatePrimitive(PrimitiveType.Cube);
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
                    tcSetter.RotateRollCurrent(mouseScroll);
                }
            }

            float x = Input.GetAxis("Horizontal");
            float y = Input.GetAxis("Vertical");
            if (x != 0 || y != 0) {
                tcSetter.MoveCurrent(new Vector2(x, y));
            }

            if (Input.GetMouseButton(1)) {
                var mouseDelta = (Vector2)Input.mousePosition - mousePos;
                if (mouseDelta != Vector2.zero) {
                    tcSetter.RotateHorizontalCurrent(mouseDelta.x);
                    tcSetter.RotateVerticalCurrent(mouseDelta.y);
                }
            }

            mousePos = Input.mousePosition;

            if (Input.GetKeyUp(KeyCode.Space)) {
                // tcSetter.SetFollowCurrent(target.transform, new Vector3(0, 0, -10));
                tcSetter.SetLookAtCurrent(target.transform, Vector3.zero);
            }

            if (Input.GetKeyUp(KeyCode.Escape)) {
                if (tcCore.IsPause) {
                    tcCore.Resume();
                } else {
                    tcCore.Pause();
                }
            }

        }

        void LateUpdate() {
            float dt = Time.deltaTime;
            tcCore.Tick(dt);
        }

    }

}