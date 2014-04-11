using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using PowersOfTwo.Core;
using WebService;

namespace PowersOfTwo
{
    public class DuelPlayViewModel : ViewModel, IMovementCommandProvider
    {
        #region Fields

        private readonly GameProxy _gameProxy;

        #endregion Fields

        #region Constructors

        public DuelPlayViewModel(GameProxy gameProxy)
        {
            _gameProxy = gameProxy;
            _gameProxy.GameOver += GameOver;
            _gameProxy.GameStarted += GameStarted;
            _gameProxy.PointsUpdated += PointsUpdated;
            _gameProxy.CellsChanged += CellsChanged;
            _gameProxy.OpponentCellsChanged += OpponentCellsChanged;
            _gameProxy.OpponentPointsUpdated += OpponentPointsUpdated;

            LeftCommand = new RelayCommand(p => MoveLeft());
            RightCommand = new RelayCommand(p => MoveRight());
            UpCommand = new RelayCommand(p => MoveUp());
            DownCommand = new RelayCommand(p => MoveDown());
        }

        #endregion Constructors

        #region Public Properties

        public ICommand DownCommand
        {
            get;
            private set;
        }

        public ICommand LeftCommand
        {
            get;
            private set;
        }

        public PlayerViewModel Player { get; private set; }

        public PlayerViewModel Opponent { get; private set; }

        public ICommand RightCommand
        {
            get;
            private set;
        }

        public ICommand UpCommand
        {
            get;
            private set;
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
