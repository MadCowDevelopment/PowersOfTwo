using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace PowersOfTwo.Services.Replay
{
    public class ReplayData
    {

        #region Constructors

        public ReplayData(int points, string player1Name, List<int?> player1Cells, string player2Name, List<int?> player2Cells)
        {
            Points = points;
            Player1Name = player1Name;
            Player1Cells = player1Cells;
            Player2Name = player2Name;
            Player2Cells = player2Cells;
            Events = new List<ReplayEvent>();
        }

        public ReplayData(int points, string player1Name, List<int?> player1Cells)
            : this(points, player1Name, player1Cells, null, null)
        {
        }

        protected ReplayData() {}

        #endregion Constructors

        #region Public Properties

        public List<ReplayEvent> Events
        {
            get;
            set;
        }

        #endregion Public Properties

        #region Private Properties

        public bool IsSinglePlayer
        {
            get
            {
                return Player2Name == null;
            }
        }

        public int Points { get; set; }
        public string Player1Name { get; set; }

        public List<int?> Player1Cells { get; set; }
        public string Player2Name { get; set; }

        public List<int?> Player2Cells { get; set; }

        #endregion Private Properties

        #region Public Methods

        public void AddEvent(ReplayEvent replayEvent)
        {
            Events.Add(replayEvent);
        }

        public string GetFilename()
        {
            var players = IsSinglePlayer ? "Solo" : string.Format("{0} vs {1}", Player1Name, Player2Name);
            var time = Events.First().RecordTime.ToString("yyyy-MM-dd-HH-mm-ss", CultureInfo.InvariantCulture);
            return string.Format("{0}_{1}.potr", time, players);
        }

        #endregion Public Methods
    }
}