namespace WebService
{
    public class Player
    {
        public Player(string connectionId, string name, int remainingPoints)
        {
            ConnectionId = connectionId;
            Name = name;
            RemainingPoints = remainingPoints;
        }

        public string ConnectionId { get; private set; }

        public string Name { get; private set; }

        public int RemainingPoints { get; set; }
    }
}