using PowersOfTwo.Core;

namespace WebService
{
    public class Player : User
    {
        #region Constructors

        public Player(string connectionId, string name, double remainingPoints)
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

        public double RemainingPoints
        {
            get;
            set;
        }

        #endregion Public Properties
    }

    public abstract class User
    {
        #region Constructors

        protected User(string connectionId, string name)
        {
            ConnectionId = connectionId;
            Name = name;
        }

        #endregion Constructors

        #region Public Properties

        public string ConnectionId
        {
            get; private set;
        }

        public string Name
        {
            get; private set;
        }

        #endregion Public Properties
    }

    public class Spectator : User
    {
        #region Constructors

        public Spectator(string connectionId, string name)
            : base(connectionId, name)
        {
        }

        #endregion Constructors
    }
}