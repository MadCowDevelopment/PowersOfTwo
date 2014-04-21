using PowersOfTwo.Core;

namespace WebService
{
    public class Player
    {
        #region Constructors

        public Player(string connectionId, string name, int remainingPoints)
        {
            ConnectionId = connectionId;
            Name = name;
            RemainingPoints = remainingPoints;
        }

        #endregion Constructors

        #region Public Properties

        public string ConnectionId
        {
            get; private set;
        }

        public GameLogic GameLogic
        {
            get; set;
        }

        public bool IsReady
        {
            get; set;
        }

        public string Name
        {
            get; private set;
        }

        public int RemainingPoints
        {
            get; set;
        }

        #endregion Public Properties
    }
}