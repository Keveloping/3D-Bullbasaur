using OpenTK;
using Aiv.Fast2D;

namespace Aiv.Fast3D.Component.UI {
    public class Font {

        protected int numCol;
        protected int firstVal; //codice ascii del prima carattere presente nella mia spritesheet
        public string TextureName { get; protected set; }
        public Texture Texture { get; protected set; }
        public int CharacterWidth { get; protected set; }
        public int CharacterHeight { get; protected set; }

        public Font (string textureName ,string texturePath, int numCol, int firstCharacterASCIIValue, int charWidth, int charHeight) {
            TextureName = textureName;
            Texture = GfxMgr.AddTexture (textureName , texturePath);
            firstVal = firstCharacterASCIIValue;
            CharacterWidth = charWidth;
            CharacterHeight = charHeight;
            this.numCol = numCol;
        }

        public virtual Vector2 Getoffset (char c) {
            int cVal = c;
            int delta = cVal - firstVal;
            int x = delta % numCol;
            int y = delta / numCol;
            return new Vector2 (x * CharacterWidth , y * CharacterHeight);
        }


    }
}
