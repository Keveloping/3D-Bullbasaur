using System;
using OpenTK;

namespace Aiv.Fast3D.Component {
    public class CameraOrbital : UserComponent {
        private float mouseCurrentScroll, sensitivity;

        private float minSensitivity;
        public float MinSensitivity {
            get { return minSensitivity; }
            set {
                minSensitivity = value < 0.2f ? minSensitivity = 0.2f : minSensitivity = value; 
            }
        }

        private float maxSensitivity;
        public float MaxSensitivity {
            get { return maxSensitivity; }
            set {
                maxSensitivity = value > 3 ? maxSensitivity = 3 : maxSensitivity = value;
            }
        }

        private Vector2 lastMousePos;
        private Vector3 targetOffset;
        private Transform target;
        private PauseLogic pauseLogic;
          

        public CameraOrbital(GameObject owner, Vector3 targetOffset, float _minSensitivity, float _maxSensitivity) : base(owner) {
            this.targetOffset = targetOffset;
            MinSensitivity = _minSensitivity;
            MaxSensitivity = _maxSensitivity;
            sensitivity = MinSensitivity;
        }


        public override void Awake() {
            target = GameObject.Find("Player").transform;
            pauseLogic = Game.CurrentScene.FindGameObject("PauseLogic").GetComponent<PauseLogic>();
            lastMousePos = Game.Win.MousePosition;           
        }


        public override void Start() {
            if (target == null) return;
            transform.Position = target.Position + targetOffset;     
        }


        public override void Update() {
            Sensitivity();
        }
        private void Sensitivity() {
            if (!pauseLogic.InPause) {
                if (Input.GetButtonDown("IncreasedSensitivity") && sensitivity < maxSensitivity) {
                    sensitivity += 0.1f;
                    EventHandlerManager.CastEvent((int)EventsEnum.ChangeSensitivity, new ChangeSensitivityArgs("IncreasedSensitivity"));
                }
                else if (Input.GetButtonDown("ReduceSensitivity") && sensitivity > minSensitivity) {
                    sensitivity -= 0.1f;
                    EventHandlerManager.CastEvent((int)EventsEnum.ChangeSensitivity, new ChangeSensitivityArgs("ReduceSensitivity"));
                }
            }     
        }


        public override void LateUpdate() {
            transform.Position = target.Position + targetOffset;
            float deltaScroll = mouseCurrentScroll - Game.Win.MouseWheel;
            mouseCurrentScroll = Game.Win.MouseWheel;      
            Rotate();
            Zoom(deltaScroll);
        }
        private void Rotate() {
            Vector2 DeltaMouse = (Game.Win.RawMousePosition - lastMousePos) * 0.5f * Game.DeltaTime;
            lastMousePos = Game.Win.RawMousePosition;
            float angle = DeltaMouse.X;
            Vector3 Dist = transform.Position - target.Position;
            float currentRotation = (float)Math.Atan2(Dist.Z, Dist.X);
            currentRotation += angle * sensitivity;
            Vector3 newOffset = new Vector3((float)Math.Cos(currentRotation), 0, (float)Math.Sin(currentRotation));
            newOffset *= new Vector3(targetOffset.X, 0, targetOffset.Z).Length;
            newOffset.Y = targetOffset.Y;
            targetOffset = newOffset;
            Vector3 cameraTargetDir = target.Position - transform.Position;
            float yRotation = (float)Math.Atan2(cameraTargetDir.Z, cameraTargetDir.X) + MathHelper.DegreesToRadians(-90);
            transform.Rotation = new Vector3(transform.Rotation.X, yRotation, transform.Rotation.Z);
        }
        

        public void Zoom(float zoomDir) {
            if (zoomDir != 0 && !pauseLogic.InPause) {
                float zoomFactor = 1;
                if (zoomDir > 0) zoomFactor = 1.1f;
                else zoomFactor = 0.9f;
                targetOffset *= zoomFactor;
            }
        }


        public override Component Clone(GameObject owner) {
            return new CameraOrbital(owner, targetOffset, minSensitivity, maxSensitivity);
        }
    }
}
