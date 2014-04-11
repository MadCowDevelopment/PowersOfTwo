using System.Windows.Input;

namespace PowersOfTwo
{
    public class QueueViewModel : ViewModel
    {
        private readonly MainWindowViewModel _mainWindowViewModel;
        private readonly GameProxy _gameProxy;

        public QueueViewModel(MainWindowViewModel mainWindowViewModel, GameProxy gameProxy)
        {
            _mainWindowViewModel = mainWindowViewModel;
            _gameProxy = gameProxy;
            LeaveQueueCommand = new RelayCommand(p => LeaveQueue());
        }

        private void LeaveQueue()
        {
            _gameProxy.LeaveQueue();
            _mainWindowViewModel.ShowMainMenu();
        }

        public ICommand LeaveQueueCommand { get; private set; }
    }
}