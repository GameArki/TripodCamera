using System;
using TripodCamera.Facades;

namespace TripodCamera.Domain {

    public class TCDomain {

        TCCameraDomain cameraDomain;
        public TCCameraDomain CameraDomain => cameraDomain;

        public TCDomain() {
            this.cameraDomain = new TCCameraDomain();
        }

        public void Inject(TCFacades facades) {
            cameraDomain.Inject(facades);
        }

    }

}