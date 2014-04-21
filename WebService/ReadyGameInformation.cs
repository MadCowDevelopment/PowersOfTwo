using System;

namespace WebService
{
    internal class ReadyGameInformation : IDisposable
    {
        #region Fields

        private readonly System.Timers.Timer _timer;

        private int _remainingTime = 13;

        #endregion Fields

        #region Constructors

        public ReadyGameInformation(GameInformation gameInformation)
        {
            GameInformation = gameInformation;

            _timer = new System.Timers.Timer(1000);
            _timer.Elapsed += TimerElapsed;
            _timer.Start();
        }

        #endregion Constructors

        #region Events

        public event EventHandler<RemainingTimeChangedEventArgs> RemainingTimeChanged;

        #endregion Events

        #region Public Properties

        public GameInformation GameInformation
        {
            get; private set;
        }

        #endregion Public Properties

        #region Public Methods

        public void Dispose()
        {
            _timer.Stop();
        }

        #endregion Public Methods

        #region Private Methods

        private void RaiseRemainingTimeChanged(int remainingTime)
        {
            var handler = RemainingTimeChanged;
            if (handler != null) handler(this, new RemainingTimeChangedEventArgs(GameInformation, remainingTime));
        }

        private void TimerElapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            _remainingTime--;

            RaiseRemainingTimeChanged(_remainingTime);
        }

        #endregion Private Methods
    }
}