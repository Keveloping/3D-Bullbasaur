using OpenTK;

namespace Aiv.Fast3D.Component {
    public class Rigidbody : Component, IFixedUpdatable {

        public uint Type; //Rigidbodytype 
        protected uint collisionMask;
        public Collider Collider { get; set; }
        private Vector3 velocity;
        public Vector3 Velocity
        {
            get { return velocity; }
            set { velocity = value; }
        }
        private float friction;
        public float Friction
        {
            get { return friction; }
            set { if (value >= 0) friction = value; }
        }
        public bool IsGravityAffected;
        public bool IsCollisionsAffected;

        public Rigidbody (GameObject owner) : base (owner) {
            IsCollisionsAffected = true;
            IsGravityAffected = false;
            Collider = gameObject.GetComponent (typeof (Collider)) as Collider;
            PhysicsMgr.AddItem (this);
        }

        public virtual void FixedUpdate () {
            if (IsGravityAffected) {
                velocity.Y += Game.Gravity * Game.DeltaTime;
            }
            if (velocity.LengthSquared != 0) { 
                float fAmount = friction * Game.DeltaTime;
                float newVelocityLength = velocity.Length - fAmount;
                if (newVelocityLength < 0) newVelocityLength = 0;
                velocity = velocity.Normalized () * newVelocityLength;
            }
            transform.Position += Velocity * Game.DeltaTime;
        }

        public virtual bool Collides (Rigidbody other, ref Collision Collision) {
            return Collider.Collides (other.Collider, ref Collision);
        }

        public void AddCollisionType (uint add) {
            collisionMask |= add;
        }

        public bool CollisionTypeMatches (uint type) {
            return (type & collisionMask) != 0; // == type
        }

        public override Component Clone(GameObject owner)
        {
            Rigidbody clone = new Rigidbody(owner);
            clone.IsCollisionsAffected = IsCollisionsAffected;
            clone.IsGravityAffected = IsGravityAffected;
            clone.Velocity = Velocity;
            clone.collisionMask = collisionMask;
            clone.Type = Type;
            return clone;
        }

    }
}
