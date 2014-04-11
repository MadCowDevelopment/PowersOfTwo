using System.Windows.Input;
using Microsoft.AspNet.SignalR.Client;

namespace PowersOfTwo
{
    public class MainMenuViewModel : ViewModel
    {
        private readonly MainWindowViewModel _mainWindowViewModel;

        private readonly GameProxy _gameProxy;

        public MainMenuViewModel(MainWindowViewModel mainWindowViewModel)
        {
            _mainWindowViewModel = mainWindowViewModel;

            _gameProxy = new GameProxy();
            _gameProxy.ConnectionStateChanged += change => CanStartDuel = change.NewState == ConnectionState.Connected;

            PlayDuelCommand = new RelayCommand(p => QueueForDuel());
            PlaySoloCommand = new RelayCommand(p => StartSoloGame());
        }

        private void StartSoloGame()
        {
            _mainWindowViewModel.Content = new SoloPlayViewModel(_mainWindowViewModel);
        }

        public bool CanStartDuel { get; private set; }

        private void QueueForDuel()
        {
            var duelViewModel = new DuelPlayViewModel(_gameProxy);
            _mainWindowViewModel.Content = new QueueViewModel(_mainWindowViewModel, _gameProxy);
            _gameProxy.GameStarted += p => _mainWindowViewModel.Content = duelViewModel;
            _gameProxy.Queue();
        }

        public ICommand PlayDuelCommand { get; private set; }

        public ICommand PlaySoloCommand { get; private set; }
    }
}