﻿using System.Collections.Generic;
using System.Windows;
using PowersOfTwo.Core;
using PowersOfTwo.Services;
using WebService;

namespace PowersOfTwo.ViewModels
{
    public class DuelPlayViewModel : PlayViewModel
    {
        #region Fields

        private readonly MainWindowViewModel _mainWindowViewModel;
        private readonly GameProxy _gameProxy;

        #endregion Fields

        #region Constructors

        public DuelPlayViewModel(
            OverlayViewModel overlayViewModel, 
            MainWindowViewModel mainWindowViewModel,
            GameProxy gameProxy) :base(overlayViewModel)
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

        public PlayerViewModel Opponent { get; private set; }

        #endregion Public Properties

        #region Private Methods

        private void GameOver(bool win)
        {
            var text = win ? "You win!" : "You lose!";
            OverlayViewModel.ShowOverlay(new OverlayTextViewModel(text));
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

        protected override void OnGameOver()
        {
            _mainWindowViewModel.ShowMainMenu();
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
