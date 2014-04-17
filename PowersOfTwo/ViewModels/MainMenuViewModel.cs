using System.Windows;
using System.Windows.Input;

using Microsoft.AspNet.SignalR.Client;

using PowersOfTwo.Framework;
using PowersOfTwo.Services;

namespace PowersOfTwo.ViewModels
{
    public class MainMenuViewModel : ObservableObject
    {
        #region Fields

        private readonly GameProxy _gameProxy;
        private readonly MainWindowViewModel _mainWindowViewModel;
        private readonly OverlayViewModel _overlayViewModel;

        #endregion Fields

        #region Constructors

        public MainMenuViewModel(MainWindowViewModel mainWindowViewModel, OverlayViewModel overlayViewModel)
        {
            _mainWindowViewModel = mainWindowViewModel;
            _overlayViewModel = overlayViewModel;

            _gameProxy = new GameProxy();
            _gameProxy.ConnectionStateChanged += change => CanStartDuel = change.NewState == ConnectionState.Connected;

            PlayDuelCommand = new RelayCommand(p => QueueForDuelGame());
            PlaySoloCommand = new RelayCommand(p => StartSoloGame());
            PlayRankedCommand = new RelayCommand(p => QueueForRankedGame());
            QuitCommand = new RelayCommand(p => Application.Current.Shutdown());
        }

        #endregion Constructors

        #region Public Properties

        public bool CanStartDuel
        {
            get; private set;
        }

        public bool CanStartRanked
        {
            get; private set;
        }

        public ICommand PlayDuelCommand
        {
            get; private set;
        }

        public ICommand PlayRankedCommand
        {
            get; private set;
        }

        public ICommand PlaySoloCommand
        {
            get; private set;
        }

        public ICommand QuitCommand
        {
            get; private set;
        }

        #endregion Public Properties

        #region Private Methods

        private void QueueForDuelGame()
        {
            var duelViewModel = new DuelPlayViewModel(_overlayViewModel, _mainWindowViewModel, _gameProxy);
            _mainWindowViewModel.Content = new QueueViewModel(_mainWindowViewModel, _gameProxy);
            _gameProxy.GameStarted += p => _mainWindowViewModel.Content = duelViewModel;
            _gameProxy.Queue();
        }

        private void QueueForRankedGame()
        {
            // TODO
        }

        private void StartSoloGame()
        {
            _mainWindowViewModel.Content = new SoloPlayViewModel(_overlayViewModel, _mainWindowViewModel);
        }

        #endregion Private Methods
    }
}