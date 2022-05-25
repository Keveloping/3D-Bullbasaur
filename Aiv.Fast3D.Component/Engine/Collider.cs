using OpenTK;

namespace Aiv.Fast3D.Component {
    public abstract class Collider : Component {


        public Vector3 Offset;
        public Vector3 Position { get { return transform.Position + Offset; } }

        public Collider (GameObject owner , Vector3 Offset) : base (owner) {
            this.Offset = Offset;
            Rigidbody rigidbody = gameObject.GetComponent (typeof (Rigidbody)) as Rigidbody;
            if (rigidbody != null) {
                rigidbody.Collider = this;
            }
        }

        public abstract bool Collides (Collider aCollider, ref Collision collision);
        public abstract bool Collides (CircleCollider circle , ref Collision collision);
        public abstract bool Collides (BoxCollider box , ref Collision collision);
        public abstract bool Collides (SphereCollider sphere , ref Collision collision);
        public abstract bool Contains (Vector3 point);
    }
}
