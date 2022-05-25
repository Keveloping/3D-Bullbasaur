using Aiv.Fast2D;

namespace Aiv.Fast3D.Component {
    public static class Game {
        public static float WorkingHeight;
        public static float UnitSize { get; private set; }
        public static float Gravity { get; set; }

        private static bool changeScene;
        private static Scene nextScene;
        private static bool firstFrame;

        public static Window Win;
        private static float timeScale = 1;
        public static float TimeScale
        {
            get { return timeScale; }
            set
            {
                timeScale = value < 0 ? 0 : value;
            }
        }
        public static bool IsRunning;
        public static float DeltaTime
        {
            get {
                if (firstFrame) {
                    return 0;
                } else {
                    return Win.DeltaTime * timeScale;
                }
            }
        }
        private static Scene currentScene;
        public static Scene CurrentScene
        {
            get { return currentScene; }
        }

        public static void Init (string windowName , int windowWidth , int windowHeight , Scene startScene , float workingHeight, float ortographicSize, float zNear, float zFar) {
            Win = new Window (windowWidth , windowHeight , windowName);
            WorkingHeight = workingHeight;
            Win.SetDefaultViewportOrthographicSize (ortographicSize);
            UnitSize = WorkingHeight / Win.OrthoHeight;
            Win.SetVSync (false);
            Win.SetZNearZFar (zNear , zFar);
            Win.SetClearColor (0.9f , 0.4f , 1f , 1f);
            Win.EnableDepthTest ();
            ChangeScene (startScene);
        }

        public static float PixelsToUnit (float pixelSize) {
            return pixelSize / UnitSize;
        }

        public static int UnitToPixels (float unitSize)
        {
            return (int) (unitSize * UnitSize);
        }

        public static void Play () {
            IsRunning = true;
            while (Win.IsOpened && IsRunning) {

                //Game loop

                PhysicsMgr.FixedUpdate ();
                PhysicsMgr.CheckCollisions ();


                currentScene.Update ();
                currentScene.LateUpdate ();

                CameraMgr.MoveCameras();

                Game.Win.RenderTo (DrawMgr.ShadowMap);
                DrawMgr.DrawShadow ();
                Game.Win.RenderTo (null);
                DrawMgr.Draw ();
                firstFrame = false;
                if (changeScene) {
                    changeScene = false;
                    ChangeScene (nextScene);
                }

                //Fine Game loop

                Input.PerformLastKey ();

                Win.Update ();
            }
        }

        private static void ChangeScene (Scene nextScene) {
            if (currentScene != null) {
                currentScene.DestroyScene ();
            }
            if (nextScene == null) {
                IsRunning = false;
                return;
            }
            currentScene = nextScene;
            currentScene.InizializeScene ();
            currentScene.Awake ();
            currentScene.Start ();
            currentScene.IsInizialized = true;
            firstFrame = true;
        }

        public static void TriggerChangeScene (Scene nextScene) {
            changeScene = true;
            Game.nextScene = nextScene;
        }
    }
}
