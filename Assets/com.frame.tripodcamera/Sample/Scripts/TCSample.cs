using UnityEngine;

namespace TripodCamera.Sample {

    public class TCSample : MonoBehaviour {

        TCCore tcCore;

        Vector2 mousePos;

        GameObject target;
        GameObject target2;

        void Awake() {
            tcCore = new TCCore();
            tcCore.Initialize(Camera.main);

            target = GameObject.CreatePrimitive(PrimitiveType.Cube);
            target2 = GameObject.CreatePrimitive(PrimitiveType.Cube);
        }

        void Update() {

            var tcSetter = tcCore.SetterAPI;

            var mouseScroll = Input.mouseScrollDelta.y;
            if (Input.GetKey(KeyCode.LeftControl)) {
                if (mouseScroll != 0) {
                    tcSetter.Zoom_In_Current(mouseScroll);
                }
            } else if (Input.GetKey(KeyCode.LeftShift)) {
                tcSetter.Push_In_Current(mouseScroll);
            } else if (Input.GetKey(KeyCode.LeftAlt)) {
                if (mouseScroll != 0) {
                    tcSetter.Rotate_Roll_Current(mouseScroll);
                }
            }

            float x = Input.GetAxis("Horizontal");
            float y = Input.GetAxis("Vertical");
            if (x != 0 || y != 0) {
                if (Input.GetKey(KeyCode.LeftControl)) {
                    tcSetter.Move_AndChangeLookAtOffset_Current(new Vector2(x, y));
                } else {
                    tcSetter.Move_Current(new Vector2(x, y));
                }
            }

            if (Input.GetMouseButton(1)) {
                var mouseDelta = (Vector2)Input.mousePosition - mousePos;
                if (mouseDelta != Vector2.zero) {
                    tcSetter.Rotate_Horizontal_Current(mouseDelta.x);
                    tcSetter.Rotate_Vertical_Current(mouseDelta.y);
                }
            }

            mousePos = Input.mousePosition;

            if (Input.GetKeyUp(KeyCode.Space)) {
                tcSetter.LookAt_SetInit_Current(target.transform, Vector3.zero);
            }

            if (Input.GetKeyUp(KeyCode.F)) {
                tcSetter.Follow_SetInit_Current(target.transform, new Vector3(0, 0, -10));
            }

            if (Input.GetKeyUp(KeyCode.R)) {
                tcSetter.LookAt_SetInit_Current(target2.transform, Vector3.zero);
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