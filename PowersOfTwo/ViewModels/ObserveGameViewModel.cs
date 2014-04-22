using System.Threading.Tasks;
using System.Windows.Input;
using PowersOfTwo.Dto;
using PowersOfTwo.Framework;
using PowersOfTwo.Services;

namespace PowersOfTwo.ViewModels
{
    public class ObserveGameViewModel : ObservableObject
    {
        private readonly MainWindowViewModel _mainWindowViewModel;
        private readonly GameProxy _gameProxy;
        private readonly OverlayViewModel _overlayViewModel;
        private RunningGameDto _runningGame;

        public ObserveGameViewModel(
            MainWindowViewModel mainWindowViewModel, 
            GameProxy gameProxy, 
            OverlayViewModel overlayViewModel)
        {
            _mainWindowViewModel = mainWindowViewModel;
            _gameProxy = gameProxy;
            _overlayViewModel = overlayViewModel;

            LeaveCommand = new RelayCommand(p => Leave());
        }

        public async Task Initialize(string groupName)
        {
            _runningGame = await _gameProxy.GetRunningGame(groupName);

            Player1 = new PlayerViewModel(_runningGame.Player1.Name);
            Player1.Cells = _runningGame.Player1.Cells;
            Player1.Points = _runningGame.Player1.Points;
            Player2 = new PlayerViewModel(_runningGame.Player2.Name);
            Player2.Cells = _runningGame.Player2.Cells;
            Player2.Points = _runningGame.Player2.Points;

            _gameProxy.CellsChanged += cells => Player1.Cells = cells;
            _gameProxy.OpponentCellsChanged += cells => Player2.Cells = cells;
            _gameProxy.PointsUpdated += points => Player1.Points = points;
            _gameProxy.OpponentPointsUpdated += points => Player2.Points = points;
            _gameProxy.GameOver += GameProxyGameOver;
        }

        private void GameProxyGameOver(bool player1Win)
        {
            _overlayViewModel.Closed += GameOverOverlayClosed;
            var winnerName = player1Win ? Player1.Name : Player2.Name;
            _overlayViewModel.Show(new OverlayTextViewModel(string.Format("{0} wins", winnerName), 24), true, false);
        }

        private void GameOverOverlayClosed(bool? obj)
        {
            _mainWindowViewModel.ShowMainMenu();
        }

        public PlayerViewModel Player1 { get; private set; }
        public PlayerViewModel Player2 { get; private set; }

        private void Leave()
        {
            _overlayViewModel.Closed += OverlayViewModelClosed;
            _overlayViewModel.Show(new OverlayTextViewModel("Leave?", 72), true, true);
        }

        private void OverlayViewModelClosed(bool? result)
        {
            _overlayViewModel.Closed -= OverlayViewModelClosed;
            if (result.HasValue && result.Value)
            {
                _gameProxy.StopSpectating(_runningGame.GroupName);
                _mainWindowViewModel.ShowMainMenu();
            }
        }

        public ICommand LeaveCommand { get; private set; }
    }
}