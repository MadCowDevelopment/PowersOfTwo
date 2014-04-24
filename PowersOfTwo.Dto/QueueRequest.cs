using PowersOfTwo.Core;

namespace PowersOfTwo.Dto
{
    public class QueueRequest
    {
        #region Constructors

        public QueueRequest(string userName, int gameModeId)
        {
            UserName = userName;
            GameModeId = gameModeId;
        }

        #endregion Constructors

        #region Public Properties

        public int GameModeId
        {
            get; private set;
        }

        public string UserName
        {
            get; private set;
        }

        #endregion Public Properties
    }
}