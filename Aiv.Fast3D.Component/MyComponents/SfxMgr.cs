using System;
using Aiv.Audio;

namespace Aiv.Fast3D.Component {
    enum SfxIndex {
        EnemyDeath,
        PlayerShoot,
        BulletRebound,
        IncreasedSensitivity,
        ReduceSensitivity,
        PlayerHit,
        last
    }

    public class SfxMgr : UserComponent {
        private AudioClip[] mySFX;
        private AudioSourceComponent myAudioSource;

        public SfxMgr(GameObject owner) : base(owner) {
            mySFX = new AudioClip[(int)SfxIndex.last];
            for (int i = 0; i < mySFX.Length; i++) {
                mySFX[i] = AudioClipMgr.GetClip(Enum.GetNames(typeof(SfxIndex))[i]);
            }
        }


        public override void Awake() {
            myAudioSource = GetComponent<AudioSourceComponent>();
        }


        public override void Start() {
            EventHandlerManager.AddListener((int)EventsEnum.EnemyDeath, OnEnemyDeath);
            EventHandlerManager.AddListener((int)EventsEnum.PlayerShoot, OnPlayerShoot);
            EventHandlerManager.AddListener((int)EventsEnum.BulletRebound, OnBulletRebound);
            EventHandlerManager.AddListener((int)EventsEnum.ChangeSensitivity, OnChangeSensitivity);
            EventHandlerManager.AddListener((int)EventsEnum.PlayerHit, OnPlayerHit);
        }
        public override void OnDestroy() {
            EventHandlerManager.RemoveListener((int)EventsEnum.EnemyDeath, OnEnemyDeath);
            EventHandlerManager.RemoveListener((int)EventsEnum.PlayerShoot, OnPlayerShoot);
            EventHandlerManager.RemoveListener((int)EventsEnum.BulletRebound, OnBulletRebound);
            EventHandlerManager.RemoveListener((int)EventsEnum.ChangeSensitivity, OnChangeSensitivity);
            EventHandlerManager.RemoveListener((int)EventsEnum.PlayerHit, OnPlayerHit);
        }


        public void OnEnemyDeath(EventArgs message) {
            PlaySFX(SfxIndex.EnemyDeath);
        }
        public void OnPlayerShoot(EventArgs message) {
            PlaySFX(SfxIndex.PlayerShoot);
        }
        public void OnBulletRebound(EventArgs message) {
            PlaySFX(SfxIndex.BulletRebound);
        }
        public void OnPlayerHit(EventArgs message) {
            PlaySFX(SfxIndex.PlayerHit);
        }
        public void OnChangeSensitivity(EventArgs message) {
            ChangeSensitivityArgs castedMessage = message as ChangeSensitivityArgs;
            if (castedMessage == null) return;

            switch (castedMessage.button) {
                case "IncreasedSensitivity":
                    PlaySFX(SfxIndex.IncreasedSensitivity);
                    break;
                case "ReduceSensitivity":
                    PlaySFX(SfxIndex.ReduceSensitivity);
                    break;
            }
        }
        private void PlaySFX(SfxIndex sfxIndex) {
            myAudioSource.PlayOneShot(mySFX[(int)sfxIndex]);
        }


        public override Component Clone(GameObject owner) {
            return new SfxMgr(owner);
        }
    }  
}
