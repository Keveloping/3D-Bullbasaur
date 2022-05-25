using Aiv.Audio;
using System.Collections.Generic;

namespace Aiv.Fast3D.Component {
    public static class AudioClipMgr {

        private const int maxPlayOneShotSource = 40;
        private static AudioSource[] audioSourcePlayOneShot;


        private static float[] volumes;
        internal static int NumberOfLayer
        {
            get { return volumes.Length; }
        }

        private static Dictionary<string , AudioClip> clips;

        static AudioClipMgr () {
            clips = new Dictionary<string , AudioClip> ();
            volumes = new float[1];
            volumes[0] = 1;
            audioSourcePlayOneShot = new AudioSource[5];
            for (int i = 0; i < audioSourcePlayOneShot.Length; i++) {
                audioSourcePlayOneShot[i] = new AudioSource ();
            }
        }

        public static void Init (int numberOfLayer = 1) {
            if (numberOfLayer < 1) numberOfLayer = 1;
            volumes = new float[numberOfLayer];
            for (int i = 0; i < volumes.Length; i++) {
                volumes[i] = 0.5f;
            }
        }

        public static AudioClip AddClip (string name, string path) {
            AudioClip audioClip = new AudioClip (path);
            clips.Add (name , audioClip);
            return audioClip;
        }

        public static AudioClip GetClip (string name) {
            if (!clips.ContainsKey (name)) {
                return null;
            }
            return clips[name];
        }

        public static void ClearAll () {
            clips.Clear ();
        }

        public static void SetVolume (int layer, float value) {
            if (layer < 0 || layer >= volumes.Length) {
                //qualquadra non cosa
                return;
            }
            volumes[layer] = value;
        }

        public static float GetVolume (int layer) {
            if (layer < 0 || layer >= volumes.Length) {
                return -1;
            }
            return volumes[layer];
        }

        internal static void PlayOneShot (AudioClip clip, float volume) {
            for (int i = 0; i < audioSourcePlayOneShot.Length; i++) {
                if (!audioSourcePlayOneShot[i].IsPlaying) {
                    audioSourcePlayOneShot[i].Volume = volume;
                    audioSourcePlayOneShot[i].Play (clip);
                    return;
                }
            }
            if (audioSourcePlayOneShot.Length >= maxPlayOneShotSource) return;
            int newLength = audioSourcePlayOneShot.Length * 2 <=
                maxPlayOneShotSource ? audioSourcePlayOneShot.Length * 2 : maxPlayOneShotSource;
            AudioSource[] temp = new AudioSource[newLength];
            int j = 0;
            for (; j < audioSourcePlayOneShot.Length; j++) {
                temp[j] = audioSourcePlayOneShot[j];
            }
            for (; j < temp.Length; j++) {
                temp[j] = new AudioSource ();
            }
            PlayOneShot (clip , volume);
        }

    }
}