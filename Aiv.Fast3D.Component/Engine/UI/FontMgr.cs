using System.Collections.Generic;

namespace Aiv.Fast3D.Component.UI {
    public static class FontMgr {

        private static Dictionary<string , Font> fonts;
        private static Font defaultFont;

        static FontMgr () {
            fonts = new Dictionary<string , Font> ();
            defaultFont = null;
        }

        public static Font GetFont (string name) {
            if (fonts.ContainsKey (name)) {
                return fonts[name];
            }
            return defaultFont;
        }

        public static Font AddFont (string fontName , string texturePath , int numCol , int firstCharacterASCIIValue , int charWidth , int charHeight) {
            Font f = new Font (fontName , texturePath , numCol , firstCharacterASCIIValue , charWidth , charHeight);
            fonts.Add (fontName , f);
            if (defaultFont == null) {
                defaultFont = f;
            }
            return f;
        }

        public static void ClearAll () {
            fonts.Clear ();
            defaultFont = null;
        }

    }
}
