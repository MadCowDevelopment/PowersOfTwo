﻿using System.Collections.Generic;

using PowersOfTwo.Core;
using PowersOfTwo.Dto;
using PowersOfTwo.Services;
using PowersOfTwo.Services.Replay;

namespace PowersOfTwo.ViewModels
{
    public class DuelPlayViewModel : PlayViewModel
    {
        #region Fields

        private readonly GameProxy _gameProxy;
        private readonly MainWindowViewModel _mainWindowViewModel;

        private readonly ReplayRecorder _replayRecorder;

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

            _replayRecorder = new ReplayRecorder();
        }

        #endregion Constructors

        #region Public Properties

        public PlayerViewModel Opponent
        {
            get;
            private set;
        }

        #endregion Public Properties

        #region Public Methods

        public void Start()
        {
            OverlayViewModel.Show(new StartGameCountdownViewModel(OverlayViewModel), allowAccept: false);
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
            _replayRecorder.Record(new CellsChangedEvent(1, cells));
        }

        private void GameOver(bool win)
        {
            var text = win ? "You win!" : "You lose!";
            OverlayViewModel.Show(new OverlayTextViewModel(text, 72), p => _mainWindowViewModel.ShowMainMenu());
        }

        private void GameStarted(StartGameDto startGameInformation)
        {
            Player = new PlayerViewModel(startGameInformation.Name);
            Player.Cells = startGameInformation.Cells;
            Player.Points = startGameInformation.StartPoints;

            Opponent = new PlayerViewModel(startGameInformation.OpponentName);
            Opponent.Cells = startGameInformation.OpponentCells;
            Opponent.Points = startGameInformation.StartPoints;

            _replayRecorder.Record(new GameStartedEvent(Player.Name, Opponent.Name, startGameInformation.StartPoints));
        }

        private void OpponentCellsChanged(List<NumberCell> cells)
        {
            Opponent.Cells = cells;
            _replayRecorder.Record(new CellsChangedEvent(2, cells));
        }

        private void OpponentPointsUpdated(int remainingPoints)
        {
            Opponent.Points = remainingPoints;
            _replayRecorder.Record(new PointsChangedEvent(2, remainingPoints));
        }

        private void PointsUpdated(int remainingPoints)
        {
            Player.Points = remainingPoints;
            _replayRecorder.Record(new PointsChangedEvent(1, remainingPoints));
        }

        #endregion Private Methods
    }
}