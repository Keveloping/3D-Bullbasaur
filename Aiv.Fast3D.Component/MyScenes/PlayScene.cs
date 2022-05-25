using Aiv.Fast2D;
using OpenTK;
using Aiv.Fast3D.Component.UI;

namespace Aiv.Fast3D.Component {
    public class PlayScene : Scene{
        protected override void LoadAssets () {
            //Player textures
            GfxMgr.AddTexture ("Player_Diffuse" , "MyAssets/Final_Pokemon_Diffuse.png");
            GfxMgr.AddTexture ("Player_Glossiness" , "MyAssets/Final_Pokemon_Glossiness.jpg");

            //Magikarp textures
            GfxMgr.AddTexture ("Magikarp_Diffuse" , "MyAssets/pm0129_00_Body1.png");
            GfxMgr.AddTexture ("Magikarp_Eyes_Diffuse", "MyAssets/pm0129_00_Eye1.png");
            GfxMgr.AddTexture ("Magikarp_Diffuse_Norm", "MyAssets/pm0129_00_BodyNor.png");
            GfxMgr.AddTexture ("Magikarp_Eyes_Norm", "MyAssets/pm0129_00_EyeNor.png");

            //Bullet textures
            GfxMgr.AddTexture ("Bullet_Diffuse" , "MyAssets/cartoon_grass.png");
            GfxMgr.AddTexture ("Bullet_Specular" , "MyAssets/cartoon_grass_spec.jpg");

            //Floor Textures
            GfxMgr.AddTexture ("Floor_Diffuse" , "MyAssets/grass.png");
            GfxMgr.AddTexture ("Magma_Diffuse" , "MyAssets/grass.png");
            Texture floorDiffuse = GfxMgr.GetTexture ("Floor_Diffuse");
            floorDiffuse.SetRepeatX ();
            floorDiffuse.SetRepeatY ();

            //Grass biliboard
            GfxMgr.AddTexture ("Grass_Billboard_Diffuse" , "MyAssets/grass_billboard2.png");

            //AudioClip
            AudioClipMgr.AddClip("EnemyDeath", "MyAssets/MySounds/Death02.wav");
            AudioClipMgr.AddClip("PlayerShoot", "MyAssets/MySounds/Attack02.wav");
            AudioClipMgr.AddClip("BulletRebound", "MyAssets/MySounds/Jump02.wav");
            AudioClipMgr.AddClip("PlayerHit", "MyAssets/MySounds/Hurt01.wav");
            AudioClipMgr.AddClip("ReduceSensitivity", "MyAssets/MySounds/MenuBack01.wav");
            AudioClipMgr.AddClip("IncreasedSensitivity", "MyAssets/MySounds/MenuValid01.wav");

            //AudioClipSceneMusic
            AudioClipMgr.AddClip("PlaySceneMusic", "MyAssets/MySounds/PlaySceneMusic.wav");

            //Font
            FontMgr.AddFont("stdFont", "MyAssets/textSheet.png", 15, 32, 20, 20);
        }


