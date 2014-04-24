using System;
using System.IO;
using System.Linq;

using PowersOfTwo.Core;
using PowersOfTwo.Services;
using PowersOfTwo.Services.Replay;

namespace PowersOfTwo.ViewModels
{
    public class SoloPlayViewModel : PlayViewModel
    {
        #region Fields

        private readonly GameLogic _gameLogic;
        private readonly MainWindowViewModel _mainWindowViewModel;
        private readonly ReplayRecorder _replayRecorder;

        #endregion Fields

        #region Constructors

        public SoloPlayViewModel(OverlayViewModel overlayViewModel, MainWindowViewModel mainWindowViewModel)
            : base(overlayViewModel)
        {
            _mainWindowViewModel = mainWindowViewModel;

            _gameLogic = new GameLogic(4, 4);
            _gameLogic.CellsMatched += GameLogicCellsMatched;
            _gameLogic.OutOfMoves += GameLogicOutOfMoves;

            Player = new PlayerViewModel(Environment.MachineName);
            Player.Points = 0;

            _replayRecorder = new ReplayRecorder();
            _replayRecorder.Record(new GameStartedEvent(Player.Name, Player.Points));

            UpdateCells();
        }

        #endregion Constructors

        #region Protected Methods

        protected override void MoveDown()
        {
            _gameLogic.MoveDown();
            UpdateCells();
        }

        protected override void MoveLeft()
        {
            _gameLogic.MoveLeft();
            UpdateCells();
        }

        protected override void MoveRight()
        {
            _gameLogic.MoveRight();
            UpdateCells();
        }

        protected override void MoveUp()
        {
            _gameLogic.MoveUp();
            UpdateCells();
        }

        #endregion Protected Methods

        #region Private Methods

        private void GameLogicCellsMatched(int points)
        {
            Player.Points += points;
            _replayRecorder.Record(new PointsChangedEvent(1, Player.Points));
        }

        private void GameLogicOutOfMoves()
        {
            _replayRecorder.Save();
            OverlayViewModel.Show(new OverlayTextViewModel("Game Over", 72),
                p => _mainWindowViewModel.ShowHighscore(Player.Points));
        }

        private void UpdateCells()
        {
            Player.Cells = _gameLogic.Cells.ToList();
            _replayRecorder.Record(new CellsChangedEvent(1, Player.Cells.ToList()));
        }

        #endregion Private Methods
    }
}