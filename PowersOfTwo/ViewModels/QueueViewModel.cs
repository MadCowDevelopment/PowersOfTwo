using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using System.Windows.Threading;

using PowersOfTwo.Core;
using PowersOfTwo.Framework;
using PowersOfTwo.Services;

namespace PowersOfTwo.ViewModels
{
    public class QueueViewModel : ObservableObject
    {
        #region Fields

        private readonly GameProxy _gameProxy;
        private readonly MainWindowViewModel _mainWindowViewModel;
        private readonly DispatcherTimer _timer;

        private GameLogic _gameLogic;

        #endregion Fields

        #region Constructors

        public QueueViewModel(MainWindowViewModel mainWindowViewModel, GameProxy gameProxy)
        {
            _mainWindowViewModel = mainWindowViewModel;
            _gameProxy = gameProxy;

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
            get; private set;
        }

        public ICommand LeaveQueueCommand
        {
            get; private set;
        }

        #endregion Public Properties

        #region Private Methods

        private void GameLogicOutOfMoves()
        {
            _timer.Stop();
            InitializeGameLogic();
            _timer.Start();
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