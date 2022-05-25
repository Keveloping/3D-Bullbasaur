namespace Aiv.Fast3D.Component {

    interface IDrawable {

        bool Enabled { get; }
        DrawLayer Layer { get; }
        void Draw ();

        void DrawShadow ();

    }
}
