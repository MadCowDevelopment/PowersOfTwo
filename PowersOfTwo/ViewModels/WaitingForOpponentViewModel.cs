using PowersOfTwo.Services;

namespace PowersOfTwo.ViewModels
{
    public class WaitingForOpponentViewModel : QueueTimerViewModel
    {
        #region Constructors

        public WaitingForOpponentViewModel(GameProxy gameProxy)
            : base(gameProxy)
        {
        }

        #endregion Constructors
    }
}