using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

using PowersOfTwo.Core;

using WebService;

namespace PowersOfTwo
{
    public class MainWindowViewModel : ViewModel
    {
        #region Fields

        private GameProxy _gameProxy;
        private PlayerViewModel _player;
        private PlayerViewModel _opponent;

        #endregion Fields

        #region Constructors

        public MainWindowViewModel()
        {
            Initialize();
            LeftCommand = new RelayCommand(p => MoveLeft());
            RightCommand = new RelayCommand(p => MoveRight());
            UpCommand = new RelayCommand(p => MoveUp());
            DownCommand = new RelayCommand(p => MoveDown());
        }

        #endregion Constructors

        #region Public Properties

        public ICommand DownCommand
        {
            get; private set;
        }

        public ICommand LeftCommand
        {
            get; private set;
        }

        public PlayerViewModel Player
        {
            get { return _player; }
            private set { _player = value; OnPropertyChanged(); }
        }

        public PlayerViewModel Opponent
        {
            get { return _opponent; }
            private set { _opponent = value; OnPropertyChanged(); }
        }

        public ICommand RightCommand
        {
            get; private set;
        }

        public ICommand UpCommand
        {
            get; private set;
        }

        #endregion Public Properties

        #region Private Methods

        private void GameOver(bool win)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                MessageBox.Show(win ? "You win." : "You lose.");
                Application.Current.Shutdown();
            });
        }

        private void GameStarted(StartGameInformation startGameInformation)
        {
            Player = new PlayerViewModel();
            Player.Cells = startGameInformation.Cells;
            Player.RemainingPoints = startGameInformation.StartPoints;

            Opponent = new PlayerViewModel();
            Opponent.Cells = startGameInformation.OpponentCells;
            Opponent.RemainingPoints = startGameInformation.StartPoints;
        }

        private void Initialize()
        {
            _gameProxy = new GameProxy();
            _gameProxy.GameOver += GameOver;
            _gameProxy.GameStarted += GameStarted;
            _gameProxy.PointsUpdated += PointsUpdated;
            _gameProxy.CellsChanged += CellsChanged;
            _gameProxy.OpponentCellsChanged += OpponentCellsChanged;
            _gameProxy.OpponentPointsUpdated += OpponentPointsUpdated;

            _gameProxy.Queue();
        }

        private void OpponentPointsUpdated(int remainingPoints)
        {
            Opponent.RemainingPoints = remainingPoints;
        }

        private void OpponentCellsChanged(List<NumberCell> cells)
        {
            Opponent.Cells = cells;
        }

        private void CellsChanged(List<NumberCell> cells)
        {
            Player.Cells = cells;
        }

        private void MoveDown()
        {
            _gameProxy.MoveDown();
        }

        private void MoveLeft()
        {
            _gameProxy.MoveLeft();
        }

        private void MoveRight()
        {
            _gameProxy.MoveRight();
        }

        private void MoveUp()
        {
            _gameProxy.MoveUp();
        }

        private void PointsUpdated(int remainingPoints)
        {
            Player.RemainingPoints = remainingPoints;
        }

        #endregion Private Methods
    }
}