using System.Collections.Generic;
using Aiv.Fast2D;

namespace Aiv.Fast3D.Component {
    static class GfxMgr {

        private static Dictionary<string , Texture> textures;

        static GfxMgr () {
            textures = new Dictionary<string , Texture> ();
        }

        public static Texture AddTexture (string name, string path) {
            Texture texture = new Texture (path);
            textures.Add (name , texture);
            return texture;
        }

        public static Texture GetTexture (string name) {
            return textures[name];
        }

        public static void ClearAll () {
            textures.Clear ();
        }

    }
}
