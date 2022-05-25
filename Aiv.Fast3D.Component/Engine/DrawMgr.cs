using System.Collections.Generic;

namespace Aiv.Fast3D.Component {

    public enum DrawLayer { Background, Middleground, Playground, Foreground, GUI, Last }

    static class DrawMgr {

        public static DepthTexture ShadowMap
        {
            get;
            private set;
        }

        private static List<IDrawable>[] items;

        static DrawMgr () {
            items = new List<IDrawable>[(int)DrawLayer.Last];
            for (int i = 0; i < items.Length; i++) {
                items[i] = new List<IDrawable> ();
            }
            ShadowMap = new DepthTexture (4096 , 4096 , 24);
        }

        public static void AddItem (IDrawable item) {
            items[(int) item.Layer].Add (item);
        }


        public static void Draw () {
            for (int i = 0; i < items.Length; i++) {
                for (int j = 0; j < items[i].Count; j++) {
                    if (!items[i][j].Enabled) continue;
                    items[i][j].Draw ();
                }
            }
        }

        public static void DrawShadow () {
            for (int i = 0; i < items.Length; i++) {
                for (int j = 0; j < items[i].Count; j++) {
                    if (!items[i][j].Enabled) continue;
                    items[i][j].DrawShadow ();
                }
            }
        }

        public static void ClearAll () {
            for (int i = 0; i < items.Length; i++) {
                items[i].Clear ();
            }
        }


    }
}
