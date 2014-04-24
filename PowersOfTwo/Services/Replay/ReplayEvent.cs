using System;

namespace PowersOfTwo.Services.Replay
{
    public abstract class ReplayEvent
    {
        #region Constructors

        protected ReplayEvent()
        {
            RecordTime = DateTime.Now;
        }

        #endregion Constructors

        #region Public Properties

        public DateTime RecordTime
        {
            get; set;
        }

        #endregion Public Properties

        #region Public Methods

        public abstract void Replay(IReplayTarget replayTarget);

        #endregion Public Methods
    }
}