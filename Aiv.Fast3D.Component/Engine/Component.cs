namespace Aiv.Fast3D.Component {
    public abstract class Component {

        protected bool enabled;
        public virtual bool Enabled
        {
            get { return  gameObject.IsActive && enabled; }
            set { enabled = value; }
        }

        public virtual bool EnableSelf
        {
            get { return enabled; }
        }
        public GameObject gameObject
        {
            get;
            private set;
        }
        public Transform transform
        {
            get { return gameObject.transform; }
        }

        public Component (GameObject owner) {
            gameObject = owner;
            enabled = true;
        }

        public T GetComponent<T> () where T : Component {
            return gameObject.GetComponent<T> ();
        }

        public abstract Component Clone(GameObject owner);


        public virtual void OnDestroy() {
        }
    }
}
