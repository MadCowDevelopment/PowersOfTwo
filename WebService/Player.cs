using PowersOfTwo.Core;

namespace WebService
{
    public class Player : User
    {
        #region Constructors

        public Player(string connectionId, string name, int remainingPoints)
            : base(connectionId, name)
        {
            RemainingPoints = remainingPoints;
        }

        #endregion Constructors

        #region Public Properties

        public GameLogic GameLogic
        {
            get;
            set;
        }

        public bool IsReady
        {
            get;
            set;
        }

        public int RemainingPoints
        {
            get;
            set;
        }

        #endregion Public Properties
    }

    public abstract class User
    {
        public string ConnectionId { get; private set; }
        public string Name { get; private set; }

        protected User(string connectionId, string name)
        {
            ConnectionId = connectionId;
            Name = name;
        }
    }

    public class Spectator : User
    {
        public Spectator(string connectionId, string name)
            : base(connectionId, name)
        {

        }
    }
}