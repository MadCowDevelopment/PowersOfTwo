using System.Timers;

using PowersOfTwo.Framework;

namespace PowersOfTwo.ViewModels
{
    public class StartGameCountdownViewModel : ObservableObject
    {
        #region Fields

        private readonly OverlayViewModel _overlayViewModel;
        private readonly Timer _timer;

        #endregion Fields

        #region Constructors

        public StartGameCountdownViewModel(OverlayViewModel overlayViewModel)
        {
            _overlayViewModel = overlayViewModel;
            RemainingTime = 3;
            _timer = new Timer(1000);
            _timer.Elapsed += TimerElapsed;
            _timer.Start();
        }

        #endregion Constructors

        #region Public Properties

        public int RemainingTime
        {
            get; private set;
        }

        #endregion Public Properties

        #region Private Methods

        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            RemainingTime--;
            if (RemainingTime == 0)
            {
                _timer.Stop();
                _overlayViewModel.Hide(null);
            }
        }

        #endregion Private Methods
    }
}