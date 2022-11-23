using System.Collections.Generic;
using UnityEngine;

namespace TripodCamera.Sample {

    public class TCSample : MonoBehaviour {

        TCCore tcCore;

        Vector2 mousePos;

        GameObject followTarget;
        GameObject loolAtTarget;

        List<GameObject> targets;
        PrimitiveType[] randomPrimitiveTypes = new PrimitiveType[] {
            PrimitiveType.Cube,
            PrimitiveType.Cylinder,
            PrimitiveType.Sphere,
            PrimitiveType.Capsule,
        };

        void Awake() {
            
            tcCore = new TCCore();
            tcCore.Initialize(Camera.main);

            this.targets = new List<GameObject>();

        }

        float sensitivity = 0.05f;
        void OnGUI() {
            
            GUILayout.BeginHorizontal();
            GUILayout.Label("灵敏度: ");
            sensitivity = GUILayout.HorizontalSlider(sensitivity, 0, 1, GUILayout.Width(100));
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            if (GUILayout.RepeatButton("推")) {
                tcCore.SetterAPI.Push_In_Current(sensitivity);
            }
            if (GUILayout.RepeatButton("拉")) {
                tcCore.SetterAPI.Push_In_Current(-sensitivity);
            }
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            if (GUILayout.RepeatButton("左看")) {
                tcCore.SetterAPI.Rotate_Horizontal_Current(-sensitivity);
            }
            if (GUILayout.RepeatButton("右看")) {
                tcCore.SetterAPI.Rotate_Horizontal_Current(sensitivity);
            }   
            if (GUILayout.RepeatButton("上看")) {
                tcCore.SetterAPI.Rotate_Vertical_Current(sensitivity);
            }
            if (GUILayout.RepeatButton("下看")) {
                tcCore.SetterAPI.Rotate_Vertical_Current(-sensitivity);
            }
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            if (GUILayout.RepeatButton("左转")) {
                tcCore.SetterAPI.Rotate_Roll_Current(-sensitivity);
            }
            if (GUILayout.RepeatButton("右转")) {
                tcCore.SetterAPI.Rotate_Roll_Current(sensitivity);
            }
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            if (GUILayout.RepeatButton("左移")) {
                tcCore.SetterAPI.Move_Current(new Vector2(-sensitivity, 0));
            }
            if (GUILayout.RepeatButton("右移")) {
                tcCore.SetterAPI.Move_Current(new Vector2(sensitivity, 0));
            }
            if (GUILayout.RepeatButton("上移")) {
                tcCore.SetterAPI.Move_Current(new Vector2(0, sensitivity));
            }
            if (GUILayout.RepeatButton("下移")) {
                tcCore.SetterAPI.Move_Current(new Vector2(0, -sensitivity));
            }
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            if (GUILayout.RepeatButton("放大")) {
                tcCore.SetterAPI.Zoom_In_Current(sensitivity);
            }
            if (GUILayout.RepeatButton("缩小")) {
                tcCore.SetterAPI.Zoom_In_Current(-sensitivity);
            }
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("生成随机物体")) {
                var go = GameObject.CreatePrimitive(randomPrimitiveTypes[Random.Range(0, randomPrimitiveTypes.Length)]);
                go.transform.position = new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), Random.Range(-10, 10));
                targets.Add(go);
            }
            if (GUILayout.Button("清空随机物体")) {
                foreach (var go in targets) {
                    Destroy(go);
                }
                targets.Clear();
            }
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("跟随下一个")) {
                if (followTarget == null) {
                    followTarget = targets[0];
                } else {
                    var index = targets.IndexOf(followTarget);
                    if (index == targets.Count - 1) {
                        followTarget = targets[0];
                    } else {
                        followTarget = targets[index + 1];
                    }
                }
                tcCore.SetterAPI.Follow_SetInit_Current(followTarget.transform, new Vector3(0, 0, -10));
            }
            if (GUILayout.Button("盯着下一个")) {
                if (loolAtTarget == null) {
                    loolAtTarget = targets[0];
                } else {
                    var index = targets.IndexOf(loolAtTarget);
                    if (index == targets.Count - 1) {
                        loolAtTarget = targets[0];
                    } else {
                        loolAtTarget = targets[index + 1];
                    }
                }
                tcCore.SetterAPI.LookAt_SetInit_Current(loolAtTarget.transform, Vector3.zero);
            }
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("取消跟随")) {
                tcCore.SetterAPI.Follow_ChangeTarget_Current(null);
            }
            if (GUILayout.Button("取消盯着")) {
                tcCore.SetterAPI.LookAt_ChangeTarget_Current(null);
            }
            GUILayout.EndHorizontal();

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
                tcSetter.LookAt_SetInit_Current(followTarget.transform, Vector3.zero);
            }

            if (Input.GetKeyUp(KeyCode.F)) {
                tcSetter.Follow_SetInit_Current(followTarget.transform, new Vector3(0, 0, -10));
            }

            if (Input.GetKeyUp(KeyCode.R)) {
                tcSetter.LookAt_SetInit_Current(loolAtTarget.transform, Vector3.zero);
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