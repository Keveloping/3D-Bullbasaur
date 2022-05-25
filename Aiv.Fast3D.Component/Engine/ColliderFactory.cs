using System;
using OpenTK;

namespace Aiv.Fast3D.Component {
    public class ColliderFactory {

        public static Collider CreateCircleFor (GameObject obj) {
            SpriteRenderer spriteRenderer = obj.GetComponent (typeof (SpriteRenderer)) as SpriteRenderer;
            float halfDiagonal = (float) (Math.Sqrt (spriteRenderer.Width * spriteRenderer.Width +
                spriteRenderer.Height * spriteRenderer.Height)) * 0.5f;
            return new CircleCollider (obj , halfDiagonal , Vector3.Zero);
        }

        public static Collider CreateBoxFor (GameObject obj) {
            SpriteRenderer spriteRenderer = obj.GetComponent (typeof (SpriteRenderer)) as SpriteRenderer;
            return new BoxCollider (obj , spriteRenderer.Width , spriteRenderer.Height , new Vector3 ((0.5f - spriteRenderer.Pivot.X) * spriteRenderer.Width, (0.5f - spriteRenderer.Pivot.Y) * spriteRenderer.Height, 0));
        }

        public static Collider CreateSphereFor (GameObject obj, float radius, Vector3 offset, bool showCollider) {
            return new SphereCollider (obj , radius , offset , showCollider);
        }
    }

}
