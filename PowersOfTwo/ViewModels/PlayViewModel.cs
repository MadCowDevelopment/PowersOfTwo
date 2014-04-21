using System.Windows.Input;

using PowersOfTwo.Framework;
using PowersOfTwo.Interfaces;

namespace PowersOfTwo.ViewModels
{
    public abstract class PlayViewModel : ObservableObject, IMovementCommandProvider
    {
        #region Constructors

        protected PlayViewModel(OverlayViewModel overlayViewModel)
        {
            OverlayViewModel = overlayViewModel;
            OverlayViewModel.Closed += OverlayViewModelClosed;

            LeftCommand = new RelayCommand(p => MoveLeft());
            RightCommand = new RelayCommand(p => MoveRight());
            UpCommand = new RelayCommand(p => MoveUp());
            DownCommand = new RelayCommand(p => MoveDown());
        }

        #endregion Constructors

        #region Public Properties

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

        public PlayerViewModel Player
        {
            get; protected set;
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

        #endregion Public Properties

        #region Protected Properties

        protected OverlayViewModel OverlayViewModel
        {
            get; private set;
        }

        #endregion Protected Properties

        #region Protected Methods

        protected abstract void MoveDown();

        protected abstract void MoveLeft();

        protected abstract void MoveRight();

        protected abstract void MoveUp();

        protected abstract void OnGameOver();

        #endregion Protected Methods

        #region Private Methods

        private void OverlayViewModelClosed(bool? result)
        {
            OverlayViewModel.Closed -= OverlayViewModelClosed;
            OnGameOver();
        }

        #endregion Private Methods
    }
}