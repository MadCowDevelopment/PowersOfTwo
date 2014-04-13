using System.Windows;
using System.Windows.Input;
using Microsoft.AspNet.SignalR.Client;
using PowersOfTwo.Framework;
using PowersOfTwo.Services;

namespace PowersOfTwo.ViewModels
{
    public class MainMenuViewModel : ObservableObject
    {
        private readonly MainWindowViewModel _mainWindowViewModel;
        private readonly OverlayViewModel _overlayViewModel;

        private readonly GameProxy _gameProxy;

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

        private void QueueForRankedGame()
        {
            // TODO
        }

        private void StartSoloGame()
        {
            _mainWindowViewModel.Content = new SoloPlayViewModel(_overlayViewModel, _mainWindowViewModel);
        }

        public bool CanStartDuel { get; private set; }

        public bool CanStartRanked { get; private set; }

        private void QueueForDuelGame()
        {
            var duelViewModel = new DuelPlayViewModel(_overlayViewModel, _mainWindowViewModel, _gameProxy);
            _mainWindowViewModel.Content = new QueueViewModel(_mainWindowViewModel, _gameProxy);
            _gameProxy.GameStarted += p => _mainWindowViewModel.Content = duelViewModel;
            _gameProxy.Queue();
        }

        public ICommand PlayDuelCommand { get; private set; }

        public ICommand PlaySoloCommand { get; private set; }

        public ICommand PlayRankedCommand { get; private set; }

        public ICommand QuitCommand { get; private set; }
    }
}