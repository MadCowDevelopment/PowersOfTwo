using System.Collections.Generic;

using PowersOfTwo.Core;
using PowersOfTwo.Services;

using WebService;

namespace PowersOfTwo.ViewModels
{
    public class DuelPlayViewModel : PlayViewModel
    {
        #region Fields

        private readonly GameProxy _gameProxy;
        private readonly MainWindowViewModel _mainWindowViewModel;

        #endregion Fields

        #region Constructors

        public DuelPlayViewModel(
            OverlayViewModel overlayViewModel,
            MainWindowViewModel mainWindowViewModel,
            GameProxy gameProxy)
            : base(overlayViewModel)
        {
            _mainWindowViewModel = mainWindowViewModel;
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

        public PlayerViewModel Opponent
        {
            get; private set;
        }

        #endregion Public Properties

        #region Public Methods

        public void Start()
        {
            OverlayViewModel.Show(new StartGameCountdownViewModel(OverlayViewModel), false, false);
        }

        #endregion Public Methods

        #region Protected Methods

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

        #endregion Protected Methods

        #region Private Methods

        private void CellsChanged(List<NumberCell> cells)
        {
            Player.Cells = cells;
        }

        private void GameOver(bool win)
        {
            var text = win ? "You win!" : "You lose!";
            OverlayViewModel.Closed += OverlayViewModelClosed;
            OverlayViewModel.Show(new OverlayTextViewModel(text, 72), true, false);
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

        private void OpponentCellsChanged(List<NumberCell> cells)
        {
            Opponent.Cells = cells;
        }

        private void OpponentPointsUpdated(int remainingPoints)
        {
            Opponent.Points = remainingPoints;
        }

        private void OverlayViewModelClosed(bool? obj)
        {
            OverlayViewModel.Closed -= OverlayViewModelClosed;
            _mainWindowViewModel.ShowMainMenu();
        }

        private void PointsUpdated(int remainingPoints)
        {
            Player.Points = remainingPoints;
        }

        #endregion Private Methods
    }
}