using System.Collections.Generic;
using PowersOfTwo.Core;

namespace WebService
{
    public class GameInformation
    {
        #region Constructors

        public GameInformation(string groupName, GameMode gameMode, Player player1, Player player2)
        {
            GroupName = groupName;
            GameMode = gameMode;
            Player1 = player1;
            Player2 = player2;
            Spectators = new List<Spectator>();
        }

        #endregion Constructors

        #region Public Properties

        public string GroupName
        {
            get;
            private set;
        }

        public GameMode GameMode { get; private set; }

        public bool IsFinished
        {
            get;
            set;
        }

        public Player Player1
        {
            get;
            private set;
        }

        public Player Player2
        {
            get;
            private set;
        }

        public List<Spectator> Spectators { get; private set; }

        #endregion Public Properties

        #region Public Methods

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

        public bool HasAnyPlayerConnectionId(string connectionId)
        {
            return Player1.ConnectionId == connectionId || Player2.ConnectionId == connectionId;
        }

        public Player OtherPlayer(Player player)
        {
            if (player == Player1) return Player2;
            if (player == Player2) return Player1;
            return null;
        }

        #endregion Public Methods
    }

}