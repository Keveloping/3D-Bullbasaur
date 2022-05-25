namespace Aiv.Fast3D.Component {
    class QuitSceneLogic : UserComponent {
        private string exitGame;


        public QuitSceneLogic(GameObject owner, string _exitGame) : base(owner) {
            exitGame = _exitGame;
        }


        public override void Update() {
            if (Input.GetButtonDown(exitGame)) {
                Game.TriggerChangeScene(null);
            }
        }


        public override Component Clone(GameObject owner) {
            return new QuitSceneLogic(owner, exitGame);
        }
    }
}
