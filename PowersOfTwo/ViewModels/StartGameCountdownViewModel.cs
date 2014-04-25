using System.Globalization;
using System.Timers;

using PowersOfTwo.Framework;

namespace PowersOfTwo.ViewModels
{
    public class StartGameCountdownViewModel : ObservableObject
    {
        #region Fields

        private readonly OverlayViewModel _overlayViewModel;
        private readonly Timer _timer;

        private int _remainingTime;

        #endregion Fields

        #region Constructors

        public StartGameCountdownViewModel(OverlayViewModel overlayViewModel)
        {
            _overlayViewModel = overlayViewModel;
            _remainingTime = 3;
            RemainingTime = _remainingTime.ToString(CultureInfo.InvariantCulture);

            _timer = new Timer(1000);
            _timer.Elapsed += TimerElapsed;
            _timer.Start();
        }

        #endregion Constructors

        #region Public Properties

        public string RemainingTime
        {
            get; private set;
        }

        #endregion Public Properties

        #region Private Methods

        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            _remainingTime--;
            if (_remainingTime == 0)
            {
                RemainingTime = "Go!";
            }
            else if (_remainingTime == -1)
            {
                _timer.Stop();
                _overlayViewModel.Hide(null);
            }
            else
            {
                RemainingTime = _remainingTime.ToString(CultureInfo.InvariantCulture);
            }
        }

        #endregion Private Methods
    }
}