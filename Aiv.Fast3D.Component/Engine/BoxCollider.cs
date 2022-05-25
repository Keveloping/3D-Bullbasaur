using System;
using OpenTK;

namespace Aiv.Fast3D.Component {
    public class BoxCollider : Collider {

        protected float halfWidth;
        protected float halfHeight;
        public float Height { get { return halfHeight * 2; } }
        public float Width { get { return halfWidth * 2; } }

        public BoxCollider (GameObject owner , float w , float h , Vector3 offset) : base (owner , offset) {
            halfWidth = w * 0.5f;
            halfHeight = h * 0.5f;
        }

        public override bool Collides (Collider aCollider, ref Collision Collision) {
            return aCollider.Collides (this, ref Collision);
        }

        //Box vs Circle
        public override bool Collides (CircleCollider circle, ref Collision Collision) {
            float deltaX = circle.Position.X - Math.Max (
                                                Position.X - halfWidth ,
                                                Math.Min (circle.Position.X , Position.X + halfWidth)
                                               );
            float deltaY = circle.Position.Y - Math.Max (
                                                Position.Y - halfHeight ,
                                                Math.Min (circle.Position.Y , Position.Y + halfHeight)
                                               );
            // RAD(x2 + y2) < r
            // x2 + y2 < r2
            return (deltaX * deltaX + deltaY * deltaY) < (circle.Radius * circle.Radius);
        }

        //Box vs Box
        public override bool Collides (BoxCollider box, ref Collision Collision) {
            //float deltaX = box.Position.X - Position.X;
            //float deltaY = box.Position.Y - Position.Y;

            //return (Math.Abs (deltaX) <= halfWidth + box.halfWidth)
            //    && (Math.Abs (deltaY) <= halfHeight + box.halfHeight);
            float deltaX = Math.Abs (box.Position.X - Position.X) - (halfWidth + box.halfWidth);
            if (deltaX > 0) return false;
            float deltaY = Math.Abs (box.Position.Y - Position.Y) - (halfHeight + box.halfHeight);
            if (deltaY > 0) return false;
            Collision.Type = CollisionType.RectsIntersection;
            Collision.Delta = new Vector2 (-deltaX , -deltaY);
            return true;
        }

        public override bool Collides (SphereCollider sphere , ref Collision collision) {
            return false;
        }

        public override bool Contains (Vector3 point) {
            return
                point.X >= Position.X - halfWidth &&
                point.X <= Position.X + halfWidth &&
                point.Y >= Position.Y - halfHeight &&
                point.Y <= Position.Y + halfHeight;
        }

        public override Component Clone (GameObject owner)
        {
            return new BoxCollider(owner, Width,  Height, Offset);
        }

    }

}
