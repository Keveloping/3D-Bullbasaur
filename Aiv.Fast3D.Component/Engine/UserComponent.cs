

namespace Aiv.Fast3D.Component {
    public abstract class UserComponent : Component, IStartable, IUpdatable, ICollidable {

        public override bool Enabled {
            get
            {
                return base.Enabled;
            }
            set
            {
                if (gameObject.IsActive) {
                    if (!Enabled && value) { //non è attivo e lo voglio abilitare (value = true)
                        OnEnable ();
                    } else if (Enabled && !value) { //è attivo e lo voglio disabilitare (value = false)
                        OnDisable ();
                    }
                }
                base.Enabled = value;
            }
        }

        public UserComponent (GameObject owner) : base (owner) {

        }

        public virtual void Awake () {

        }

        public virtual void Start () {

        }

        public virtual void Update () {

        }

        public virtual void LateUpdate () {

        }

        public virtual void OnCollide (Collision CollisionInfo) {

        }

        public virtual void OnEnable () {

        }

        public virtual void OnDisable () {

        }

    }
}
