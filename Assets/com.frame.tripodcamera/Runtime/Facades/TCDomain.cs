using System;
using TripodCamera.Facades;

namespace TripodCamera.Domain {

    internal class TCDomain {

        TCCameraDomain cameraDomain;
        internal TCCameraDomain CameraDomain => cameraDomain;

        TCApplyDomain applyDomain;
        internal TCApplyDomain ApplyDomain => applyDomain;

        internal TCDomain() {
            this.cameraDomain = new TCCameraDomain();
            this.applyDomain = new TCApplyDomain();
        }

        internal void Inject(TCFacades facades) {
            cameraDomain.Inject(facades);
            applyDomain.Inject(facades);
        }

    }

}