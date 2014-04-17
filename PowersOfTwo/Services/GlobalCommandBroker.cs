using System.Windows.Input;

using PowersOfTwo.Framework;
using PowersOfTwo.Interfaces;
using PowersOfTwo.ViewModels;

namespace PowersOfTwo.Services
{
    public class GlobalCommandBroker
    {
        #region Constructors

        public GlobalCommandBroker(MainWindowViewModel mainWindowViewModel)
        {
            DownCommand =
                new RelayCommand(
                    p => (mainWindowViewModel.Content as IMovementCommandProvider).DownCommand.Execute(null),
                    p => (mainWindowViewModel.Content is IMovementCommandProvider) &&
                         (mainWindowViewModel.Content as IMovementCommandProvider).DownCommand.CanExecute(p));

            UpCommand =
                new RelayCommand(
                    p => (mainWindowViewModel.Content as IMovementCommandProvider).UpCommand.Execute(null),
                    p => (mainWindowViewModel.Content is IMovementCommandProvider) &&
                         (mainWindowViewModel.Content as IMovementCommandProvider).UpCommand.CanExecute(p));

            LeftCommand =
                new RelayCommand(
                    p => (mainWindowViewModel.Content as IMovementCommandProvider).LeftCommand.Execute(null),
                    p => (mainWindowViewModel.Content is IMovementCommandProvider) &&
                         (mainWindowViewModel.Content as IMovementCommandProvider).LeftCommand.CanExecute(p));

            RightCommand =
                new RelayCommand(
                    p => (mainWindowViewModel.Content as IMovementCommandProvider).RightCommand.Execute(null),
                    p =>
                        (mainWindowViewModel.Content is IMovementCommandProvider) &&
                        (mainWindowViewModel.Content as IMovementCommandProvider).RightCommand.CanExecute(p));
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
    }
}