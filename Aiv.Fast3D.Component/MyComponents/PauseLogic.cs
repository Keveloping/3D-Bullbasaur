namespace Aiv.Fast3D.Component {
    class PauseLogic : UserComponent{
        private bool inPause;
        public bool InPause {
            get { return inPause; }
        }

        private AudioSourceComponent audioSource;
        private GameObject pauseObject;
       

        public PauseLogic(GameObject owner) : base(owner) {

        }


        public override void Awake() {
            audioSource = Game.CurrentScene.FindGameObject("Audio").GetComponent<AudioSourceComponent>();
            pauseObject = GameObject.Find("PauseWrite");
            pauseObject.IsActive = false;
        }


        public override void Update() {
            if (Input.GetButtonDown("PauseGame")) {
                inPause = !inPause;
                if (inPause == true) {
                    Game.Win.SetClearColor(0, 0, 0, 0);
                    audioSource.Pause();
                    pauseObject.IsActive = inPause;
                }
                else {
                    Game.Win.SetClearColor(0.9f, 0.4f, 1f, 1f);
                    audioSource.Play();
                    pauseObject.IsActive = inPause;
                }
                Game.TimeScale = inPause ? 0 : 1;
            }
        }

 
        public override Component Clone(GameObject owner) {
            return new PauseLogic(owner);
        }
    }
}
