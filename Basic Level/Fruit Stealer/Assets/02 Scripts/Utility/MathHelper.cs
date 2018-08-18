using System;

namespace FruitStealer
{
    public static class MathHelper
    {
        private static Random random = new Random(Configuration.RandomSeedNumber);

        public static bool RandomBool(double probability = 0.5f)
        {
            if (probability < 0.0f || probability > 1.0f)
                throw new IndexOutOfRangeException("Probability must be in range [0.0, 1.0]");

            return random.NextDouble() <= probability;
        }

        public static int Range(int min, int max)
        {
            if (min >= max)
                throw new InvalidOperationException("Min must be lower than max");

            return random.Next(min, max);
        }

        public static float Range(double min, double max)
        {
            if (min >= max)
                throw new InvalidOperationException("Min must be lower than max");

            return (float) (min + random.NextDouble() * (max - min));
        }
    }
}