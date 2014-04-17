using System.Linq;
using PowersOfTwo.Core;

namespace PowersOfTwo.ViewModels
{
    public class SoloPlayViewModel : PlayViewModel
    {
        private readonly MainWindowViewModel _mainWindowViewModel;
        private readonly GameLogic _gameLogic = new GameLogic(4, 4);

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

        private void GameLogicOutOfMoves()
        {
            OverlayViewModel.Show(new OverlayTextViewModel("Game Over"));
        }

        private void GameLogicCellsMatched(int points)
        {
            Player.Points += points;
        }

        protected override void OnGameOver()
        {
            _mainWindowViewModel.ShowHighscore(Player.Points);
        }

        protected override void MoveDown()
        {
            _gameLogic.MoveDown();
            Player.Cells = _gameLogic.Cells.ToList();
        }

        protected override void MoveUp()
        {
            _gameLogic.MoveUp();
            Player.Cells = _gameLogic.Cells.ToList();
        }

        protected override void MoveRight()
        {
            _gameLogic.MoveRight();
            Player.Cells = _gameLogic.Cells.ToList();
        }

        protected override void MoveLeft()
        {
            _gameLogic.MoveLeft();
            Player.Cells = _gameLogic.Cells.ToList();
        }
    }
}
