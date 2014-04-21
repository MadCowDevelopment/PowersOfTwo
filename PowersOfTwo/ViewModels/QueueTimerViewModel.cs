using PowersOfTwo.Framework;
using PowersOfTwo.Services;

namespace PowersOfTwo.ViewModels
{
    public class QueueTimerViewModel : ObservableObject
    {
        #region Fields

        private readonly GameProxy _gameProxy;

        #endregion Fields

        #region Constructors

        public QueueTimerViewModel(GameProxy gameProxy)
        {
            _gameProxy = gameProxy;
            _gameProxy.QueueRemainingTimeChanged += GameProxyQueueRemainingTimeChanged;
        }

        #endregion Constructors

        #region Public Properties

        public int? RemainingTime
        {
            get; private set;
        }

        #endregion Public Properties

        #region Private Methods

        private void GameProxyQueueRemainingTimeChanged(int remainingTime)
        {
            RemainingTime = remainingTime;
        }

        #endregion Private Methods
    }
}