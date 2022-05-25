
namespace Aiv.Fast3D.Component.AI {
    public abstract class State {

        protected FSM machine;

        public void AssignStateMachine (FSM stateMachine) {
            machine = stateMachine;
        }
        
        //GameLoop Wrapper
        public virtual void Awake () {

        }

        public virtual void Start () {

        }

        public virtual void Update () {

        }

        public virtual void LateUpdate () {

        }

        public virtual void OnCollide (Collision colliiosn) {

        }
        
        //FSM Events
        public virtual void Enter () {

        }

        public virtual void Exit () {

        }

    }
}
