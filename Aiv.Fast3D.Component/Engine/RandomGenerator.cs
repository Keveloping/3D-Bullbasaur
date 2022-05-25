using System;

namespace Aiv.Fast3D.Component {
    public static class RandomGenerator {
        private static Random rand;

        static RandomGenerator () {
            rand = new Random ();
        }

        public static int GetRandomInt (int min , int max) {
            return rand.Next (min , max);
        }

        public static float GetRandomFloat (float min , float max) {
            return (float) (min + rand.NextDouble () * (max - min));
        }

    }
}
