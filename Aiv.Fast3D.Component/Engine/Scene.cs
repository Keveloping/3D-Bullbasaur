using System;
using System.Collections.Generic;
using Aiv.Fast3D.Component.UI;
using OpenTK;


namespace Aiv.Fast3D.Component {


    public abstract class Scene {

        protected DirectionalLight directionLight;
        public DirectionalLight DirectionalLight
        {
            get { return directionLight; }
            set {
                directionLight = value;
                directionLight.SetShadowProjection (-30 , 30 , -30 , 30 , -30 , 30);
            }
        }
        protected Vector3 ambientLight;
        public Vector3 AmbientLight
        {
            get { return ambientLight; }
            set { ambientLight = value; }
        }


        protected List<GameObject> sceneObjects = new List<GameObject> ();
        protected bool isInizialized;
        public bool IsInizialized
        {
            get { return isInizialized; }
            set { isInizialized = value; }
        }

        public virtual void InizializeScene () {
            LoadAssets ();
        }

        protected virtual void LoadAssets () {

        }

        public void Awake () {
            foreach (GameObject go in sceneObjects) {
                if (!go.IsActive) continue;
                go.Awake ();
            }
        }

        public void Start () {
            foreach (GameObject go in sceneObjects) {
                if (!go.IsActive) continue;
                go.Start ();
            }
        }

        public virtual void Update () {
            foreach (GameObject go in sceneObjects) {
                if (!go.IsActive) continue;
                go.Update ();
            }
        }

        public void LateUpdate () {
            foreach (GameObject go in sceneObjects) {
                if (!go.IsActive) continue;
                go.LateUpdate ();
            }
        }

        public GameObject FindGameObject (string name) {
            foreach (GameObject obj in sceneObjects) {
                if (obj.Name == name) {
                    return obj;
                }
            }
            return null;
        }

        public GameObject[] FindGameObjectsByTag (string tag) {
            List<GameObject> gameObjectsTagged = new List<GameObject> ();
            foreach (GameObject obj in sceneObjects) {
                if (obj.Tag == tag) {
                    gameObjectsTagged.Add (obj);
                }
            }
            return gameObjectsTagged.ToArray ();
        }
        
        public void RegisterGameObject (GameObject go) {
            sceneObjects.Add (go);
        }

        public virtual void DestroyScene () {
            PhysicsMgr.ClearAll ();
            DrawMgr.ClearAll ();
            GfxMgr.ClearAll ();
            FontMgr.ClearAll ();
            CameraMgr.ClearAll ();
            AudioClipMgr.ClearAll ();
            EventHandlerManager.ClearListeners ();
            sceneObjects.Clear ();
            GC.Collect ();
        }

    }
}
