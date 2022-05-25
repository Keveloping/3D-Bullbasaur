using OpenTK;

namespace Aiv.Fast3D.Component {
    public class EnemyMgr : UserComponent {
        private int numOfEnemies;
        public int NumOfEnemies {
            get { return numOfEnemies; }
            set { 
                numOfEnemies = value < 1 ? numOfEnemies = 10 : numOfEnemies = value;
            }
        }

        private Enemy[] enemies;
        public Enemy[] Enemies {
            get { return enemies; }
        }

        private Material[] materials;
        

        public EnemyMgr (GameObject owner, int poolSize) : base (owner) {
            CreateEnemyMaterials ();
            enemies = new Enemy[poolSize];
            for (int i = 0; i < enemies.Length; i++) {
                enemies[i] = CreateEnemy ();
                numOfEnemies++;
            }
        }


        public override void Update() {
            Respawn();
        }
        private void Respawn() {
            int counter = 0;
            for (int i = 0; i < enemies.Length; i++) {
                if (!enemies[i].gameObject.IsActive) {
                    counter++;
                }
            }
            if (counter == enemies.Length) {
                for (int i = 0; i < enemies.Length; i++) {
                    enemies[i].gameObject.IsActive = true;
                    enemies[i].Speed += 0.5f;
                    enemies[i].transform.Position = new Vector3(RandomGenerator.GetRandomFloat(-25, 25), 0, RandomGenerator.GetRandomFloat(-25, 25));
                }
            }
        }


        private void CreateEnemyMaterials () {
            materials = new Material[2];
            materials[0] = new Material ();
            materials[0].Ambient = Game.CurrentScene.AmbientLight;
            materials[0].ShadowMap = DrawMgr.ShadowMap;
            materials[0].Diffuse = GfxMgr.GetTexture ("Magikarp_Diffuse");
            materials[0].Lights = new Light[] { Game.CurrentScene.DirectionalLight };
            materials[0].NormalMap = GfxMgr.GetTexture("Magikarp_Diffuse_Norm");

            materials[1] = new Material ();
            materials[1].Ambient = Game.CurrentScene.AmbientLight;
            materials[1].ShadowMap = DrawMgr.ShadowMap;
            materials[1].Diffuse = GfxMgr.GetTexture ("Magikarp_Eyes_Diffuse");
            materials[1].Lights = new Light[] { Game.CurrentScene.DirectionalLight };
            materials[1].NormalMap = GfxMgr.GetTexture("Magikarp_Eyes_Norm");
        }


        private Enemy CreateEnemy () {
            Mesh3[] enemymeshes = SceneImporter.LoadMesh ("MyAssets/MagikarpF.FBX", new Vector3 (0.03f));
            GameObject enemy = new GameObject ("Enemy", new Vector3(RandomGenerator.GetRandomFloat(-25, 25), 0, RandomGenerator.GetRandomFloat(-25, 25)));
            enemy.Tag = "enemy";
            Rigidbody rb = enemy.AddComponent<Rigidbody> ();
            rb.Type = (uint) RigidbodyType.enemy;
            rb.AddCollisionType((uint)RigidbodyType.player);
            enemy.AddComponent (ColliderFactory.CreateSphereFor (enemy , 1 , new Vector3 (0 , 1 , 0) , false));
            enemy.AddComponent<MeshRenderer> (enemymeshes , materials);
            enemy.AddComponent<KeepInsideFloor>(-24.5f, 24.5f, -24.5f, 24.5f);
            return enemy.AddComponent<Enemy> ();
        }


        public override Component Clone (GameObject owner) {
            throw new System.NotImplementedException ();
        }
    }
}
