namespace PowersOfTwo.Dto
{
    public class RunningGameDto
    {
        #region Constructors

        public RunningGameDto(string groupName, PlayerDto player1, PlayerDto player2)
        {
            GroupName = groupName;
            Player1 = player1;
            Player2 = player2;
        }

        #endregion Constructors

        #region Public Properties

        public string GroupName
        {
            get; private set;
        }

        public PlayerDto Player1
        {
            get; private set;
        }

        public PlayerDto Player2
        {
            get; private set;
        }

        #endregion Public Properties

        #region Public Methods

        public override bool Equals(object obj)
        {
            var other = obj as RunningGameDto;
            if (other == null) return false;
            return GroupName == other.GroupName;
        }

        public override int GetHashCode()
        {
            return GroupName.GetHashCode();
        }

        #endregion Public Methods
    }
}