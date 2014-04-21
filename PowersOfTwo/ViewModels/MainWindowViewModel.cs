using System;

using PowersOfTwo.Framework;
using PowersOfTwo.Services;

namespace PowersOfTwo.ViewModels
{
    public class MainWindowViewModel : ObservableObject, IDisposable
    {
        #region Fields

        private readonly MainMenuViewModel _mainMenuViewModel;

        #endregion Fields

        #region Constructors

        public MainWindowViewModel()
        {
            Overlay = new OverlayViewModel();

            _mainMenuViewModel = new MainMenuViewModel(this, Overlay);
            CommandBroker = new GlobalCommandBroker(this);
            ShowMainMenu();
        }

        #endregion Constructors

        #region Public Properties

        public GlobalCommandBroker CommandBroker
        {
            get; private set;
        }

        public ObservableObject Content
        {
            get; set;
        }

        public OverlayViewModel Overlay
        {
            get; private set;
        }

        #endregion Public Properties

        #region Public Methods

        public void Dispose()
        {
            _mainMenuViewModel.Dispose();
        }

        public void ShowHighscore(int score)
        {
            Content = new HighscoreViewModel(this, score);
        }

        public void ShowMainMenu()
        {
            Content = _mainMenuViewModel;
        }

        #endregion Public Methods
    }
}