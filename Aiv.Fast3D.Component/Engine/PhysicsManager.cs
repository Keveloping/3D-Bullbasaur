using System.Collections.Generic;
using OpenTK;

namespace Aiv.Fast3D.Component {

    public enum CollisionType { None, RectsIntersection };

    public struct Collision {
        public Vector2 Delta;
        public Collider Collider;
        public CollisionType Type;
    }

    public class PhysicsMgr {

        static List<Rigidbody> items;

        static Collision Collision;

        static PhysicsMgr () {
            items = new List<Rigidbody> ();
        }

        public static void AddItem (Rigidbody item) {
            items.Add (item);
        }

        public static void RemoveItem (Rigidbody item) {
            items.Remove (item);
        }

        public static void FixedUpdate () {
            for (int i = 0; i < items.Count; i++) {
                if (items[i].Enabled) {
                    items[i].FixedUpdate ();
                }
            }
        }

        public static void CheckCollisions () {
            for (int i = 0; i < items.Count - 1; i++) {
                if (items[i].IsCollisionsAffected && items[i].Enabled) {
                    //check collisions with next items
                    for (int j = i + 1; j < items.Count; j++) {
                        if (items[j].IsCollisionsAffected && items[j].Enabled) {
                            //check if one of two rigid bodies is interested in collision
                            bool firstCheck = items[i].CollisionTypeMatches (items[j].Type);
                            bool secondCheck = items[j].CollisionTypeMatches (items[i].Type);

                            if ((firstCheck || secondCheck) && items[i].Collides (items[j], ref Collision)) {
                                //Console.WriteLine(items[i].Type + " Collides with " + items[j].Type);

                                if (firstCheck) {
                                    Collision.Collider = items[j].Collider;
                                    items[i].gameObject.OnCollide (Collision);
                                }

                                if (secondCheck) {
                                    Collision.Collider = items[i].Collider;
                                    items[j].gameObject.OnCollide (Collision);
                                }
                            }
                        }
                    }
                }
            }
        }

        public static void ClearAll () {
            items.Clear ();
        }


    }
}

