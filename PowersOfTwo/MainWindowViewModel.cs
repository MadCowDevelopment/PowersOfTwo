using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

using PowersOfTwo.Core;

using WebService;

namespace PowersOfTwo
{
    public class MainWindowViewModel : ViewModel
    {
        #region Fields

        private List<NumberCell> _cells;
        private GameProxy _gameProxy;
        private int _remainingPoints;

        #endregion Fields

        #region Constructors

        public MainWindowViewModel()
        {
            Initialize();
            LeftCommand = new RelayCommand(p => MoveLeft());
            RightCommand = new RelayCommand(p => MoveRight());
            UpCommand = new RelayCommand(p => MoveUp());
            DownCommand = new RelayCommand(p => MoveDown());
        }

        #endregion Constructors

        #region Public Properties

        public List<NumberCell> Cells
        {
            get { return _cells; }
            private set { _cells = value; OnPropertyChanged(); }
        }

        public ICommand DownCommand
        {
            get; private set;
        }

        public ICommand LeftCommand
        {
            get; private set;
        }

        public int RemainingPoints
        {
            get { return _remainingPoints; }
            set
            {
                _remainingPoints = value; OnPropertyChanged();
            }
        }

        public ICommand RightCommand
        {
            get; private set;
        }

        public ICommand UpCommand
        {
            get; private set;
        }

        #endregion Public Properties

        #region Private Methods

        private void GameOver(bool win)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                MessageBox.Show(win ? "You win." : "You lose.");
                Application.Current.Shutdown();
            });
        }

        private void GameStarted(StartGameInformation startGameInformation)
        {
            RemainingPoints = startGameInformation.StartPoints;
            Cells = startGameInformation.Cells;
        }

        private void Initialize()
        {
            _gameProxy = new GameProxy();
            _gameProxy.GameOver += GameOver;
            _gameProxy.GameStarted += GameStarted;
            _gameProxy.PointsUpdated += PointsUpdated;

            _gameProxy.Queue();
        }

        private void MoveDown()
        {
            Cells = _gameProxy.MoveDown();
        }

        private void MoveLeft()
        {
            Cells = _gameProxy.MoveLeft();
        }

        private void MoveRight()
        {
            Cells = _gameProxy.MoveRight();
        }

        private void MoveUp()
        {
            Cells = _gameProxy.MoveUp();
        }

        private void PointsUpdated(int remainingPoints)
        {
            RemainingPoints = remainingPoints;
        }

        #endregion Private Methods
    }
}