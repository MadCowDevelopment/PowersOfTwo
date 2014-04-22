using PowersOfTwo.Core;

namespace PowersOfTwo.Dto
{
    public class QueueRequest
    {
        public string UserName { get; private set; }
        public int GameModeId { get; private set; }

        public QueueRequest(string userName, int gameModeId)
        {
            UserName = userName;
            GameModeId = gameModeId;
        }
    }
}
