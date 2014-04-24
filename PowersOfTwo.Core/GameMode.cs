using System;

namespace PowersOfTwo.Core
{
    public class ShortGameMode : GameMode
    {
        #region Public Properties

        public override int Id
        {
            get { return 1; }
        }

        public override int StartingPoints
        {
            get { return 2048; }
        }

        #endregion Public Properties
    }

    public class LongGameMode : GameMode
    {
        #region Public Properties

        public override int Id
        {
            get { return 2; }
        }

        public override int StartingPoints
        {
            get { return 4096; }
        }

        #endregion Public Properties
    }

    public class RankedGameMode : GameMode
    {
        #region Public Properties

        public override int Id
        {
            get { return 3; }
        }

        public override int StartingPoints
        {
            get { return 0; }
        }

        #endregion Public Properties
    }

    public abstract class GameMode
    {
        #region Public Properties

        public abstract int Id
        {
            get;
        }

        public abstract int StartingPoints
        {
            get;
        }

        #endregion Public Properties

        #region Public Static Methods

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

        #endregion Public Static Methods
    }
}