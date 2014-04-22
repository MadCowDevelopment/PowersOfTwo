namespace PowersOfTwo.Services.Replay
{
    public class PointsChangedEvent : ReplayEvent
    {
        public int PlayerNumber { get; set; }
        public int Points { get; set; }

        public PointsChangedEvent(int playerNumber, int points)
        {
            PlayerNumber = playerNumber;
            Points = points;
        }

        public override void Replay(IReplayTarget replayTarget)
        {
            var player = PlayerNumber == 1 ? replayTarget.Player1 : replayTarget.Player2;
            player.Points = Points;
        }
    }
}