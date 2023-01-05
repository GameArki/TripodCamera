using System.Collections.Generic;
using UnityEngine;
using GameArki.FPEasing;

namespace GameArki.TripodCamera.Sample {

    public class TCSample : MonoBehaviour {

        TCCore tcCore;

        Vector2 mousePos;

        GameObject followTarget;
        float follow_duration_horizontal;
        float follow_duration_vertical;
        GameObject loolAtTarget;

        List<GameObject> targets;
        PrimitiveType[] randomPrimitiveTypes = new PrimitiveType[] {
            PrimitiveType.Cube,
            PrimitiveType.Cylinder,
            PrimitiveType.Sphere,
            PrimitiveType.Capsule,
        };

        // ==== Input ====
        float sensitivity = 0.05f;

        // - Shake State
        float shakeAmplitudeX = 0.1f;
        float shakeAmplitudeY = 0.1f;
        float shakeFrequency = 10;
        float shakeDuration = 1;
        int shakeEasingType = (int)EasingType.Linear;

        // - Move State
        Vector2 moveOffset = new Vector2(0.1f, 0.1f);
        float moveDuration = 1;
        int moveEasingType = (int)EasingType.Linear;
        bool isInherit_move;
        bool isExitReset_move;

        // - Rotate State
        Vector2 rotOffset = new Vector2(0.1f, 0.1f);
        float rotDuration = 1;
        int rotEasingType = (int)EasingType.Linear;
        bool isInherit_rotate;
        bool isExitReset_rotate;

        // - Push State
        float pushOffset = 1;
        float pushDuration = 1;
        int pushEasingType = (int)EasingType.Linear;
        bool isInherit_push;
        bool isExitReset_push;

        // - Round State
        Vector2 roundOffset = Vector2.right;
        float roundDuration = 1;
        int roundEasingType = (int)EasingType.Linear;
        bool isInherit_round;
        bool isExitReset_round;

        bool showMenu;

        void Awake() {

            tcCore = new TCCore();
            tcCore.Initialize(Camera.main);
            int mainID = 5;
            tcCore.SetterAPI.SpawnByMain(mainID);
            var hook = tcCore.SetterAPI.GetHook(mainID);

            this.targets = new List<GameObject>();

        }

        void OnGUI() {

            showMenu = GUILayout.Toggle(showMenu, "显示菜单");

            if (!showMenu) return;

            GUILayout.BeginHorizontal();
            GUILayout.BeginVertical();
            GUILayout.BeginHorizontal();
            GUILayout.Label("灵敏度: ");
            sensitivity = GUILayout.HorizontalSlider(sensitivity, 0, 1, GUILayout.Width(100));
            GUILayout.Label(" " + sensitivity.ToString("F2"));
            GUILayout.EndHorizontal();

            GUILayout.Space(10);
            GUILayout.BeginHorizontal();
            if (GUILayout.RepeatButton("推")) {
                tcCore.SetterAPI.Push_In_Current(sensitivity);
            }
            if (GUILayout.RepeatButton("拉")) {
                tcCore.SetterAPI.Push_In_Current(-sensitivity);
            }
            GUILayout.EndHorizontal();

            GUILayout.Space(10);
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

            GUILayout.Space(10);
            GUILayout.BeginHorizontal();
            if (GUILayout.RepeatButton("左转")) {
                tcCore.SetterAPI.Rotate_Roll_Current(-sensitivity);
            }
            if (GUILayout.RepeatButton("右转")) {
                tcCore.SetterAPI.Rotate_Roll_Current(sensitivity);
            }
            GUILayout.EndHorizontal();

            GUILayout.Space(10);
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

            GUILayout.Space(10);
            GUILayout.BeginHorizontal();
            if (GUILayout.RepeatButton("放大")) {
                tcCore.SetterAPI.Zoom_In_Current(sensitivity);
            }
            if (GUILayout.RepeatButton("缩小")) {
                tcCore.SetterAPI.Zoom_In_Current(-sensitivity);
            }
            GUILayout.EndHorizontal();

            GUILayout.Space(10);

            GUILayout.BeginHorizontal();
            GUILayout.Label($"跟随过渡时间_水平方向:{follow_duration_horizontal.ToString("F2")}");
            follow_duration_horizontal = GUILayout.HorizontalSlider(follow_duration_horizontal, 0, 2, GUILayout.Width(100));
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label($"跟随过渡时间_垂直方向:{follow_duration_vertical.ToString("F2")}");
            follow_duration_vertical = GUILayout.HorizontalSlider(follow_duration_vertical, 0, 2, GUILayout.Width(100));
            GUILayout.EndHorizontal();

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
                tcCore.SetterAPI.Follow_SetInit_Current(followTarget.transform, new Vector3(0, 5, -8),
                EasingType.Linear, follow_duration_horizontal,
                EasingType.Linear, follow_duration_vertical);
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

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("取消跟随")) {
                tcCore.SetterAPI.Follow_ChangeTarget_Current(null);
            }
            if (GUILayout.Button("取消盯着")) {
                tcCore.SetterAPI.LookAt_ChangeTarget_Current(null);
            }
            GUILayout.EndHorizontal();

