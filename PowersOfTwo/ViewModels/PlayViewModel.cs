using System.Windows.Input;
using PowersOfTwo.Framework;
using PowersOfTwo.Interfaces;

namespace PowersOfTwo.ViewModels
{
    public abstract class PlayViewModel : ObservableObject, IMovementCommandProvider
    {
        protected OverlayViewModel OverlayViewModel { get; private set; }

        protected PlayViewModel(OverlayViewModel overlayViewModel)
        {
            OverlayViewModel = overlayViewModel;
            OverlayViewModel.Closed += OverlayViewModelClosed;
            
            LeftCommand = new RelayCommand(p => MoveLeft());
            RightCommand = new RelayCommand(p => MoveRight());
            UpCommand = new RelayCommand(p => MoveUp());
            DownCommand = new RelayCommand(p => MoveDown());
        }

        private void OverlayViewModelClosed()
        {
            OverlayViewModel.Closed -= OverlayViewModelClosed;
            OnGameOver();
        }

        protected abstract void OnGameOver();

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