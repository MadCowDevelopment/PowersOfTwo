using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using System.Windows.Threading;

using PowersOfTwo.Core;
using PowersOfTwo.Dto;
using PowersOfTwo.Framework;
using PowersOfTwo.Services;

namespace PowersOfTwo.ViewModels
{
    public class QueueViewModel : ObservableObject
    {
        #region Fields

        private readonly GameProxy _gameProxy;
        private readonly MainWindowViewModel _mainWindowViewModel;
        private readonly OverlayViewModel _overlayViewModel;
        private readonly DispatcherTimer _timer;

        private GameLogic _gameLogic;
        private bool _opponentReady;

        #endregion Fields

        #region Constructors

        public QueueViewModel(MainWindowViewModel mainWindowViewModel, OverlayViewModel overlayViewModel, GameProxy gameProxy)
        {
            _mainWindowViewModel = mainWindowViewModel;
            _overlayViewModel = overlayViewModel;
            _gameProxy = gameProxy;
            _gameProxy.OpponentFound += GameProxyOpponentFound;
            _gameProxy.OpponentReady += GameProxyOpponentReady;
            _gameProxy.OpponentLeft += GameProxyOpponentLeft;
            _gameProxy.GameStarted += GameProxyGameStarted;
            _gameProxy.GameCanceled += GameProxyGameCanceled;

            InitializeGameLogic();

            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromMilliseconds(100);
            _timer.Tick += TimerTick;
            _timer.Start();

            LeaveQueueCommand = new RelayCommand(p => LeaveQueue());
        }

        #endregion Constructors

        #region Public Properties

        public List<NumberCell> Cells
        {
            get;
            private set;
        }

        public ICommand LeaveQueueCommand
        {
            get;
            private set;
        }

        #endregion Public Properties

        #region Private Methods

        private void AcceptOpponentOverlayClosed(bool? result)
        {
            _overlayViewModel.Closed -= AcceptOpponentOverlayClosed;
            if (!result.HasValue) return;

            if (result.Value)
            {
                _gameProxy.AcceptGame();
                if (!_opponentReady) _overlayViewModel.Show(new WaitingForOpponentViewModel(_gameProxy), false, false);
            }
            else
            {
                _gameProxy.RejectGame();
                _mainWindowViewModel.ShowMainMenu();
            }
        }

        private void GameCanceledOverlayClosed(bool? result)
        {
            _overlayViewModel.Closed -= GameCanceledOverlayClosed;
            _mainWindowViewModel.ShowMainMenu();
        }

        private void GameLogicOutOfMoves()
        {
            _timer.Stop();
            InitializeGameLogic();
            _timer.Start();
        }

        private void GameProxyGameCanceled()
        {
            _overlayViewModel.Hide(null);
            _overlayViewModel.Closed += GameCanceledOverlayClosed;
            _overlayViewModel.Show(new OverlayTextViewModel("Time's up!", 72), true, false);
        }

        private void GameProxyGameStarted(StartGameDto obj)
        {
            _overlayViewModel.Hide(null);
        }

        private void GameProxyOpponentFound(string opponentName)
        {
            _overlayViewModel.Closed += AcceptOpponentOverlayClosed;
            _overlayViewModel.Show(new AcceptOpponentViewModel(_gameProxy, opponentName), true, true);
        }

        private void GameProxyOpponentLeft()
        {
            _overlayViewModel.Hide(null);
            _overlayViewModel.Closed += GameCanceledOverlayClosed;
            _overlayViewModel.Show(new OverlayTextViewModel("Opponent left!", 42), true, false);
        }

        private void GameProxyOpponentReady()
        {
            _opponentReady = true;
        }

        private void InitializeGameLogic()
        {
            _gameLogic = new GameLogic(4, 4);
            _gameLogic.OutOfMoves += GameLogicOutOfMoves;
            Cells = _gameLogic.Cells;
        }

        private void LeaveQueue()
        {
            _gameProxy.LeaveQueue();
            _mainWindowViewModel.ShowMainMenu();
        }

        private void TimerTick(object sender, EventArgs e)
        {
            var randomDirection = RNG.Next(0, 4);
            switch (randomDirection)
            {
                case 0:
                    _gameLogic.MoveLeft();
                    break;
                case 1:
                    _gameLogic.MoveRight();
                    break;
                case 2:
                    _gameLogic.MoveUp();
                    break;
                case 3:
                    _gameLogic.MoveDown();
                    break;
            }

            Cells = _gameLogic.Cells.ToList();
        }

        #endregion Private Methods
    }
}