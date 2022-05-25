using OpenTK;

namespace Aiv.Fast3D.Component {
    public class BulletMgr : UserComponent {
        private Bullet[] bulletPool;
        private Material[] bulletMaterials;
        private int poolSize;

       
        public BulletMgr(GameObject owner, int _poolSize) : base(owner) {
            CreateBulletMaterials();
            poolSize = _poolSize;
            bulletPool = new Bullet[poolSize];
            for (int i = 0; i < bulletPool.Length; i++) {
                bulletPool[i] = CreateBullet(i);
            }
        }


        public Bullet GetBullet() {
            for (int i = 0; i < bulletPool.Length; i++) {
                if (!bulletPool[i].gameObject.IsActive) {
                    return bulletPool[i];             
                }
            }
            return null;
        }


        private void CreateBulletMaterials () {
            Material bulletMaterial = new Material ();
            bulletMaterial.Ambient = Game.CurrentScene.AmbientLight;
            bulletMaterial.Lights = new Light[] { Game.CurrentScene.DirectionalLight };
            bulletMaterial.Diffuse = GfxMgr.GetTexture ("Bullet_Diffuse");
            bulletMaterial.SpecularMap = GfxMgr.GetTexture ("Bullet_Specular");
            bulletMaterial.ShadowMap = DrawMgr.ShadowMap;
            bulletMaterials = new Material[] { bulletMaterial };
        }
        private Bullet CreateBullet (int id) {
            Mesh3[] meshes = new Mesh3[1] { new Sphere () };
            GameObject bullet = new GameObject ("Bullet_" + id , Vector3.Zero);
            bullet.AddComponent<MeshRenderer> (meshes , bulletMaterials);
            bullet.AddComponent (ColliderFactory.CreateSphereFor (bullet , ((Sphere) meshes[0]).Radius , Vector3.Zero , false));
            Rigidbody rb = bullet.AddComponent<Rigidbody> ();
            rb.IsGravityAffected = true;
            rb.Type = (uint) RigidbodyType.bullet;
            rb.AddCollisionType((uint)RigidbodyType.enemy);
            return bullet.AddComponent<Bullet> (10);
        }


        public override Component Clone (GameObject owner) {
            throw new System.NotImplementedException ();
        }
    }
}