            GUILayout.Space(10);
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

            GUILayout.Space(20);
            GUILayout.BeginHorizontal();
            GUILayout.Label("震幅 x: ");
            shakeAmplitudeX = GUILayout.HorizontalSlider(shakeAmplitudeX, 0, 10, GUILayout.Width(100));
            GUILayout.Label(" " + shakeAmplitudeX.ToString("F2"));
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("震幅 y: ");
            shakeAmplitudeY = GUILayout.HorizontalSlider(shakeAmplitudeY, 0, 10, GUILayout.Width(100));
            GUILayout.Label(" " + shakeAmplitudeY.ToString("F2"));
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("震频: ");
            shakeFrequency = GUILayout.HorizontalSlider(shakeFrequency, 0, 100, GUILayout.Width(100));
            GUILayout.Label(" " + shakeFrequency.ToString("F2"));
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("震时: ");
            shakeDuration = GUILayout.HorizontalSlider(shakeDuration, 0, 3, GUILayout.Width(100));
            GUILayout.Label(" " + shakeDuration.ToString("F2"));
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("震式: ");
            shakeEasingType = (int)GUILayout.HorizontalSlider((int)shakeEasingType, 0, (int)EasingType.InOutBounce, GUILayout.Width(100));
            GUILayout.Label(" " + ((EasingType)shakeEasingType).ToString(), GUILayout.Width(100));
            GUILayout.EndHorizontal();

            if (GUILayout.Button("进入震动状态")) {
                var arg = new TCShakeStateModel() {
                    amplitudeOffset = new Vector2(shakeAmplitudeX, shakeAmplitudeY),
                    easingType = (EasingType)shakeEasingType,
                    frequency = shakeFrequency,
                    duration = shakeDuration
                };
                tcCore.SetterAPI.Enter_Shake_Current(new TCShakeStateModel[] { arg });
            }

            GUILayout.EndVertical();

            GUILayout.BeginVertical();
            GUILayout.Label("移动");
            GUILayout.BeginHorizontal();
            GUILayout.Label("x: ");
            moveOffset.x = GUILayout.HorizontalSlider(moveOffset.x, -100, 100, GUILayout.Width(100));
            GUILayout.Label(" " + moveOffset.x.ToString("F2"));
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("y: ");
            moveOffset.y = GUILayout.HorizontalSlider(moveOffset.y, -100, 100, GUILayout.Width(10));
            GUILayout.Label(" " + moveOffset.y.ToString("F2"));
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("持续: ");
            moveDuration = GUILayout.HorizontalSlider(moveDuration, 0, 3, GUILayout.Width(100));
            GUILayout.Label(" " + moveDuration.ToString("F2"));
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("easing: ");
            moveEasingType = (int)GUILayout.HorizontalSlider((int)moveEasingType, 0, (int)EasingType.InOutBounce, GUILayout.Width(100));
            GUILayout.Label(" " + ((EasingType)moveEasingType).ToString(), GUILayout.Width(100));
            GUILayout.EndHorizontal();
            isInherit_move = GUILayout.Toggle(isInherit_move, "isInherit");
            isExitReset_move = GUILayout.Toggle(isExitReset_move, "isExitReset");

            if (GUILayout.Button("进入移动状态")) {
                var arg = new TCMovementStateModel() {
                    offset = moveOffset,
                    easingType = (EasingType)moveEasingType,
                    duration = moveDuration,
                    isInherit = isInherit_move
                };
                tcCore.SetterAPI.Enter_Move_Current(new TCMovementStateModel[] { arg }, isExitReset_move, EasingType.Linear, 0.5f);
            }

