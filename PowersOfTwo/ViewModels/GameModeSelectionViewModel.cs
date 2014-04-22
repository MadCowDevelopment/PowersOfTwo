using System.Windows.Input;
using PowersOfTwo.Core;
using PowersOfTwo.Framework;
using PowersOfTwo.Services;

namespace PowersOfTwo.ViewModels
{
    public class GameModeSelectionViewModel : ObservableObject
    {
        private readonly MainWindowViewModel _mainWindowViewModel;
        private readonly OverlayViewModel _overlayViewModel;
        private readonly GameProxy _gameProxy;

        public GameModeSelectionViewModel(MainWindowViewModel mainWindowViewModel, OverlayViewModel overlayViewModel, GameProxy gameProxy)
        {
            _mainWindowViewModel = mainWindowViewModel;
            _overlayViewModel = overlayViewModel;
            _gameProxy = gameProxy;

            PlayShortCommand = new RelayCommand(p => QueueForDuelGame(new ShortGameMode()));
            PlayLongCommand = new RelayCommand(p=> QueueForDuelGame(new LongGameMode()));
            PlayRankedCommand = new RelayCommand(p => QueueForDuelGame(new RankedGameMode()), p => false);
            LeaveCommand = new RelayCommand(p => _mainWindowViewModel.ShowMainMenu());
        }

        public ICommand PlayShortCommand
        {
            get;
            private set;
        }

        public ICommand PlayLongCommand
        {
            get;
            private set;
        }
        
        public ICommand PlayRankedCommand
        {
            get;
            private set;
        }

        public ICommand LeaveCommand
        {
            get;
            private set;
        }

        private void QueueForDuelGame(GameMode gameMode)
        {
            var duelViewModel = new DuelPlayViewModel(_overlayViewModel, _mainWindowViewModel, _gameProxy);
            _mainWindowViewModel.Content = new QueueViewModel(_mainWindowViewModel, _overlayViewModel, _gameProxy);
            _gameProxy.GameStarted += p => StartGame(duelViewModel);
            _gameProxy.Queue(gameMode);
        }

        private void StartGame(DuelPlayViewModel duelPlayViewModel)
        {
            duelPlayViewModel.Start();
            _mainWindowViewModel.Content = duelPlayViewModel;
        }
    }
}