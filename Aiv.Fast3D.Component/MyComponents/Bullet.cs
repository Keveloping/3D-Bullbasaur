using System;
using OpenTK;

namespace Aiv.Fast3D.Component {
    public class Bullet : UserComponent {
        private float speed, floorOffset, reboundOffset;
        private Rigidbody myRigidbody;
        private Transform floorTransform;
       

        public Bullet (GameObject owner, float _speed) : base (owner) {
            speed = _speed;
            floorOffset = 0.5f;
            reboundOffset = floorOffset;
        }


        public override void Awake () {
            gameObject.IsActive = false;
            myRigidbody = GetComponent<Rigidbody> ();
            floorTransform = Game.CurrentScene.FindGameObject("Floor").transform;
        }


        public void Shoot (Vector3 startPosition, Vector3 direction) {
            transform.Position = startPosition;
            myRigidbody.Velocity = direction * speed;
            gameObject.IsActive = true;
        }


        public override void OnCollide(Collision CollisionInfo) {
            if (CollisionInfo.Collider.gameObject.Tag == "enemy") {
                CollisionInfo.Collider.gameObject.IsActive = false;
                EventHandlerManager.CastEvent((int)EventsEnum.EnemyDeath, new EventArgs());
                EventHandlerManager.CastEvent((int)EventsEnum.ScoreIncrease, new EventArgs());
                DestroyMe();
            }
        }


        public override void Update() {        
            Rebound();
            DespawnMe();
        }
        private void Rebound() {
            if (transform.Position.Y <= floorTransform.Position.Y + floorOffset) {
                transform.Position = new Vector3(transform.Position.X, floorTransform.Position.Y + floorOffset, transform.Position.Z);
                myRigidbody.Velocity = new Vector3(myRigidbody.Velocity.X, (-myRigidbody.Velocity.Y + reboundOffset) * 0.72f, myRigidbody.Velocity.Z);
                //EventHandlerManager.CastEvent((int)EventsEnum.BulletRebound, new EventArgs());
            }
        }
        private void DespawnMe() {
            float minWinX = -25;
            float maxWinX = 25;
            float minWinZ = -25;
            float maxWinZ = 25;

            if (transform.Position.X > maxWinX || transform.Position.X < minWinX
               || transform.Position.Z > maxWinZ || transform.Position.Z < minWinZ) DestroyMe();
        }
        private void DestroyMe() {
            gameObject.IsActive = false;
        }


        public override Component Clone (GameObject owner) {
            throw new NotImplementedException ();
        }
    }
}