            GUILayout.Label("旋转");
            GUILayout.BeginHorizontal();
            GUILayout.Label("x: ");
            rotOffset.x = GUILayout.HorizontalSlider(rotOffset.x, -90, 90, GUILayout.Width(100));
            GUILayout.Label(" " + rotOffset.x.ToString("F2"));
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("y: ");
            rotOffset.y = GUILayout.HorizontalSlider(rotOffset.y, -90, 90, GUILayout.Width(100));
            GUILayout.Label(" " + rotOffset.y.ToString("F2"));
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("持续: ");
            rotDuration = GUILayout.HorizontalSlider(rotDuration, 0, 3, GUILayout.Width(100));
            GUILayout.Label(" " + rotDuration.ToString("F2"));
            GUILayout.EndHorizontal();
            isInherit_rotate = GUILayout.Toggle(isInherit_rotate, "isInherit");
            isExitReset_rotate = GUILayout.Toggle(isExitReset_rotate, "isExitReset");

            if (GUILayout.Button("进入旋转状态")) {
                var arg = new TCRotationStateModel() {
                    offset = rotOffset,
                    easingType = (EasingType)rotEasingType,
                    duration = rotDuration,
                    isInherit = isInherit_rotate
                };
                tcCore.SetterAPI.Enter_Rotation_Current(new TCRotationStateModel[] { arg }, isExitReset_rotate, EasingType.Linear, 0.5f);
            }

            GUILayout.Label("推进");
            GUILayout.BeginHorizontal();
            GUILayout.Label("pushOffset: ");
            pushOffset = GUILayout.HorizontalSlider(pushOffset, -100, 100, GUILayout.Width(100));
            GUILayout.Label(" " + pushOffset.ToString("F2"));
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("持续: ");
            pushDuration = GUILayout.HorizontalSlider(pushDuration, 0, 10, GUILayout.Width(100));
            GUILayout.Label(" " + pushDuration.ToString("F2"));
            GUILayout.EndHorizontal();
            isInherit_push = GUILayout.Toggle(isInherit_push, "isInherit");
            isExitReset_push = GUILayout.Toggle(isExitReset_push, "isExitReset");

            if (GUILayout.Button("进入推进状态")) {
                var arg = new TCPushStateModel() {
                    offset = pushOffset,
                    easingType = (EasingType)pushEasingType,
                    duration = pushDuration,
                    isInherit = isInherit_push
                };
                tcCore.SetterAPI.Enter_Push_Current(new TCPushStateModel[] { arg }, isExitReset_push, EasingType.Linear, 0.5f);
            }

            GUILayout.Label("绕柱");
            GUILayout.BeginHorizontal();
            GUILayout.Label("roundOffset: ");
            roundOffset.x = GUILayout.HorizontalSlider(roundOffset.x, -360, 360, GUILayout.Width(100));
            GUILayout.Label(" " + roundOffset.x.ToString("F2"));
            roundOffset.y = GUILayout.HorizontalSlider(roundOffset.y, -360, 360, GUILayout.Width(100));
            GUILayout.Label(" " + roundOffset.y.ToString("F2"));
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("持续: ");
            roundDuration = GUILayout.HorizontalSlider(roundDuration, 0, 10, GUILayout.Width(100));
            GUILayout.Label(" " + roundDuration.ToString("F2"));
            GUILayout.EndHorizontal();
            isInherit_round = GUILayout.Toggle(isInherit_round, "isInherit");
            isExitReset_round = GUILayout.Toggle(isExitReset_round, "isExitReset");

            if (GUILayout.Button("进入绕柱状态")) {
                TCRoundStateModel arg;
                arg.tar = GameObject.Find("绕柱柱子").transform;
                arg.offset = roundOffset;
                arg.easingType = (EasingType)roundEasingType;
                arg.duration = roundDuration;
                arg.isInherit = isInherit_round;

                tcCore.SetterAPI.Enter_Round_Current(new TCRoundStateModel[] { arg }, isExitReset_round, EasingType.Linear, 0.5f);
            }

            GUILayout.EndVertical();

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

        }

        void LateUpdate() {
            float dt = Time.deltaTime;
            tcCore.Tick(dt);
        }

    }

}