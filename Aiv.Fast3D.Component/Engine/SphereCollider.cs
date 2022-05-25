using OpenTK;
using System;

namespace Aiv.Fast3D.Component {
    public class SphereCollider : Collider, IDrawable {

        public DrawLayer Layer
        {
            get { return DrawLayer.Background; }
        }

        private Sphere debugMesh;
        private float Radius;
        private bool debug;
        public bool Debug
        {
            get { return debug; }
            set { debug = value; }
        }

        public SphereCollider (GameObject owner , float radius , Vector3 offset , bool showCollider) : base (owner , offset) {
            Radius = radius;
            debugMesh = new Sphere ();
            float scaleFactor = radius / debugMesh.Radius;
            debugMesh.Scale3 = new Vector3 (scaleFactor , scaleFactor , scaleFactor);
            DrawMgr.AddItem (this);
            debug = showCollider;
        }


        public override bool Collides (Collider aCollider , ref Collision collision) {
            return  aCollider.Collides (this , ref collision);
        }

        public override bool Collides (BoxCollider box , ref Collision collision) {
            return box.Collides (this , ref collision);
        }

        public override bool Collides (CircleCollider circle , ref Collision collision) {
            return circle.Collides (this , ref collision);
        }

        public override bool Collides (SphereCollider sphere , ref Collision collision) {
            Vector3 dist = sphere.Position - Position;
            return dist.LengthSquared <= Math.Pow (Radius + sphere.Radius , 2);
        }

        public void DrawShadow () {
            return;
        }

        public void Draw () {
            if (!debug) return;
            debugMesh.Position3 = Position;
            debugMesh.DrawColor (1 , 1 , 0 , 0.5f);
        }

        public override bool Contains (Vector3 point) {
            Vector3 distFromCenter = point - Position;
            return distFromCenter.Length <= Radius;
        }

        public override Component Clone (GameObject owner) {
            throw new NotImplementedException ();
        }
    }
}
