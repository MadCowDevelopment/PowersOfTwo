namespace PowersOfTwo.Dto
{
    public class OpponentFoundDto
    {
        #region Constructors

        public OpponentFoundDto(string groupName, string opponentName)
        {
            GroupName = groupName;
            OpponentName = opponentName;
        }

        #endregion Constructors

        #region Public Properties

        public string GroupName
        {
            get; private set;
        }

        public string OpponentName
        {
            get; private set;
        }

        #endregion Public Properties
    }
}