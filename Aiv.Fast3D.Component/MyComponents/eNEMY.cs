using System;
using OpenTK;

namespace Aiv.Fast3D.Component {
    public class Enemy : UserComponent {   
        private float speed;
        public float Speed {
            get { return speed; }
            set { speed = value > 7 ? speed = 7 : speed = value; }
        }

        private Rigidbody myRigidbody;
        private Transform playerTransform;


        public Enemy (GameObject owner) : base (owner) {
            speed = RandomGenerator.GetRandomFloat(1, 3);        
        }


        public override void Awake () {
            myRigidbody = GetComponent<Rigidbody> ();
            playerTransform = GameObject.Find ("Player").transform;
        }


        public override void Update () {
            Movement();
        }
        private void Movement() {
            Vector3 diff = (playerTransform.Position - transform.Position).Normalized();
            myRigidbody.Velocity = diff * speed;
            if (myRigidbody.Velocity.LengthSquared > 0) {
                transform.Rotation = Utils.LookAt(Vector3.Lerp(transform.Forward, myRigidbody.Velocity, Game.DeltaTime * 3));
            }
        }


        public override void OnCollide(Collision CollisionInfo) {
            if (CollisionInfo.Collider.gameObject.Tag == "player") {
                CollisionInfo.Collider.gameObject.GetComponent<PlayerController>().TakeDamage();
                DestroyMe();                
            }
        }
        private void DestroyMe() {      
            gameObject.IsActive = false;
            EventHandlerManager.CastEvent((int)EventsEnum.EnemyDeath, new EventArgs());
            EventHandlerManager.CastEvent((int)EventsEnum.ScoreIncrease, new EventArgs());
        }


        public override Component Clone (GameObject owner) {
            throw new NotImplementedException ();
        }
    }
}
