using PowersOfTwo.ViewModels;

namespace PowersOfTwo.Services.Replay
{
    public interface IReplayTarget
    {
        #region Properties

        PlayerViewModel Player1
        {
            get; set;
        }

        PlayerViewModel Player2
        {
            get; set;
        }

        #endregion Properties
    }
}