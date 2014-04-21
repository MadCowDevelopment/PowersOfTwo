using PowersOfTwo.Services;

namespace PowersOfTwo.ViewModels
{
    public class AcceptOpponentViewModel : QueueTimerViewModel
    {
        #region Constructors

        public AcceptOpponentViewModel(GameProxy gameProxy, string opponentName)
            : base(gameProxy)
        {
            OpponentName = opponentName;
        }

        #endregion Constructors

        #region Public Properties

        public string OpponentName
        {
            get; private set;
        }

        #endregion Public Properties
    }
}