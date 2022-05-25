using System.Collections.Generic;
using OpenTK;

namespace Aiv.Fast3D.Component {
    public class SheetAnimator : UserComponent {


        private SpriteRenderer spriteRenderer;
        private Dictionary<string , SheetClip> myClip;
        private SheetClip defaultClip;
        public SheetClip DefaultClip
        {
            set { defaultClip = value; }
        }
        private SheetClip currentClip;

        private int currentIndexFrame;
        private float sliceTime; // 1 / FPS della clip
        private float currentSliceTime; //quanto tempo è passato da quando ho messo l'ultimo frame.


        public SheetAnimator (GameObject owner, SpriteRenderer spriteRenderer) : base (owner) {
            this.spriteRenderer = spriteRenderer;
            myClip = new Dictionary<string , SheetClip> ();
        }

        public void AddClip (SheetClip clip) {
            myClip.Add (clip.AnimationName , clip);
            if (defaultClip == null) {
                defaultClip = clip;
            }
        }

        public override void Awake()
        {
            if (spriteRenderer == null)
            {
                spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
            }
        }

        public override void Start () {
            ChangeClip (defaultClip.AnimationName);
        }

        public void ChangeClip (string name) {
            if (!myClip.ContainsKey (name)) return;
            currentClip = myClip[name];
            sliceTime = 1f / currentClip.FPS;
            spriteRenderer.MyTexture = currentClip.Texture;
            currentIndexFrame = 0;
            SetNewFrame (currentClip.Frames[0]);
        }

        private void SetNewFrame (int index) {
            int rowIndex = index / currentClip.NumberOfColumn;
            int columnIndex = index % currentClip.NumberOfColumn;
            spriteRenderer.TextureOffset = new Vector2 (columnIndex * currentClip.FrameWidth , rowIndex * currentClip.FrameHeight);
            currentSliceTime = sliceTime;
        }

        public override void LateUpdate () {
            currentSliceTime -= Game.DeltaTime;
            if (currentSliceTime <= 0) {
                currentSliceTime = sliceTime;
                currentIndexFrame++;
                if (currentIndexFrame >= currentClip.Frames.Length) { //ultimo frame dell'animazione
                    if (currentClip.Loop) {
                        currentIndexFrame = 0;
                        SetNewFrame (currentClip.Frames[currentIndexFrame]);
                    } else {
                        if (!string.IsNullOrEmpty (currentClip.NextAnimation)) {
                            ChangeClip (currentClip.NextAnimation);
                        } else {
                            //rimango in questo frame per sempre --> magari è il frame di morte
                        }
                    }
                } else { //non è l'ultimo frame dell'animazione
                    SetNewFrame (currentClip.Frames[currentIndexFrame]);
                }
            }
        }

        public string GetCurrentAnimationName () {
            return currentClip.AnimationName;
        }

        public override Component Clone(GameObject owner)
        {
            SheetAnimator sa = new SheetAnimator(owner, owner.GetComponent<SpriteRenderer>());
            foreach (var clip in myClip)
            {
                sa.AddClip(clip.Value);
            }
            return sa;
        }

    }
}
