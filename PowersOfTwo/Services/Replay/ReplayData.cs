using System.Collections.Generic;

namespace PowersOfTwo.Services.Replay
{
    public class ReplayData
    {
        #region Constructors

        public ReplayData()
        {
            Events = new List<ReplayEvent>();
        }

        #endregion Constructors

        #region Public Properties

        public List<ReplayEvent> Events
        {
            get; private set;
        }

        #endregion Public Properties

        #region Public Methods

        public void AddEvent(ReplayEvent replayEvent)
        {
            Events.Add(replayEvent);
        }

        #endregion Public Methods
    }
}