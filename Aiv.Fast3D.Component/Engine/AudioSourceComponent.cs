using Aiv.Audio;

namespace Aiv.Fast3D.Component {
    public enum AudioSourceStatus {
        play,
        pause,
        stop
    }

    public class AudioSourceComponent : Component {

        private AudioSourceStatus myStatus;

        private int myType;
        public int MyType
        {
            get { return myType; }
            set
            {
                myType = value < 0 ? 0 : value >= AudioClipMgr.NumberOfLayer ? AudioClipMgr.NumberOfLayer -1 : value;
                internalAudioSource.Volume = myVolume * AudioClipMgr.GetVolume (myType);
            }
        }

        private float myVolume;
        public float MyVolume
        {
            get { return myVolume; }
            set
            {
                myVolume = value < 0 ? 0 : value > 1 ? 1 : value;
                internalAudioSource.Volume = myVolume * AudioClipMgr.GetVolume (myType);
            }
        }

        private bool loop;
        public bool Loop
        {
            get { return loop; }
            set
            {
                loop = value;
            }
        }

        private AudioSource internalAudioSource;
        private AudioClip myAudioClip;

        public AudioSourceComponent (GameObject owner) : base (owner) {
            myStatus = AudioSourceStatus.stop;
            internalAudioSource = new AudioSource ();
            MyVolume = 1;
        }

        public void SetClip (AudioClip clip) {
            myAudioClip = clip;
        }

        public void Play () {
            if (myAudioClip == null) return;
            switch (myStatus) {
                case AudioSourceStatus.pause:
                    internalAudioSource.Resume ();
                    myStatus = AudioSourceStatus.play;
                    break;
                case AudioSourceStatus.stop:
                    internalAudioSource.Play (myAudioClip , loop);
                    myStatus = AudioSourceStatus.play;
                    break;
            }
        }

        public void Pause () {
            switch (myStatus) {
                case AudioSourceStatus.play:
                    internalAudioSource.Pause ();
                    myStatus = AudioSourceStatus.pause;
                    break;
            }
        }

        public void Stop () {
            switch (myStatus) {
                case AudioSourceStatus.play:
                case AudioSourceStatus.pause:
                    internalAudioSource.Stop ();
                    myStatus = AudioSourceStatus.stop;
                    break;
            }
        }

        public override Component Clone (GameObject owner) {
            AudioSourceComponent asc = new AudioSourceComponent (owner);
            asc.MyVolume = MyVolume;
            asc.Loop = Loop;
            asc.myAudioClip = myAudioClip;
            asc.MyType = MyType;
            return asc;
        }

        public void PlayOneShot (AudioClip clip) {
            AudioClipMgr.PlayOneShot (clip , myVolume * AudioClipMgr.GetVolume (myType));
        }
    }
}
