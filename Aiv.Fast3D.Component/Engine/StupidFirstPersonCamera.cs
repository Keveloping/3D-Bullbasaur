using Aiv.Fast2D;
using OpenTK;


namespace Aiv.Fast3D.Component {
    public class StupidFirstPersonCamera : UserComponent {

        private float rotationSpeed = 50f;
        private float moveSpeed = 5f;
        private Vector2 lastMousePosition;

        private CameraComponent camera;

        private Rigidbody rigidbody;

        public StupidFirstPersonCamera (GameObject owner) : base (owner) {

        }

        public override void Awake () {
            camera = GetComponent<CameraComponent> ();
            rigidbody = GetComponent<Rigidbody> ();
        }

        public override Component Clone (GameObject owner) {
            throw new System.NotImplementedException ();
        }

        public override void Update () {
            rigidbody.Velocity = camera.Right * Input.GetAxis ("Horizontal") + camera.Forward * Input.GetAxis ("Vertical");
            rigidbody.Velocity.Normalize ();
            rigidbody.Velocity *= moveSpeed;

            Vector3 rotationVector = new Vector3 (-(Game.Win.RawMouseY - lastMousePosition.Y) , Game.Win.RawMouseX - lastMousePosition.X , 0) * Game.Win.DeltaTime * rotationSpeed;
            lastMousePosition = new Vector2 (Game.Win.RawMouseX , Game.Win.RawMouseY);
            transform.EulerRotation += rotationVector;
        }

    }
}