        public override void InizializeScene () {
            base.InizializeScene ();
            CreateLights ();
            CreateCamera ();
            CreateFloor ();
            CreatePlayer ();
            CreateBullet ();
            CreateBillboards();
            CreateEnemies ();
            CreateSFXManager();
            CreateQuitSceneLogic();
            CreatePauseUI();
            CreateAudioObject();
            CreateUiMgr();
        }
        private void CreateLights () {
            DirectionalLight = new DirectionalLight (Utils.EulerRotationToDirection (new Vector3 (-45 , 180 , 0)));
            AmbientLight = new Vector3 (0.6f , 0.6f , 0.5f);
        }
        private void CreateCamera () {
            GameObject mainCamera = new GameObject ("MainCamera" , new Vector3 (0 , 5 , -10));
            mainCamera.AddComponent<CameraComponent> (90 , 0.01f , 1000f);
            mainCamera.AddComponent<CameraOrbital> (new Vector3 (0 , 2 , -5), 0.2f, 1.5f);

            CameraMgr.AddCamera("GUI", new Camera(), 0);
        }
        private void CreateFloor () {
            GameObject floor = new GameObject ("Floor" , Vector3.Zero);
            floor.transform.Scale = new Vector3 (50 , 50 , 1);
            floor.transform.EulerRotation -= Vector3.UnitX * 90;
            Material floorMaterial = new Material ();
            floorMaterial.Ambient = AmbientLight;
            floorMaterial.Lights = new Light[] { DirectionalLight };
            floorMaterial.Diffuse = GfxMgr.GetTexture ("Floor_Diffuse");
            floorMaterial.ShadowMap = DrawMgr.ShadowMap;
            Mesh3 floorMesh = new Plane ();
            floor.AddComponent<MeshRenderer> (
                    new Mesh3[1] { floorMesh } ,
                    new Material[1] { floorMaterial }
                );
        }
        private void CreatePlayer () {
            GameObject player = new GameObject ("Player" , Vector3.Zero);
            player.Tag = "player";
            Material playerMaterial = new Material ();
            playerMaterial.Ambient = AmbientLight;
            playerMaterial.Lights = new Light[] { directionLight };
            playerMaterial.Diffuse = GfxMgr.GetTexture ("Player_Diffuse");
            playerMaterial.SpecularMap = GfxMgr.GetTexture ("Player_Glossiness");
            playerMaterial.ShadowMap = DrawMgr.ShadowMap;
            Mesh3[] playerMeshes = SceneImporter.LoadMesh ("MyAssets/Pokemon.obj");
            Material[] playerMaterials = new Material[playerMeshes.Length];
            for (int i = 0; i < playerMaterials.Length; i++) {
                playerMaterials[i] = playerMaterial;
            }
            player.AddComponent<MeshRenderer> (playerMeshes , playerMaterials);
            player.AddComponent<PlayerController> (10, 5);
            Rigidbody rb = player.AddComponent<Rigidbody> ();
            player.AddComponent(ColliderFactory.CreateSphereFor(player, 1, new Vector3(0, 1, 0), false));
            rb.Type = (uint)RigidbodyType.player;
            player.AddComponent<ReturnInsideFloor>(-24.5f, 24.5f, -24.5f, 24.5f);
        }
        private void CreateBullet () {
            GameObject bulletMgr = new GameObject ("BulletMgr" , Vector3.Zero);
            bulletMgr.AddComponent<BulletMgr> (10);
        }
        private void CreateEnemies () {
            GameObject enemyMgr = new GameObject ("EnemyMgr" , Vector3.Zero);
            enemyMgr.AddComponent<EnemyMgr> (10);
        }
        private void CreateBillboards() {
            Billboard.CreateBillboardMaterial();
            for (int i = 0; i < 10; i++) {
                GameObject billboard = new GameObject("Billboard_" + i, Vector3.Zero);
                billboard.AddComponent<Billboard>(i);
            }
        }
        private void CreateSFXManager() {
            GameObject sfxObj = new GameObject("SFXManager", Vector3.Zero);
            AudioSourceComponent asc = sfxObj.AddComponent<AudioSourceComponent>();
            asc.MyType = (int)AudioLayer.sfx;
            sfxObj.AddComponent<SfxMgr>();
        }
        private void CreateQuitSceneLogic() {
            GameObject quitSceneLogic = new GameObject("QuitSceneLogic", Vector3.Zero);
            quitSceneLogic.AddComponent<QuitSceneLogic>("ExitGame");
        }
        private void CreatePauseUI() {
            Font font = FontMgr.GetFont("stdFont");

            GameObject pauseWrite = new GameObject("PauseWrite", new Vector3(Game.Win.OrthoWidth * 0.5f - 1, 1 , 0));
            pauseWrite.AddComponent<TextBox>(font, 5, Vector2.One * 2).SetText("Pause");
            pauseWrite.GetComponent<TextBox>().Camera = CameraMgr.GetCamera("GUI");
            GameObject pauseLogic = new GameObject("PauseLogic", Vector3.Zero);
            pauseLogic.AddComponent<PauseLogic>();
        }
        private void CreateAudioObject() {
            GameObject audio = new GameObject("Audio", Vector3.Zero);
            AudioSourceComponent asc = audio.AddComponent<AudioSourceComponent>();
            asc.SetClip(AudioClipMgr.GetClip("PlaySceneMusic"));
            asc.MyType = (int)AudioLayer.music;
            asc.Loop = true;
            asc.Play();
        }
        private void CreateUiMgr() {
            Font font = FontMgr.GetFont("stdFont");

            Vector3 hpUIPosition = new Vector3(0.3f, 0.3f, 0);
            GameObject hpUI = new GameObject("HpUI", hpUIPosition);
            hpUI.AddComponent<TextBox>(font, 15, Vector2.One);
            hpUI.GetComponent<TextBox>().Camera = CameraMgr.GetCamera("GUI");
            Vector3 bulletUIPosition = new Vector3(0.3f, 0.7f, 0);
            GameObject bulletUI = new GameObject("BulletUI", bulletUIPosition);
            bulletUI.AddComponent<TextBox>(font, 15, Vector2.One).SetText("BULLET = FULL");
            bulletUI.GetComponent<TextBox>().Camera = CameraMgr.GetCamera("GUI");
            Vector3 enemiesUIPosition = new Vector3(0.3f, 1.1f, 0);
            GameObject enemiesUI = new GameObject("EnemiesUI", enemiesUIPosition);
            enemiesUI.AddComponent<TextBox>(font, 15, Vector2.One);
            enemiesUI.GetComponent<TextBox>().Camera = CameraMgr.GetCamera("GUI");
            Vector3 ScorePosition = new Vector3(0.3f, 1.5f, 0);
            GameObject score = new GameObject("Score", ScorePosition);
            score.AddComponent<TextBox>(font, 30, Vector2.One);
            score.GetComponent<TextBox>().Camera = CameraMgr.GetCamera("GUI");

            GameObject rudimentaryUiMgr = new GameObject("ScoreMgr", Vector3.Zero);
            rudimentaryUiMgr.AddComponent<UiMgr>();         
        }
    }
}
