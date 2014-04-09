using System;

namespace PowersOfTwo.Core
{
    public static class RNG
    {
        #region Fields

        private static readonly Random Random = new Random((int)DateTime.Now.Ticks);

        #endregion Fields

        #region Public Static Methods

        public static int Next(int min, int max)
        {
            return Random.Next(min, max);
        }

        #endregion Public Static Methods
    }
}