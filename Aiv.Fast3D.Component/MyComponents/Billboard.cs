using OpenTK;

namespace Aiv.Fast3D.Component {
    class Billboard : UserComponent {
        private static Material[] billboardMaterial;

        private float distanceToCamera = 400f; //distanza al quadrato che deve esserci dalla camera per switchare da complex a simple  e viceversa

        private GameObject complexObj;
        private GameObject simpleObj;

        private Transform cameraTransform;

        public static void CreateBillboardMaterial() {
            billboardMaterial = new Material[1];
            billboardMaterial[0] = new Material();
            billboardMaterial[0].Diffuse = GfxMgr.GetTexture("Grass_Billboard_Diffuse");
            billboardMaterial[0].Ambient = Game.CurrentScene.AmbientLight;
            billboardMaterial[0].Lights[0] = Game.CurrentScene.DirectionalLight;
            billboardMaterial[0].ShadowMap = DrawMgr.ShadowMap;
        }

        public static GameObject CreateComplexObj(int id) {
            Mesh3[] meshes = SceneImporter.LoadMesh("MyAssets/grass.obj", new Vector3(0.02f));
            GameObject complexObj = new GameObject("ComplexBillboard_" + id, Vector3.Zero);
            complexObj.AddComponent<MeshRenderer>(meshes, billboardMaterial);
            return complexObj;
        }

        public static GameObject CreateSimpleObj(int id) {
            Mesh3[] meshes = new Mesh3[1];
            meshes[0] = new Plane();
            GameObject simpleObj = new GameObject("SimpleBillboard_" + id, Vector3.Zero);
            MeshRenderer mr = simpleObj.AddComponent<MeshRenderer>(meshes, billboardMaterial);
            mr.castShadow = false;
            mr.receiveShadow = false;
            return simpleObj;
        }

        public Billboard(GameObject owner, int id) : base(owner) {
            complexObj = CreateComplexObj(id);
            simpleObj = CreateSimpleObj(id);
            transform.Position = new Vector3(RandomGenerator.GetRandomFloat(-25, 25), 0, RandomGenerator.GetRandomFloat(-25, 25));
            complexObj.transform.Position = transform.Position;
            simpleObj.transform.Position = new Vector3(transform.Position.X, transform.Position.Y + 0.5f, transform.Position.Z);
        }

        public override void Awake() {
            cameraTransform = GameObject.Find("MainCamera").transform;
        }

        public override void LateUpdate() {
            float distance = (transform.Position - cameraTransform.Position).LengthSquared;
            complexObj.IsActive = distance < distanceToCamera;
            simpleObj.IsActive = !complexObj.IsActive;
            simpleObj.transform.Rotation = Utils.LookAt(cameraTransform.Position - transform.Position);
        }

        public override Component Clone(GameObject owner) {
            throw new System.NotImplementedException();
        }
    }
}
