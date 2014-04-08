using System.Collections.Generic;
using System.Windows.Input;

namespace PowersOfTwo
{
    public class MainWindowViewModel : ViewModel
    {
        private GameLogic _gameLogic;

        private const int Rows = 4;
        private const int Columns = 4;

        public MainWindowViewModel()
        {
            Initialize();
            LeftCommand = new RelayCommand(p => MoveLeft());
            RightCommand = new RelayCommand(p => MoveRight());
            UpCommand = new RelayCommand(p => MoveUp());
            DownCommand = new RelayCommand(p => MoveDown());
        }

        public ICommand LeftCommand { get; private set; }

        public ICommand RightCommand { get; private set; }

        public ICommand UpCommand { get; private set; }

        public ICommand DownCommand { get; private set; }

        public List<NumberCell> Cells { get { return _gameLogic.Cells; } }

        private void Initialize()
        {
            _gameLogic = new GameLogic(Rows, Columns);
        }

        private void MoveLeft()
        {
            _gameLogic.MoveLeft();
        }

        private void MoveRight()
        {
            _gameLogic.MoveRight();
        }

        private void MoveUp()
        {
            _gameLogic.MoveUp();
        }

        private void MoveDown()
        {
            _gameLogic.MoveDown();
        }
    }
}
