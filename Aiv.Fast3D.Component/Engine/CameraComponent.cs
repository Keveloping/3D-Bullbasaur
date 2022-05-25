using OpenTK;

namespace Aiv.Fast3D.Component {
    public class CameraComponent :  Component {

        private PerspectiveCamera camera;
        public PerspectiveCamera Camera
        {
            get { return camera; }
        }
        public Vector3 Forward
        {
            get { return camera.Forward; }
        }
        public Vector3 Right
        {
            get { return camera.Right; }
        }

        public CameraComponent (GameObject owner, float fov, float zNear, float zFar)  : base (owner) {
            camera = new PerspectiveCamera (transform.Position , transform.EulerRotation , fov , zNear , zFar);
            CameraMgr.Add3DCamera (this);
        }

        public void MoveCamera () {
            camera.Position3 = transform.Position;
            camera.EulerRotation3 = transform.EulerRotation;
        }

        public override Component Clone (GameObject owner) {
            throw new System.NotImplementedException ();
        }
    }
}
