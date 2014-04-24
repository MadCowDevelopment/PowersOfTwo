namespace PowersOfTwo.Services.Replay
{
    public class PointsChangedEvent : ReplayEvent
    {
        #region Constructors

        public PointsChangedEvent(int playerNumber, int points)
        {
            PlayerNumber = playerNumber;
            Points = points;
        }

        #endregion Constructors

        #region Public Properties

        public int PlayerNumber
        {
            get; set;
        }

        public int Points
        {
            get; set;
        }

        #endregion Public Properties

        #region Public Methods

        public override void Replay(IReplayTarget replayTarget)
        {
            var player = PlayerNumber == 1 ? replayTarget.Player1 : replayTarget.Player2;
            player.Points = Points;
        }

        #endregion Public Methods
    }
}