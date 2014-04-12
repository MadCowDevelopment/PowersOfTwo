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
    public class QueueViewModel : ViewModel
    {
        private readonly MainWindowViewModel _mainWindowViewModel;
        private readonly GameProxy _gameProxy;

        private GameLogic _gameLogic;

        private readonly DispatcherTimer _timer;

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

        private void InitializeGameLogic()
        {
            _gameLogic = new GameLogic(4, 4);
            _gameLogic.OutOfMoves += GameLogicOutOfMoves;
            Cells = _gameLogic.Cells;
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

        private void GameLogicOutOfMoves()
        {
            _timer.Stop();
            InitializeGameLogic();
            _timer.Start();
        }

        public List<NumberCell> Cells { get; private set; }

        private void LeaveQueue()
        {
            _gameProxy.LeaveQueue();
            _mainWindowViewModel.ShowMainMenu();
        }

        public ICommand LeaveQueueCommand { get; private set; }
    }
}