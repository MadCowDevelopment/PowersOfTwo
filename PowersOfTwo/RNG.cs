using System;

namespace PowersOfTwo
{
    public static class RNG
    {
        private static readonly Random _random = new Random((int) DateTime.Now.Ticks);

        public static int Next(int min, int max)
        {
            return _random.Next(min, max);
        }
    }
}