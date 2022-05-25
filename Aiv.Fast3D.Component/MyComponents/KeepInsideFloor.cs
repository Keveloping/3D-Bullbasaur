using OpenTK;

namespace Aiv.Fast3D.Component {
    class KeepInsideFloor : UserComponent {
        private float minWinX, maxWinX, minWinZ, maxWinZ;


        public KeepInsideFloor(GameObject owner, float _minWinX, float _maxWinX, float _minWinZ, float _maxWinZ) : base(owner) {
            minWinX = _minWinX;
            maxWinX = _maxWinX;
            minWinZ = _minWinZ;
            maxWinZ = _maxWinZ;
        }


        public override void LateUpdate() {
            InsideScreen();
        }
        private void InsideScreen() {
            transform.Position = transform.Position.X <= minWinX ? transform.Position = new Vector3(minWinX, transform.Position.Y, transform.Position.Z) :
                                 transform.Position.X >= maxWinX ? transform.Position = new Vector3(maxWinX, transform.Position.Y, transform.Position.Z) :
                                 transform.Position.Z <= minWinZ ? transform.Position = new Vector3(transform.Position.X, transform.Position.Y, minWinZ) :
                                 transform.Position.Z >= maxWinZ ? transform.Position = new Vector3(transform.Position.X, transform.Position.Y, maxWinZ) :
                                 transform.Position = transform.Position;
        }


        public override Component Clone(GameObject owner) {
            return new KeepInsideFloor(owner, minWinX, maxWinX, minWinZ, maxWinZ);
        }
    }
}
