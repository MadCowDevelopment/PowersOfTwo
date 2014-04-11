using System.Collections.Generic;
using System.Windows;
using PowersOfTwo.Core;
using WebService;

namespace PowersOfTwo
{
    public class DuelPlayViewModel : PlayViewModel
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
        }

        #endregion Constructors

        #region Public Properties

        public PlayerViewModel Opponent { get; private set; }

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
            Player.Points = startGameInformation.StartPoints;

            Opponent = new PlayerViewModel();
            Opponent.Cells = startGameInformation.OpponentCells;
            Opponent.Points = startGameInformation.StartPoints;
        }

        private void OpponentPointsUpdated(int remainingPoints)
        {
            Opponent.Points = remainingPoints;
        }

        private void OpponentCellsChanged(List<NumberCell> cells)
        {
            Opponent.Cells = cells;
        }

        private void CellsChanged(List<NumberCell> cells)
        {
            Player.Cells = cells;
        }

        protected override void MoveDown()
        {
            _gameProxy.MoveDown();
        }

        protected override void MoveLeft()
        {
            _gameProxy.MoveLeft();
        }

        protected override void MoveRight()
        {
            _gameProxy.MoveRight();
        }

        protected override void MoveUp()
        {
            _gameProxy.MoveUp();
        }

        private void PointsUpdated(int remainingPoints)
        {
            Player.Points = remainingPoints;
        }

        #endregion Private Methods
    }
}
