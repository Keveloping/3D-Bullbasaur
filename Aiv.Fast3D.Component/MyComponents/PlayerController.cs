using System;
using OpenTK;

namespace Aiv.Fast3D.Component {
    public class PlayerController : UserComponent {
        private float movementSpeed;

        private int hp;
        public int Hp {
            get { return hp; }
            set { hp = value; }
        }

        private Rigidbody myRigidbody;
        private CameraComponent mainCamera;
        private BulletMgr bulletPool;
        private PauseLogic pauseLogic;


        public PlayerController (GameObject owner, float _movementSpeed, int _hp) : base (owner) {
            movementSpeed = _movementSpeed;
            hp = _hp;
        }


        public override void Awake () {
            myRigidbody = GetComponent<Rigidbody> ();
            mainCamera = GameObject.Find ("MainCamera").GetComponent<CameraComponent> ();
            bulletPool = GameObject.Find ("BulletMgr").GetComponent<BulletMgr> ();
            pauseLogic = Game.CurrentScene.FindGameObject("PauseLogic").GetComponent<PauseLogic>();
        }


        public override void Update () {
            UpdateMove ();
            UpdateShoot ();
        }


        private void UpdateMove () {
            myRigidbody.Velocity = Input.GetAxis ("Horizontal") * mainCamera.Right + Input.GetAxis ("Vertical") * mainCamera.Forward;
            if (myRigidbody.Velocity.LengthSquared > 1) {
                myRigidbody.Velocity.Normalize ();
            }
            myRigidbody.Velocity = myRigidbody.Velocity * movementSpeed;

            if (myRigidbody.Velocity.LengthSquared > 0) {
                transform.Rotation = Utils.LookAt (Vector3.Lerp (transform.Forward , myRigidbody.Velocity , Game.DeltaTime * 3));
            }
        }
        private void UpdateShoot () {
            if (Input.GetButtonDown("Shoot") && !pauseLogic.InPause) {
                Bullet bullet = bulletPool.GetBullet();
                if (bullet == null) {
                    return;
                }
                bullet.Shoot(transform.Position + new Vector3(transform.Forward.X, 0.7f, transform.Forward.Z),
                             new Vector3(transform.Forward.X, 0.3f, transform.Forward.Z));
                EventHandlerManager.CastEvent((int)EventsEnum.PlayerShoot, new EventArgs());
            }
        }


        public void TakeDamage() {
            EventHandlerManager.CastEvent((int)EventsEnum.PlayerHit, new EventArgs());
            gameObject.IsActive = hp == 0 ? false : true;            
            if (!gameObject.IsActive) Game.TriggerChangeScene(null);        
        }


        public override Component Clone (GameObject owner) {
            throw new NotImplementedException ();
        }
    }
}
