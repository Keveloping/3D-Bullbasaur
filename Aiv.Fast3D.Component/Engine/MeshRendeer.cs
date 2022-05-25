namespace Aiv.Fast3D.Component {
    public class MeshRenderer : Component, IDrawable {
        public bool castShadow;
        public bool receiveShadow;

        private Mesh3[] meshes;
        public Mesh3[] Meshes
        {
            get { return meshes; }
        }
        private Material[] materials;

        public DrawLayer Layer
        {
            get { return DrawLayer.Background; }
        }
        
        public MeshRenderer (GameObject owner, Mesh3[] meshes, Material[] materials) : base (owner) {
            this.meshes = meshes;
            this.materials = materials;
            for (int i = 0; i < materials.Length; i++) {
                materials[i].ShadowMap = DrawMgr.ShadowMap;
                //materials[i].ShadowBias = 0.01f;
            }
            receiveShadow = true;
            castShadow = true;
            DrawMgr.AddItem (this);
        }

        public void Draw () {
            for (int i = 0; i <meshes.Length; i++) {
                materials[i].ShadowMap = receiveShadow ? DrawMgr.ShadowMap : null;
                meshes[i].Position3 = transform.Position;
                meshes[i].EulerRotation3 = transform.EulerRotation;
                meshes[i].Scale3 = transform.Scale;
                meshes[i].DrawPhong (materials[i]);
            }
        }


        public void DrawShadow () {
            if (!castShadow) return;
            for (int i = 0; i < meshes.Length; i++) {
                meshes[i].DrawShadowMap (Game.CurrentScene.DirectionalLight);
            }
        }
        public override Component Clone (GameObject owner) {
            return new MeshRenderer (owner , (Mesh3[]) meshes.Clone () , (Material[]) materials.Clone ());
        }
    }
}
