using System;
using UnityEngine;
using JackEasing;

namespace TripodCamera {

    [Serializable]
    public struct TCDollyPointModel {

        /* Last Point Read Outside */

        // - Base Info
        public Vector3 positionOffset;

        // - Move
        public Vector3 moveOffset;
        public EasingType moveEasingType;
        public float moveMaintainTime;

        // - Rotate
        public Vector3 lookOffset;
        public EasingType lookEasingType;
        public float lookMaintainTime;

        // - Zoom
        public float zoomInOffset;
        public EasingType zoomInEasingType;
        public float zoomInMaintainTime;

        public float MaintainTimeMax => Mathf.Max(moveMaintainTime, lookMaintainTime, zoomInMaintainTime);

    }

}