using Aiv.Fast2D;

namespace Aiv.Fast3D.Component {
    public class Sheet {

        public Texture Texture
        {
            get;
            private set;
        }
        public int NumberOfColumn
        {
            get;
            private set;
        }
        public int NumberOfRow
        {
            get;
            private set;
        }
        public float FrameWidth
        {
            get { return Texture.Width / NumberOfColumn; }
        }
        public float FrameHeight
        {
            get { return Texture.Height / NumberOfRow; }
        }

        public Sheet (Texture texture, int numberOfColumn, int numberOfRow) {
            Texture = texture;
            NumberOfColumn = numberOfColumn;
            NumberOfRow = numberOfRow;
        }

    }
}
