using System;

namespace PowersOfTwo.Core
{
    public class ShortGameMode : GameMode
    {
        public override int StartingPoints
        {
            get { return 2048; }
        }

        public override int Id
        {
            get { return 1; }
        }
    }

    public class LongGameMode : GameMode
    {
        public override int StartingPoints
        {
            get { return 4096; }
        }

        public override int Id
        {
            get { return 2; }
        }
    }

    public class RankedGameMode : GameMode
    {
        public override int StartingPoints
        {
            get { return 0; }
        }

        public override int Id
        {
            get { return 3; }
        }
    }

    public abstract class GameMode
    {
        public abstract int StartingPoints { get; }

        public abstract int Id { get; }

        public static GameMode FromId(int gameModeId)
        {
            switch (gameModeId)
            {
                case 1:
                    return new ShortGameMode();
                case 2:
                    return new LongGameMode();
                case 3:
                    return new RankedGameMode();
                default:
                    throw new InvalidOperationException("Game mode is not supported.");
            }
        }
    }
}
