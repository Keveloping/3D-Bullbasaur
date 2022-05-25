using OpenTK;
using Aiv.Fast2D;

namespace Aiv.Fast3D.Component.UI {
    public class TextBox : Component, IDrawable {

        public DrawLayer Layer { get { return DrawLayer.GUI; } }

        private Font myFont;
        private Sprite sprite;
        private Vector2[] availableCharacters_positions;
        private Vector2[] availableCharacters_offset;
        private string currentText;

        public Camera Camera
        {
            get { return sprite.Camera; }
            set { sprite.Camera = value; }
        }

        public int MaxCharacters
        {
            get { return availableCharacters_positions.Length; }
        }

        public TextBox (GameObject owner , Font font , int maxCharacters , Vector2 fontScale) : base (owner) {

            currentText = string.Empty;
            myFont = font;
            availableCharacters_positions = new Vector2[maxCharacters];
            availableCharacters_offset = new Vector2[maxCharacters];
            sprite = new Sprite (Game.PixelsToUnit(font.CharacterWidth * fontScale.X) , Game.PixelsToUnit (font.CharacterHeight * fontScale.Y));

            DrawMgr.AddItem (this);

        }

        public void SetText (string text) {
            if (currentText == text) return; //super mega ottimizzazione
            currentText = text;
            float xPos = transform.Position.X;
            int xIndex = 0;
            float yPos = transform.Position.Y;
            int maxIndex = GetMax ();
            for (int i = 0; i < maxIndex; i++) {
                if (currentText[i].Equals ('\n')) {
                    xIndex = 0;
                    yPos += myFont.CharacterHeight;
                    continue;
                }
                availableCharacters_offset[i] = myFont.Getoffset (currentText[i]);
                availableCharacters_positions[i].X = xIndex * sprite.Width + xPos;
                availableCharacters_positions[i].Y = yPos;
                xIndex++;
            }
        }


        public void DrawShadow () {
            return;
        }

        public void Draw () {
            int maxIndex = GetMax ();
            for (int i = 0; i < maxIndex; i++) {
                sprite.position = availableCharacters_positions[i];
                sprite.DrawTexture (myFont.Texture , (int) availableCharacters_offset[i].X , (int) availableCharacters_offset[i].Y , myFont.CharacterWidth , myFont.CharacterHeight);
            }
        }

        private int GetMax () {
            return currentText.Length < availableCharacters_positions.Length ? currentText.Length : availableCharacters_positions.Length;
        }

        public override Component Clone(GameObject owner)
        {
            throw new System.NotImplementedException();
        }

    }
}
