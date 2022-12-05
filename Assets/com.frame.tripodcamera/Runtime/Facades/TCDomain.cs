using TripodCamera.Facades;

namespace TripodCamera.Domain {

    public class TCDomain {

        TCCameraDomain cameraDomain;
        public TCCameraDomain CameraDomain => cameraDomain;

        TCApplyDomain applyDomain;
        public TCApplyDomain ApplyDomain => applyDomain;

        public TCDomain() {
            this.cameraDomain = new TCCameraDomain();
            this.applyDomain = new TCApplyDomain();
        }

        public void Inject(TCFacades facades) {
            cameraDomain.Inject(facades);
            applyDomain.Inject(facades);
        }

    }

}