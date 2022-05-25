using System.Collections.Generic;

namespace Aiv.Fast3D.Component.AI {
    public class FSM : UserComponent {

        private Dictionary<int , State> states;
        private State currentState;

        private int startState;

        public FSM (GameObject owner) : base (owner) {
            states = new Dictionary<int , State> ();
            startState = int.MinValue;
        }

        public void RegisterState (int id, State state) {
            states.Add (id , state);
            state.AssignStateMachine (this);
        }

        public void SetStartState (int startState) {
            this.startState = startState;
        }

        //Switch e gestione degli eventi FSM
        public void Switch (int id) {
            if (currentState != null) {
                currentState.Exit ();
            }
            currentState = states[id];
            currentState.Enter ();
        }

        //GameLoop Wrapper
        public override void Awake () {
            foreach (State state in states.Values) {
                state.Awake ();
            }
        }

        public override void Start () {
            foreach (State state in states.Values) {
                state.Start ();
            }
            if (startState != int.MinValue) {
                Switch (startState);
            }
        }

        public override void Update () {
            if (currentState != null) {
                currentState.Update ();
            }
        }

        public override void LateUpdate () {
            if (currentState != null) {
                currentState.LateUpdate ();
            }
        }

        public override void OnCollide (Collision CollisionInfo) {
            if (currentState != null) {
                currentState.OnCollide (CollisionInfo);
            }
        }

        public override Component Clone (GameObject owner) {
            throw new System.NotImplementedException ();
        }

    }
}
