using OpenTK;
using System;

namespace Aiv.Fast3D.Component {
    public class Transform {

        private Vector3 position;
        public Vector3 Position
        {
            get { return position; }
            set { position = value; }
        }
        private Vector3 rotation; //è espressa in radianti
        public Vector3 Rotation
        {
            get { return rotation; }
            set { rotation = value; }
        }
        public Vector3 EulerRotation //è espressa in gradi
        { 
            get
            {
                return this.rotation * 180f / (float) Math.PI;
            }
            set
            {
                this.rotation = value * (float) Math.PI / 180f;
            }
        }
        private Vector3 scale;
        public Vector3 Scale
        {
            get { return scale; }
            set { scale = value; }
        }
        

        public Vector3 Forward
        {
            get
            {
                return (Matrix4.CreateRotationY (Rotation.Y) * Matrix4.CreateRotationZ (Rotation.Z) * Matrix4.CreateRotationX (Rotation.X)).ExtractRotation () * Vector3.UnitZ;
            }
        }

        public Vector3 Up
        {
            get
            {
                return (Matrix4.CreateRotationY (Rotation.Y) * Matrix4.CreateRotationZ (Rotation.Z) * Matrix4.CreateRotationX (Rotation.X)).ExtractRotation () * Vector3.UnitY;
            }
        }

        public Vector3 Right
        {
            get
            {
                return Vector3.Cross (Forward , Up);
                //return (Matrix4.CreateRotationY (Rotation.Y) * Matrix4.CreateRotationZ (Rotation.Z) * Matrix4.CreateRotationX (Rotation.X)).ExtractRotation () * Vector3.UnitX;
            }
        }

        public Transform (Vector3 position, Vector3 scale, Vector3 rotation) {
            Position = position;
            Scale = scale;
            Rotation = rotation;
        }

        public static float RadiantsToDegrees (float radiants) {
            return (180 / (float) Math.PI) * radiants;
        }

        public static float DegreesToRadiants (float degrees) {
            return (float) Math.PI * degrees / 180;
        }


    }
}
