using System;
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

        private readonly ReplayRecorder _replayService;

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

            _replayService = new ReplayRecorder();
            _replayService.Record(new GameStartedEvent(Player.Name, Player.Points));
            
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

        private void UpdateCells()
        {
            Player.Cells = _gameLogic.Cells.ToList();
            _replayService.Record(new CellsChangedEvent(1, Player.Cells.ToList()));
        }

        private void GameLogicCellsMatched(int points)
        {
            Player.Points += points;
            _replayService.Record(new PointsChangedEvent(1, Player.Points));
        }

        private void GameLogicOutOfMoves()
        {
            OverlayViewModel.Closed += OverlayViewModelClosed;
            OverlayViewModel.Show(new OverlayTextViewModel("Game Over", 72), true, false);
        }

        private void OverlayViewModelClosed(bool? obj)
        {
            OverlayViewModel.Closed -= OverlayViewModelClosed;
            _mainWindowViewModel.ShowHighscore(Player.Points);
        }

        #endregion Private Methods
    }
}