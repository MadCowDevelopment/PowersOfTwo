using System.Collections.Generic;
using System.Globalization;
using System.Linq;

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
            get;
            private set;
        }

        #endregion Public Properties

        #region Private Properties

        public bool IsSinglePlayer
        {
            get { return (Events.First() as GameStartedEvent).Player2 == null; }
        }

        private string Player1
        {
            get { return (Events.First() as GameStartedEvent).Player1; }
        }

        private string Player2
        {
            get { return (Events.First() as GameStartedEvent).Player2; }
        }

        #endregion Private Properties

        #region Public Methods

        public void AddEvent(ReplayEvent replayEvent)
        {
            Events.Add(replayEvent);
        }

        public string GetFilename()
        {
            var players = IsSinglePlayer ? "Solo" : string.Format("{0} vs {1}", Player1, Player2);
            var time = Events.First().RecordTime.ToString("yyyy-MM-dd-HH-mm-ss", CultureInfo.InvariantCulture);
            return string.Format("{0}_{1}.potr", players, time);
        }

        #endregion Public Methods
    }
}