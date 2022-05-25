using System;
using OpenTK;

namespace Aiv.Fast3D.Component {
    public class CircleCollider : Collider {

        public float Radius;

        public CircleCollider (GameObject owner , float radius , Vector3 offset) : base (owner , offset) {
            Radius = radius;
        }

        public override bool Collides (Collider aCollider, ref Collision Collision) {
            return aCollider.Collides (this, ref Collision);
        }

        //Circle vs Circle
        public override bool Collides (CircleCollider other, ref Collision Collision) {
            Vector3 dist = other.Position - Position;
            return dist.LengthSquared <= Math.Pow (Radius + other.Radius , 2);
        }

        //Circle vs Box
        public override bool Collides (BoxCollider box, ref Collision Collision) {
            return box.Collides (this, ref Collision);
        }

        public override bool Contains (Vector3 point) {
            Vector3 distFromCenter = point - Position;

            return distFromCenter.Length <= Radius;
        }

        public override bool Collides (SphereCollider sphere , ref Collision collision) {
            return false;
        }

        public override Component Clone (GameObject owner)
        {
            return new CircleCollider(owner, Radius, Offset);
        }
    }

}
