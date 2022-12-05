using TripodCamera.Facades;

namespace TripodCamera.Domain {

    public class TCDomain {

        TCCameraDomain cameraDomain;
        public TCCameraDomain CameraDomain => cameraDomain;

        TCDirectorDomain directorDomain;
        public TCDirectorDomain DirectorDomain => directorDomain;

        TCApplyDomain applyDomain;
        public TCApplyDomain ApplyDomain => applyDomain;

        public TCDomain() {
            this.cameraDomain = new TCCameraDomain();
            this.directorDomain = new TCDirectorDomain();
            this.applyDomain = new TCApplyDomain();
        }

        public void Inject(TCFacades facades) {
            cameraDomain.Inject(facades);
            directorDomain.Inject(facades);
            applyDomain.Inject(facades);
        }

    }

}