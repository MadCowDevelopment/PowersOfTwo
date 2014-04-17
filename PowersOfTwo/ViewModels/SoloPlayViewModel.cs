using System.Linq;

using PowersOfTwo.Core;

namespace PowersOfTwo.ViewModels
{
    public class SoloPlayViewModel : PlayViewModel
    {
        #region Fields

        private readonly GameLogic _gameLogic = new GameLogic(4, 4);
        private readonly MainWindowViewModel _mainWindowViewModel;

        #endregion Fields

        #region Constructors

        public SoloPlayViewModel(OverlayViewModel overlayViewModel, MainWindowViewModel mainWindowViewModel)
            : base(overlayViewModel)
        {
            _mainWindowViewModel = mainWindowViewModel;

            _gameLogic = new GameLogic(4, 4);
            _gameLogic.CellsMatched += GameLogicCellsMatched;
            _gameLogic.OutOfMoves += GameLogicOutOfMoves;

            Player = new PlayerViewModel();
            Player.Points = 0;
            Player.Cells = _gameLogic.Cells;
        }

        #endregion Constructors

        #region Protected Methods

        protected override void MoveDown()
        {
            _gameLogic.MoveDown();
            Player.Cells = _gameLogic.Cells.ToList();
        }

        protected override void MoveLeft()
        {
            _gameLogic.MoveLeft();
            Player.Cells = _gameLogic.Cells.ToList();
        }

        protected override void MoveRight()
        {
            _gameLogic.MoveRight();
            Player.Cells = _gameLogic.Cells.ToList();
        }

        protected override void MoveUp()
        {
            _gameLogic.MoveUp();
            Player.Cells = _gameLogic.Cells.ToList();
        }

        protected override void OnGameOver()
        {
            _mainWindowViewModel.ShowHighscore(Player.Points);
        }

        #endregion Protected Methods

        #region Private Methods

        private void GameLogicCellsMatched(int points)
        {
            Player.Points += points;
        }

        private void GameLogicOutOfMoves()
        {
            OverlayViewModel.Show(new OverlayTextViewModel("Game Over"));
        }

        #endregion Private Methods
    }
}