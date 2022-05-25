using OpenTK;
using Aiv.Fast2D;

namespace Aiv.Fast3D.Component {
    public class SpriteRenderer : Component, IDrawable {


        private Texture myTexture;
        public Texture MyTexture
        {
            get { return myTexture; }
            set { myTexture = value; }
        }
        private Sprite mySprite;
        public Sprite Sprite
        {
            get { return mySprite; }
        }
        private DrawLayer layer;
        public DrawLayer Layer
        {
            get { return layer; }
            set { layer = value; }
        }
        private Vector2 pivot;
        public Vector2 Pivot
        {
            get { return pivot; }
            set {
                pivot = value;
                mySprite.pivot = new Vector2 (mySprite.Width * pivot.X , mySprite.Height * pivot.Y);
            }
        }

        private float width;
        public float Width
        {
            get { return width * transform.Scale.X; }
            set { width = value; }
        }
        private float height;
        public float Height
        {
            get { return height * transform.Scale.Y; }
            set { height = value; }
        }
        private Vector2 textureOffset;
        public Vector2 TextureOffset
        {
            get { return textureOffset; }
            set { textureOffset = value; }
        }
        public Camera Camera
        {
            get { return mySprite.Camera; }
            set { mySprite.Camera = value; }
        }

        public SpriteRenderer(GameObject owner, DrawLayer layer) : base(owner)
        {
            Layer = layer;
            DrawMgr.AddItem(this);
        }

        public SpriteRenderer (GameObject owner, string textureName, Vector2 pivot, DrawLayer layer = DrawLayer.Background) : base (owner) {
            myTexture = GfxMgr.GetTexture (textureName);
            textureOffset = Vector2.Zero;
            width = Game.PixelsToUnit(myTexture.Width);
            height = Game.PixelsToUnit(myTexture.Height);
            mySprite = new Sprite (width , height);
            this.layer = layer;
            Pivot = pivot;
            DrawMgr.AddItem (this);
        }

        public SpriteRenderer (GameObject owner , string textureName , Vector2 pivot , float width, float height, DrawLayer layer = DrawLayer.Background) : base (owner) {
            myTexture = GfxMgr.GetTexture (textureName);
            textureOffset = Vector2.Zero;
            this.width = Game.PixelsToUnit (width);
            this.height = Game.PixelsToUnit(height);
            mySprite = new Sprite (this.width , this.height);
            this.layer = layer;
            Pivot = pivot;
            DrawMgr.AddItem (this);
        }

        public void DrawShadow () {
            return;
        }

        public void Draw () {
            mySprite.position = new Vector2 (transform.Position.X, transform.Position.Y); // in unità non in pixel
            mySprite.Rotation =  transform.Rotation.Z;
            mySprite.scale =  new Vector2 (transform.Scale.X, transform.Scale.Y);
            mySprite.DrawTexture (myTexture , (int) textureOffset.X , (int) textureOffset.Y , Game.UnitToPixels (width), Game.UnitToPixels(height));
        }

        public static SpriteRenderer Factory (GameObject owner , string textureName , Vector2 pivot , DrawLayer layer) {
            return new SpriteRenderer (owner , textureName , pivot , layer);
        }

        public static SpriteRenderer Factory (GameObject owner, string textureName, Vector2 pivot, DrawLayer layer, Vector2 textureOffset, float width, float height) {
            SpriteRenderer sr = new SpriteRenderer (owner , textureName , pivot , width , height , layer);
            sr.textureOffset = textureOffset;
            return sr;
        }

        public override Component Clone (GameObject owner)
        {
            SpriteRenderer sr = new SpriteRenderer(owner, Layer);
            sr.myTexture = myTexture;
            sr.mySprite = new Sprite(mySprite.Width, mySprite.Height);
            sr.width = width;
            sr.height = height;
            sr.Pivot = pivot;
            sr.textureOffset = textureOffset;
            sr.Camera = Camera;
            return sr;
        }

    }
}
