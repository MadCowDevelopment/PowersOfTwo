using System.Windows.Input;
using PowersOfTwo.Framework;
using PowersOfTwo.Interfaces;

namespace PowersOfTwo.ViewModels
{
    public abstract class PlayViewModel : ObservableObject, IMovementCommandProvider
    {
        protected PlayViewModel()
        {
            LeftCommand = new RelayCommand(p => MoveLeft());
            RightCommand = new RelayCommand(p => MoveRight());
            UpCommand = new RelayCommand(p => MoveUp());
            DownCommand = new RelayCommand(p => MoveDown());
        }

        public PlayerViewModel Player { get; protected set; }

        protected abstract void MoveDown();

        protected abstract void MoveUp();

        protected abstract void MoveRight();

        protected abstract void MoveLeft();

        public ICommand DownCommand
        {
            get;
            private set;
        }

        public ICommand LeftCommand
        {
            get;
            private set;
        }

        public ICommand RightCommand
        {
            get;
            private set;
        }

        public ICommand UpCommand
        {
            get;
            private set;
        }
    }
}