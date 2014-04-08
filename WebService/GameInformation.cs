namespace WebService
{
    public class GameInformation
    {
        public string GroupName { get; private set; }
        public Player Player1 { get; private set; }
        public Player Player2 { get; private set; }
        public bool IsFinished { get; set; }

        public GameInformation(string groupName, Player player1, Player player2)
        {
            GroupName = groupName;
            Player1 = player1;
            Player2 = player2;
        }

        public Player GetPlayer(string connectionId)
        {
            if (Player1.ConnectionId == connectionId)
            {
                return Player1;
            }
            
            if (Player2.ConnectionId == connectionId)
            {
                return Player2;
            }

            return null;
        }

        public Player OtherPlayer(Player player)
        {
            if (player == Player1) return Player2;
            if (player == Player2) return Player1;
            return null;
        }
    }
}